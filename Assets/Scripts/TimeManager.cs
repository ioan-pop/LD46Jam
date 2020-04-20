using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public float gameTimeInSeconds = 180;
    public string endScene;

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
        if(gameTimeInSeconds <= 0f) {
            SceneManager.LoadScene(endScene);
        }
    }
}
