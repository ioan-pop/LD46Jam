using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject instructionsPanel;


    public void StartGame() {
        SceneManager.LoadScene(levelToLoad);
        AudioManager.instance.PlayButtonClick();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void OpenInstructionsPanel() {
        instructionsPanel.SetActive(true);        
        AudioManager.instance.PlayButtonClick();
    }
    
    public void CloseInstructionsPanel() {
        instructionsPanel.SetActive(false);
        AudioManager.instance.PlayButtonClick();
    }
}
