﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public bool isClickMovement;

    public Material followerMaterial;

    public NavMeshAgent playerNavMeshAgent;
    public CharacterController characterController;

    public GameObject banner;

    private List<GameObject> followerInRange = new List<GameObject>();


    private float posX;
    private float posZ;



    void Update() {
        HandleMouse();
    }
    
    private void HandleMouse()
    {
        if (!isClickMovement) {
            posX = Input.GetAxis("Horizontal");
            posZ = Input.GetAxis("Vertical");

            Vector3 movePlayer = transform.right * posX + transform.forward * posZ;
            characterController.SimpleMove(movePlayer * 5f);
        } else {
            if (Input.GetMouseButton(1)) {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

                foreach (RaycastHit hit in hits) {
                    // TODO: Rethink getting component of each hit.
                    // Alternatively, could use 'hit.transform.name', but that has it's own problems
                    var terrain = hit.transform.GetComponent<Terrain>();
                    if (terrain != null) {
                        playerNavMeshAgent.destination = hit.point;
                    }
                    if ( hit.transform.tag == "follower" ) {
                        Debug.Log("hit a follower");
                        playerNavMeshAgent.destination = hit.point;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                SpreadReligion();
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                DropBanner();
            }
        }
    }

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.CompareTag("follower")) {
            if (!followerInRange.Contains(c.gameObject)) {
                followerInRange.Add(c.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider c) {
        if (c.gameObject.CompareTag("follower")) {
            if (followerInRange.Contains(c.gameObject)) {
                followerInRange.Remove(c.gameObject);
            }
        }
    }

    private void DropBanner() {
        Quaternion rotationOfTheParentOfTheParent = transform.rotation;
        /*Instantiate(banner, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 3), Quaternion.identity);*/
        GameObject bannerPlaced = Instantiate(banner, transform.position + (transform.forward * 2) + (transform.right * 2), rotationOfTheParentOfTheParent);
        bannerPlaced.GetComponent<Banner>().SetPlayerWhoPlace(transform, followerMaterial);
    }

    private void SpreadReligion() {
        foreach (GameObject follower in followerInRange) {
            follower.GetComponent<FollowerController>().FollowNewTarget(transform, followerMaterial);
        }
    }

    private Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
