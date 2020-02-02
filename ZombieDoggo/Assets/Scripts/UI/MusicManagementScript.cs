﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagementScript : MonoBehaviour
{
    private static MusicManagementScript instance = null;
    public static MusicManagementScript Instance { get { return instance; } }

     
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
