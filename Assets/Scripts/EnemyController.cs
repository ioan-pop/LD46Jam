using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemyNavMeshAgent;
    
    public Transform targetTransform;

    // Update is called once per frame
    void Update()
    {
        enemyNavMeshAgent.destination = targetTransform.position;
    }
}
