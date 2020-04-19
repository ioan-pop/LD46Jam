﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public float gameTimeInSeconds = 180;

    void Awake() {
        if(TimeManager.instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Update()
    {
        gameTimeInSeconds -= Time.deltaTime;
    }
}
