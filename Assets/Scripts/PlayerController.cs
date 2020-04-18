using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent playerNavMeshAgent;

    void Start()
    {
        
    }

    void Update() {
        HandleMouse();
    }
    
    private void HandleMouse()
    {
        if (Input.GetMouseButton(1)) {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach(RaycastHit hit in hits) {
                // TODO: Rethink getting component of each hit.
                // Alternatively, could use 'hit.transform.name', but that has it's own problems
                var terrain = hit.transform.GetComponent<Terrain>();
                if(terrain != null) {
                    playerNavMeshAgent.destination = hit.point;
                }
            }
        }
    }

    private Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
