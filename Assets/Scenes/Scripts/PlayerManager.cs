using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public Player player;
    public GameObject playerentity;
    public void Awake()
    {
        instance = this;
    }
}

