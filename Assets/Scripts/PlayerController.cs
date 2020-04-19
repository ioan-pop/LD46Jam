﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    public Camera followCamera;
    public bool isClickMovement;
    public bool enableCameraMouseRotation;
    public NavMeshAgent playerNavMeshAgent;
    public CharacterController characterController;
    public Animator playerAnimator;
    public GameObject playerModel;
    public AudioClip sound_spreadReligion;

    [Header("Follower")]
    public GameObject banner;
    public Material followerMaterial;

    private List<GameObject> followerInRange = new List<GameObject>();
    private List<GameObject> bannersInRange = new List<GameObject>();

    private bool destoryingBanner = false;

    private bool canSpreadReligion = false;
    private bool canDropBanner = false;

    private bool canDoAction = false;

    private float posX;
    private float posZ;
    private Material[] playerMaterials;
    private Color primaryColor;
    private Color secondaryColor;
    private AudioSource audioSource;

    private float actionTimer = 0f;
    private float actionCoolDown = 2f;

    private float timerTillDestroyedBanner = 0f;
    private float timeToDestroyBanner = 2f;

    void Start() {
        playerMaterials = playerModel.GetComponent<MeshRenderer>().materials;
        SetPlayerColors();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        HandleMouse();
        HandleBannerActions();
        HandleActionTimer();
        HandleSpreadReligion();
        UpdateCamera();
        HandleAnimation();
    }

    private void SetPlayerColors() {
        if(PlayerDetailsManager.instance != null) {
            primaryColor = PlayerDetailsManager.instance.primaryColor;
            secondaryColor = PlayerDetailsManager.instance.secondaryColor;
            // Robe color
            playerMaterials[0].color = PlayerDetailsManager.instance.primaryColor;
            // Cape color
            playerMaterials[4].color = PlayerDetailsManager.instance.darkPrimaryColor;
            // Secondary color
            playerMaterials[1].color = PlayerDetailsManager.instance.secondaryColor;
            // Skin color
            playerMaterials[2].color = PlayerDetailsManager.instance.skinColor;
        } else {
            primaryColor = Color.black;
            secondaryColor = Color.blue;
        }
    }
    
    private void HandleMouse() {
        if (!isClickMovement) {
            posX = Input.GetAxis("Horizontal");
            posZ = Input.GetAxis("Vertical");
            Vector3 movePlayer = transform.right * posX + transform.forward * posZ;
            characterController.SimpleMove(movePlayer * 5f);
        } else {
            if (!destoryingBanner) {
                if (Input.GetMouseButton(1)) {
                    RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

                    foreach (RaycastHit hit in hits) {
                        // TODO: Rethink getting component of each hit.
                        // Alternatively, could use 'hit.transform.name', but that has it's own problems
                        var terrain = hit.transform.GetComponent<Terrain>();
                        if (terrain != null) {
                            playerNavMeshAgent.destination = hit.point;
                        }
                    }
                }
            }
            if (Input.GetMouseButton(1)) {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

                foreach (RaycastHit hit in hits) {
                    // TODO: Rethink getting component of each hit.
                    // Alternatively, could use 'hit.transform.name', but that has it's own problems
                    if (hit.transform.tag == "terrain") {
                        playerNavMeshAgent.destination = hit.point;
                    }
                    if ( hit.transform.tag == "follower" ) {
                        // Debug.Log("hit a follower");
                        playerNavMeshAgent.destination = hit.point;
                    }
                }
            } 
        }
    }

    private void HandleBannerActions() {
        if (Input.GetKeyDown(KeyCode.E) && canDoAction) {
            DropBanner();
        }
        if (Input.GetKey(KeyCode.Q)) {
            DestroyBanner();
        } else if (Input.GetKeyUp(KeyCode.Q)) {
            timerTillDestroyedBanner = 0f;
            destoryingBanner = false;
        }
    }

    private void HandleActionTimer() {
        actionTimer += Time.deltaTime;
        if (actionTimer >= actionCoolDown) {
            canDoAction = true;
            actionTimer = 0f;
        }
    }

    private void HandleSpreadReligion() {
        if (Input.GetKeyDown(KeyCode.Space) && canDoAction) {
            SpreadReligion();
        }
    }

    private void UpdateCamera() {
        followCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z - 10f);
    }

    private void HandleAnimation() {
        if(Vector3.Distance(transform.position, playerNavMeshAgent.destination) > 0.5f) {
            playerAnimator.SetFloat("Run", 1);
        } else {
            playerAnimator.SetFloat("Run", 0);
        }
    }

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.CompareTag("follower")) {
            if (!followerInRange.Contains(c.gameObject)) {
                followerInRange.Add(c.gameObject);
            }
        }
        if (c.gameObject.CompareTag("banner")) {
            if (!bannersInRange.Contains(c.gameObject)) {
                bannersInRange.Add(c.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider c) {
        if (c.gameObject.CompareTag("follower")) {
            if (followerInRange.Contains(c.gameObject)) {
                followerInRange.Remove(c.gameObject);
            }
        }
        if (c.gameObject.CompareTag("banner")) {
            if (bannersInRange.Contains(c.gameObject)) {
                bannersInRange.Remove(c.gameObject);
            }
        }
    }

    private void DropBanner() {
        Quaternion rotationOfTheParentOfTheParent = transform.rotation;
        /*Instantiate(banner, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 3), Quaternion.identity);*/
        GameObject bannerPlaced = Instantiate(banner, transform.position + (transform.forward * 2) + (transform.right * 2), rotationOfTheParentOfTheParent);
        bannerPlaced.GetComponent<Banner>().SetBannerSettings(transform, followerMaterial, primaryColor, secondaryColor);
    }

    private void DestroyBanner() {
        timerTillDestroyedBanner += Time.deltaTime;
        if (timerTillDestroyedBanner >= timeToDestroyBanner) {
            GameObject destroyedBanner = bannersInRange[0];
            bannersInRange.RemoveAt(0);
            Destroy(destroyedBanner);
        }
        if (!destoryingBanner) {
            destoryingBanner = true;
        }
        playerNavMeshAgent.SetDestination(bannersInRange[0].transform.position);
        transform.LookAt(bannersInRange[0].transform);
        
    }

    private void SpreadReligion() {
        audioSource.PlayOneShot(sound_spreadReligion);
        foreach (GameObject follower in followerInRange) {
            follower.GetComponent<FollowerController>().FollowNewTarget(transform, followerMaterial);
        }
    }

    private Ray GetMouseRay() {
        return followCamera.ScreenPointToRay(Input.mousePosition);
    }
}
