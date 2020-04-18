using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : MonoBehaviour {

    private Transform player;
    private FollowerController followerController;
    private Material followerMaterial;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetPlayerWhoPlace(Transform playerPlace, Material material) {
        player = playerPlace;
        followerMaterial = material;
    }

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.CompareTag("follower")) {
            followerController = c.gameObject.GetComponent<FollowerController>();
            followerController.SetFollowerMaterial(followerMaterial);
            followerController.PrayAtBanner(transform, player);
        }
    }
}
