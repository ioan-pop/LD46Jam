using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject instructionsPanel;
    public GameObject settingsPanel;
    public Text movementTypeText;
    // public Toggle enableWASDButton;

    private bool isClickToMove = true;

    public void StartGame() {
        SceneManager.LoadScene(levelToLoad);
        AudioManager.instance.PlayButtonClick();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void OpenSettingsPanel() {
        settingsPanel.SetActive(true);
        AudioManager.instance.PlayButtonClick();
    }

    public void CloseSettingsPanel() {
        settingsPanel.SetActive(false);
        AudioManager.instance.PlayButtonClick();
    }

    public void OpenInstructionsPanel() {
        instructionsPanel.SetActive(true);        
        AudioManager.instance.PlayButtonClick();
    }
    
    public void CloseInstructionsPanel() {
        instructionsPanel.SetActive(false);
        AudioManager.instance.PlayButtonClick();
    }

    public void ToggleMovementSetting() {
        AudioManager.instance.PlayButtonClick();
        isClickToMove = !isClickToMove;
        movementTypeText.text = isClickToMove ? "Mouse Click" : "WASD";
        GameManager.Instance.SetClickMovement(isClickToMove);
    }
}
