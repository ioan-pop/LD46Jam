using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text religionNameText;

    void Start() {
        if(PlayerDetailsManager.instance != null) {
            religionNameText.text = PlayerDetailsManager.instance.religionName;
        }
    }
}
