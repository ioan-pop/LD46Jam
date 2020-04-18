using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : MonoBehaviour
{
    public NavMeshAgent followerNavMeshAgent;
    public bool isFollowing;

    [SerializeField]
    private Transform targetTransform;

    private void Awake() {
        isFollowing = false;
    }

    // Update is called once per frame
    void Update()
    {
/*        if (isFollowing) {
            if (Vector3.Distance(transform.position, targetTransform.position) < 3.5f) {
                followerNavMeshAgent.ResetPath();
            } else {
                followerNavMeshAgent.destination = targetTransform.position;
            }
        }*/
    }

    public void FollowNewTarget() {
        // set target
        isFollowing = true;
    }

    private void FollowerWander () {

    }

    private void FollowerPath () {

    }


}
