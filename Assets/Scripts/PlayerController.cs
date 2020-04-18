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
                var terrain = hit.transform.GetComponent<Terrain>();
                if(terrain != null) {
                    print(hit);
                    // Make nav mesh agent a public
                    playerNavMeshAgent.destination = hit.point;

                    DebugRay(hit.point);
                }
            }
        }
    }

    private Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void DebugRay(Vector3 point) {
        Debug.DrawLine(Camera.main.transform.position, point, Color.red, 5f);
        print(point);
    }
}
