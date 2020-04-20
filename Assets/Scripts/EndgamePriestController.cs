using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgamePriestController : MonoBehaviour
{
    public GameObject playerModel;

    private Material[] playerMaterials;

    // Start is called before the first frame update
    void Start()
    {
        playerMaterials = playerModel.GetComponent<MeshRenderer>().materials;
        SetPlayerColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPlayerColors() {
        if(PlayerDetailsManager.instance != null) {
            // Robe color
            playerMaterials[0].color = PlayerDetailsManager.instance.primaryColor;
            // Cape color
            playerMaterials[4].color = PlayerDetailsManager.instance.darkPrimaryColor;
            // Secondary color
            playerMaterials[1].color = PlayerDetailsManager.instance.secondaryColor;
            // Skin color
            playerMaterials[2].color = PlayerDetailsManager.instance.skinColor;
        }
    }
}
