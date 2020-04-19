using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {

    public Text gameTimeCounter;
    public Text totalCounter;
    public Text enemyCounter;
    public Text playerCounter;
    public Text religionNameText;

    void Start() {
        if (PlayerDetailsManager.instance != null) {
            religionNameText.text = PlayerDetailsManager.instance.religionName;
        }
    }
    // Update is called once per frame
    void Update() {
        totalCounter.text = GameManager.Instance.GetTotalFollowers().ToString();
        enemyCounter.text = GameManager.Instance.GetEnemyFollowers().ToString();
        playerCounter.text = GameManager.Instance.GetPlayerFollowers().ToString();
        if(TimeManager.instance != null) {
            gameTimeCounter.text = ((int)TimeManager.instance.gameTimeInSeconds).ToString();
        } else {
            gameTimeCounter.text = "???";
        }
    }
}
