using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] private string fileName;

    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }//确保saveManager为单例对象，保证存档系统全局唯一

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        saveManagers = FindAllSaveManagers();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No saved data found!");
            NewGame();
        }

        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public GameData GetCurrentGameData()
    {
        if(gameData == null)
        {
            NewGame();
        }
        return gameData;
    }
    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }//找到所有实现了接口的脚本，保存到list中

    //存档点系统，可以通过下面的两个方法手动读取和加载存档
    public void SaveAtCheckpoint()
    {
        if(gameData == null)
        {
            Debug.Log("No gameData to be saved.");
        }
        else
        {
            SaveGame();
            Debug.Log("GameData saved at checkpoint.");
        }
    }
    public void LoadFromLastCheckpoint()
    {
        LoadGame();

        if(CheckpointManager.instance != null)
        {
            CheckpointManager.instance.RestoreFromLastCheckpoint();
        }
    }
}
