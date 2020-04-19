using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {

    public Text totalCounter;
    public Text enemyCounter;
    public Text playerCounter;
    // Start is called before the first frame update
    /*    void Start()
        {

        }*/

    // Update is called once per frame
    void Update() {
        totalCounter.text = GameManager.Instance.GetTotalFollowers().ToString();
        enemyCounter.text = GameManager.Instance.GetEnemyFollowers().ToString();
        playerCounter.text = GameManager.Instance.GetPlayerFollowers().ToString();
    }

}
