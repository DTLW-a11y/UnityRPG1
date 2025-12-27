using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;

    public Player player;
    public GameObject playerentity;

    public int currency;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void RefreshSceneReferences()
    {
        // 在新场景中查找Player对象
        playerentity = GameObject.Find("player");
        player = playerentity.GetComponent<Player>();
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshSceneReferences();
    }

    public int Getcurrency() => currency;

    public void LoadData(GameData _data)
    {
        this.currency = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency =this.currency;
    }
}
