using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : MonoBehaviour {
    public GameObject bannerPlacementFX;
    public AudioClip bannerPlacement;

    private Transform player;
    private FollowerController followerController;
    private Material followerMaterial;
    private AudioSource audioSource;
    private bool isPlayer;

    private Color bannerPrimaryColor;
    private Color bannerSecondaryColor;

    void Start() {
        Instantiate(bannerPlacementFX, transform.position, Quaternion.identity);
        /*audioSource.PlayOneShot(bannerPlacement);*/
    }

    public void SetBannerSettings(bool playCheck, Transform playerPlace, Material material, Color primaryColor, Color secondaryColor) {
        isPlayer = playCheck;
        player = playerPlace;
        followerMaterial = material;
        Material[] bannerMaterials = gameObject.GetComponentInChildren<MeshRenderer>().materials;
        bannerMaterials[2].color = primaryColor;
        bannerMaterials[0].color = secondaryColor;
        bannerPrimaryColor = primaryColor;
        bannerSecondaryColor = secondaryColor;
    }

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.CompareTag("follower")) {
            followerController = c.gameObject.GetComponent<FollowerController>();
            followerController.SetFollowerMaterial(followerMaterial);
            followerController.isPlayerFollower = isPlayer;
            followerController.PrayAtBanner(transform, player, isPlayer, bannerSecondaryColor);
            Debug.Log(isPlayer);
        }
    }
}
