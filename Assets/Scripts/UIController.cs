using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Text gameTimeCounter;
    public Text totalCounter;
    public Text enemyCounter;
    public Text playerCounter;
    public Text religionNameText;
    public GameObject escapeMenu;

    private bool escapeMenuIsOpen = false;

    void Start() {
        if (PlayerDetailsManager.instance != null) {
            religionNameText.text = PlayerDetailsManager.instance.religionName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetFollowerCounters();
        SetGameTime();
        HandleEscapeMenu();
    }

    private void SetFollowerCounters()
    {
        totalCounter.text = GameManager.Instance.GetTotalFollowers().ToString();
        enemyCounter.text = GameManager.Instance.GetEnemyFollowers().ToString();
        playerCounter.text = GameManager.Instance.GetPlayerFollowers().ToString();
    }

    private void SetGameTime()
    {
        if (TimeManager.instance != null)
        {
            gameTimeCounter.text = ((int)TimeManager.instance.gameTimeInSeconds).ToString();
        }
        else
        {
            gameTimeCounter.text = "???";
        }
    }

    private void HandleEscapeMenu() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleEscapeMenu();
        }
    }

    private void ToggleEscapeMenu()
    {
        if (escapeMenuIsOpen)
        {
            escapeMenuIsOpen = false;
            escapeMenu.SetActive(false);
            Time.timeScale = 1f;
        } else {
            escapeMenuIsOpen = true;
            escapeMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ContinueGame() {
        AudioManager.instance.PlayButtonClick();
        ToggleEscapeMenu();
    }

    public void GoToMainMenu() {
        AudioManager.instance.PlayButtonClick();
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        AudioManager.instance.PlayButtonClick();
        Application.Quit();
    }
}
