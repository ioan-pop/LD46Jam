using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : MonoBehaviour
{
    public NavMeshAgent followerNavMeshAgent;
    
    public Transform targetTransform;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, targetTransform.position) < 3.5f) {
            followerNavMeshAgent.ResetPath();
        } else {
            followerNavMeshAgent.destination = targetTransform.position;
        }
    }
}
