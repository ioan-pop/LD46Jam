﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius;

    public GameObject follower;

    public int maxFollowerCount;

    private int totalFollowerSpawned;
    private List<Vector3> followerPositions = new List<Vector3>();
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        while (totalFollowerSpawned < maxFollowerCount) {
            GameObject newFollower = Instantiate(follower, RandPos(), Quaternion.identity);
            GameManager.Instance.NewFollower(newFollower);
            totalFollowerSpawned++;
        }
    }

    Vector3 RandPos() {
        Vector3 pos = new Vector3(transform.position.x + Random.Range(-(radius/2f), (radius/2)), 1, transform.position.z + Random.Range(-(radius / 2f), (radius / 2)));
/*        if (followerPositions.Contains(pos)) {
            do {
                pos = new Vector3(Random.Range(-(radius / 2f), (radius / 2)), 1, Random.Range(-(radius / 2f), (radius / 2)));
            } while (!followerPositions.Contains(pos));
            followerPositions.Add(pos);
        }*/
        return pos;
    }

    void OnDrawGizmos() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
