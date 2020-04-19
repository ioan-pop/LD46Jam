using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemyNavMeshAgent;

    public Material followerMaterial;

    public Color primaryColor;
    public Color secondaryColor;
    public GameObject enemyModel;

    public Transform targetTransform;
    public GameObject banner;

    private GameObject[] villages;
    private List<GameObject> followerInRange = new List<GameObject>();

    private float canConvertTimer;
    private float canPlaceBannerTimer;

    private bool destroyEnemyBanner;

    private float timer;
    private float timerTillBannerSpawn;

    private float timerTillCanSpread;

    private float wanderTimer;

    private float wanderRadius;

    private IEnumerator spawnFlag;
    private IEnumerator spreadReligion;

    private bool canSpread;
    /*private IEnumerator coroutine;*/

    private Material[] enemyMaterials;

    private void Awake() {
        villages = GameObject.FindGameObjectsWithTag("village");
        wanderTimer = 0f;
        wanderRadius = 10f;
        canSpread = true;
        canConvertTimer = 0f;
        canPlaceBannerTimer = GenerateNewBannerTimer();
    }

    void Start() {
        enemyMaterials = enemyModel.GetComponent<MeshRenderer>().materials;
        SetEnemyColors();
    }

    // Update is called once per frame
    void Update() {
        if (followerInRange.Count > 0 && canSpread) {
            SpreadReligion();
            canSpread = false;
        }

        Vector3 villagePos = villages[Random.Range(0, villages.Length)].transform.position;
/*        float dist = Vector3.Distance(villagePos, transform.position);


        if (dist < 10f) {
            enemyNavMeshAgent.SetDestination(villagePos);
        } else {*/

            /*timer += Time.deltaTime;*/
            timerTillBannerSpawn += Time.deltaTime;
            timerTillCanSpread += Time.deltaTime;

            if (timerTillBannerSpawn >= canPlaceBannerTimer) {
                DropBanner();
                canPlaceBannerTimer = GenerateNewBannerTimer();
                timerTillBannerSpawn = 0;
            }

            if (timerTillCanSpread >= canConvertTimer) {
                canSpread = true;
                canConvertTimer = GenerateSpreadTimer();
            }

            if (enemyNavMeshAgent.remainingDistance < 2f) {
                /*Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);*/
                enemyNavMeshAgent.SetDestination(GameManager.Instance.GetRandomCenterPoint());
/*                timer = 0;
                wanderTimer = GenerateWanderTimer();*/
            }
        /*}*/
    }

    private void SetEnemyColors() {
        // Robe color
        enemyMaterials[0].color = primaryColor;
        // Cape color
        enemyMaterials[4].color = new Color(primaryColor.r - 0.3f, primaryColor.g - 0.3f, primaryColor.b - 0.3f, 1);
        // Secondary color
        enemyMaterials[1].color = secondaryColor;
        // Skin color
        // enemyMaterials[2].color = PlayerDetailsManager.instance.skinColor;
    }

/*    private IEnumerator SpawnBanner(float waitTime) {
        while (true) {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
        }
    }*/

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

    private float GenerateWanderTimer() {
        return Random.Range(0, 10.0f);
    }

    private float GenerateNewBannerTimer() {
        return Random.Range(10, 20.0f);
    }

    private float GenerateSpreadTimer() {
        return Random.Range(0, 8f);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void SpreadReligion() {
        foreach (GameObject follower in followerInRange) {
            follower.GetComponent<FollowerController>().FollowNewTarget(transform, followerMaterial, secondaryColor);
        }
    }

    private void DropBanner() {
        Quaternion rotationOfTheParentOfTheParent = transform.rotation;
        /*Instantiate(banner, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 3), Quaternion.identity);*/
        GameObject bannerPlaced = Instantiate(banner, transform.position + (transform.forward * 2) + (transform.right * 2), rotationOfTheParentOfTheParent);
        bannerPlaced.GetComponent<Banner>().SetBannerSettings(false, transform, followerMaterial, primaryColor, secondaryColor);
    }

}
