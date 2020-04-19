using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetailsManager : MonoBehaviour
{
    public static PlayerDetailsManager instance;

    void Awake() {
        if (PlayerDetailsManager.instance != null) {
            Destroy(gameObject);
        }
        instance = this;
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public Color primaryColor;
    public Color darkPrimaryColor;
    public Color secondaryColor;
    public Color skinColor;
    public string religionName;
}
