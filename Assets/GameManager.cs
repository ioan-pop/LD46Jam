using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {
    
    public float maxFollowers;

    private static GameManager instance;
    private bool canPopulate;

    private List<GameObject> allFollowers;
    private int playerFollowersCounter;
    private int enemyFollowersCounter;

    private GameObject[] followers;

    private GameManager() {
        canPopulate = true;
        allFollowers = new List<GameObject>();
        maxFollowers = 600;
        playerFollowersCounter = 0;
        enemyFollowersCounter = 0;
    }

    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public void SetFollowers() {
        followers = GameObject.FindGameObjectsWithTag("follower");

        foreach (GameObject follower in followers) {
            allFollowers.Add(follower);
        }
    }

    public Vector3 GetCenterPoint () {
        return FindCenterPoint(allFollowers);
    }

    public Vector3 GetRandomCenterPoint() {
        return FindRandomCenterPoint(allFollowers);
    }

    public void NewFollower(GameObject follower) {
        this.allFollowers.Add(follower);
        if (allFollowers.Count >= maxFollowers) {
            canPopulate = false;
        }
        // Debug.Log(allFollowers.Count);
    }

    public void AddPriestFollower(bool isPlayer) {
        if (isPlayer) {
            this.playerFollowersCounter++;
        } else {
            this.enemyFollowersCounter++;
        }
    }

    public int GetTotalFollowers() {
        return allFollowers.Count;
    }
    public int GetPlayerFollowers() {
        return playerFollowersCounter;
    }
    public int GetEnemyFollowers() {
        return enemyFollowersCounter;
    }

    public bool CanBirth() {
        return canPopulate;
    }

    private Vector3 FindRandomCenterPoint(List<GameObject> followers) {
        if (followers.Count == 0)
            return Vector3.zero;

        if (followers.Count == 1)
            return followers[0].transform.position;

        Bounds bounds = new Bounds(followers[0].transform.position, Vector3.zero);
        int randomStart = Random.Range(1, followers.Count - 1);
        int randomEnd = Random.Range(randomStart, Random.Range(randomStart + 1, followers.Count));
        if (randomEnd <= randomStart) {
            do {
                randomEnd = Random.Range(randomStart, Random.Range(randomStart + 1, followers.Count));
            } while (randomEnd <= randomStart);
        }
        for (var i = randomStart; i < randomEnd; i++)
            if (!followers[i].GetComponent<FollowerController>().isFollowing) {
                bounds.Encapsulate(followers[i].transform.position);
            }
        return bounds.center;
    }

    private Vector3 FindCenterPoint(List<GameObject> followers) {
         if (followers.Count == 0)
             return Vector3.zero;
         if (followers.Count == 1)
             return followers[0].transform.position;
         Bounds bounds = new Bounds(followers[0].transform.position, Vector3.zero);
         for (var i = 1; i < followers.Count; i++)
             bounds.Encapsulate(followers[i].transform.position); 
         return bounds.center;
    }  
}
