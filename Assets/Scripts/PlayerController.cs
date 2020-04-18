using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public bool isClickMovement;

    public NavMeshAgent playerNavMeshAgent;
    public CharacterController characterController;

    private float posX;
    private float posZ;

    private List<GameObject> followerInRange = new List<GameObject>();

    void Start()
    {
        
    }

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

    private void SpreadReligion() {
        foreach (GameObject follower in followerInRange) {
            follower.GetComponent<FollowerController>().FollowNewTarget(transform);
        }
    }

    private Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
