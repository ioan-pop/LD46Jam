using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCreateController : MonoBehaviour
{
    public Color[] PrimaryColors;
    public Color[] SecondaryColors;
    public Color[] SkinColors;

    public Image primaryColorImage;
    public Image secondaryColorImage;
    public Image skinColorImage;

    public Text religionName;

    public GameObject priestModel;
    public GameObject bannerModel;

    public string levelToLoad;

    private int primaryColorIndex = 0;
    private int secondaryColorIndex = 0;
    private int skinColorIndex = 0;

    private Material[] playerMaterials;
    private Material[] bannerMaterials;


    // Start is called before the first frame update
    void Start()
    {
        playerMaterials = priestModel.GetComponent<MeshRenderer>().materials;
        bannerMaterials = bannerModel.GetComponent<MeshRenderer>().materials;

        SetPrimaryColor();
        SetSecondaryColor();
        SetSkinColor();
    }

    public void NextPrimaryColor() {
        primaryColorIndex++;
        if(primaryColorIndex > PrimaryColors.Length - 1) {
            primaryColorIndex = 0;
        }
        SetPrimaryColor();
    }

    public void PreviousPrimaryColor() {
        primaryColorIndex--;
        if(primaryColorIndex < 0) {
            primaryColorIndex = PrimaryColors.Length - 1;
        }
        SetPrimaryColor();
    }

    private void SetPrimaryColor() {
        primaryColorImage.color = PrimaryColors[primaryColorIndex];
        playerMaterials[0].color = PrimaryColors[primaryColorIndex];
        playerMaterials[4].color = new Color(PrimaryColors[primaryColorIndex].r - 0.3f, PrimaryColors[primaryColorIndex].g - 0.3f, PrimaryColors[primaryColorIndex].b - 0.3f, 1);
        bannerMaterials[2].color = PrimaryColors[primaryColorIndex];
    }

    public void NextSecondaryColor() {
        secondaryColorIndex++;
        if(secondaryColorIndex > SecondaryColors.Length - 1) {
            secondaryColorIndex = 0;
        }
        SetSecondaryColor();
    }

    public void PreviousSecondaryColor() {
        secondaryColorIndex--;
        if(secondaryColorIndex < 0) {
            secondaryColorIndex = SecondaryColors.Length - 1;
        }
        SetSecondaryColor();
    }

    private void SetSecondaryColor() {
        secondaryColorImage.color = SecondaryColors[secondaryColorIndex];
        playerMaterials[1].color = SecondaryColors[secondaryColorIndex];
        bannerMaterials[0].color = SecondaryColors[secondaryColorIndex];
    }

    public void NextSkinColor() {
        skinColorIndex++;
        if(skinColorIndex > SkinColors.Length - 1) {
            skinColorIndex = 0;
        }
        SetSkinColor();
    }

    public void PreviousSkinColor() {
        skinColorIndex--;
        if(skinColorIndex < 0) {
            skinColorIndex = SkinColors.Length - 1;
        }
        SetSkinColor();
    }

    private void SetSkinColor() {
        skinColorImage.color = SkinColors[skinColorIndex];
        playerMaterials[2].color = SkinColors[skinColorIndex];
    }

    public void CompleteCharacterCreation() {
        if(PlayerDetailsManager.instance != null) {
            PlayerDetailsManager.instance.primaryColor = PrimaryColors[primaryColorIndex];
            PlayerDetailsManager.instance.darkPrimaryColor = playerMaterials[4].color;
            PlayerDetailsManager.instance.secondaryColor = SecondaryColors[secondaryColorIndex];
            PlayerDetailsManager.instance.skinColor = SkinColors[skinColorIndex];
            PlayerDetailsManager.instance.religionName = religionName.text;
        }
        SceneManager.LoadScene(levelToLoad);
    }
}
