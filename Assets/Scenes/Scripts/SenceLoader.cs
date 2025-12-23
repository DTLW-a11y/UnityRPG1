using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceLoader : MonoBehaviour
{
    public static SenceLoader Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Init()//初始化加载场景
    {
        SceneManager.LoadScene("InitScene");//全局单例，不销毁
        SceneManager.LoadScene("LXHSampleScene");//第一个场景
    }
}
