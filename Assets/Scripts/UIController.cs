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
    public GameObject fadeScreen;
    public Text scrtollScreenReligionNameText;

    private bool escapeMenuIsOpen = false;
    private bool fadeScreenDisabled = false;
    private float scrollTimer = 10f;
    private RectTransform fadeScreenTransform;

    void Start() {
        if (PlayerDetailsManager.instance != null) {
            religionNameText.text = PlayerDetailsManager.instance.religionName;
            scrtollScreenReligionNameText.text = PlayerDetailsManager.instance.religionName;
        }
        fadeScreenTransform = fadeScreen.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        SetFollowerCounters();
        SetGameTime();
        HandleEscapeMenu();
        if (!fadeScreenDisabled) {
            HandleFadeScreen();
        }
    }

    private void SetFollowerCounters() {
        totalCounter.text = GameManager.Instance.GetTotalFollowers().ToString();
        enemyCounter.text = GameManager.Instance.GetEnemyFollowers().ToString();
        playerCounter.text = GameManager.Instance.GetPlayerFollowers().ToString();
    }

    private void SetGameTime() {
        if (TimeManager.instance != null) {
            gameTimeCounter.text = ((int)TimeManager.instance.gameTimeInSeconds).ToString();
        } else {
            gameTimeCounter.text = "???";
        }
    }

    private void HandleEscapeMenu() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleEscapeMenu();
        }
    }

    private void ToggleEscapeMenu() {
        if (escapeMenuIsOpen) {
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
        Time.timeScale = 1f;
        AudioManager.instance.PlayButtonClick();
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Time.timeScale = 1f;
        AudioManager.instance.PlayButtonClick();
        Application.Quit();
    }

    private void HandleFadeScreen() {
        scrollTimer -= Time.deltaTime;
        fadeScreenTransform.localPosition += Vector3.up * Time.deltaTime * 500f;
        if(scrollTimer <= 0) {
            fadeScreenDisabled = true;
            Destroy(fadeScreen);
        }
    }
}
