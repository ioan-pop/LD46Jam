using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndgameUIController : MonoBehaviour
{
    public Text victoryDefeatText;
    public Text playerCountText;
    public Text enemyCountText;

    void Start()
    {
        if(GameManager.Instance.GetPlayerFollowers() >= GameManager.Instance.GetEnemyFollowers()) {
            victoryDefeatText.text = "Victory!";
        } else {
            victoryDefeatText.text = "Defeat!";
        }
        playerCountText.text = GameManager.Instance.GetPlayerFollowers().ToString(); 
        enemyCountText.text = GameManager.Instance.GetEnemyFollowers().ToString(); 
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void MainMenu() {
        AudioManager.instance.PlayButtonClick();
        SceneManager.LoadScene(0);
    }
}
