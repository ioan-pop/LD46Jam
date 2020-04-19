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
    
    void Start() {
        Instantiate(bannerPlacementFX, transform.position, Quaternion.identity);
        /*audioSource.PlayOneShot(bannerPlacement);*/
    }

    public void SetBannerSettings(Transform playerPlace, Material material, Color primaryColor, Color secondaryColor) {
        player = playerPlace;
        followerMaterial = material;
        Material[] bannerMaterials = gameObject.GetComponentInChildren<MeshRenderer>().materials;
        bannerMaterials[2].color = primaryColor;
        bannerMaterials[0].color = secondaryColor;
    }

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.CompareTag("follower")) {
            followerController = c.gameObject.GetComponent<FollowerController>();
            followerController.SetFollowerMaterial(followerMaterial);
            followerController.PrayAtBanner(transform, player);
        }
    }
}
