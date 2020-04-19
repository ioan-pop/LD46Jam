using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip buttonClickSFX;

    void Awake() {
        if (AudioManager.instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayButtonClick() {
        audioSource.PlayOneShot(buttonClickSFX);
    }
}
