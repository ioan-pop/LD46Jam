using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : MonoBehaviour
{
    public NavMeshAgent followerNavMeshAgent;
    public bool isFollowing;

    public Material[] genderColour;
    public MeshRenderer meshRender;
    public GameObject follower;
    

    public float wanderRadius;
    public float speed;

    [SerializeField]
    private Transform targetTransform;

    private Transform targetDestination;
    private GameObject[] villages;
    private GameObject currentVillage;
    private GameObject newVillage;

    private Transform partnerTransform;
    private GameObject partnerFollower;
    private FollowerController partnerController;

    /*    private AnimEnemyController animController;*/
    private SphereCollider partnerCollider;

    private float timer;
    private float wanderTimer;
    private float villageTravelTimer;
    private float giveBirthTimer;
    private float newVillageCountdownTimer;
    private float newPartnerCountdown;

    private float generateFollowerTimer;


    private float timeToBirth;

    private int gender;
    private bool isTravelingToVillage;

    private bool findPartner;
    private bool startBirth;
    private bool spawnNewFollower;


    private void Awake() {
        partnerCollider = GetComponent<SphereCollider>();
        startBirth = false;
        timeToBirth = 15f;

        villages = GameObject.FindGameObjectsWithTag("village");

        gender = Random.Range(0, 2);
        meshRender.material = genderColour[gender];

        isTravelingToVillage = false;
        isFollowing = false;

        wanderTimer = GenerateWanderTimer();
        newVillageCountdownTimer = GenerateVillageTravelTimer();
        newPartnerCountdown = GeneratePartnerTimer();

        if (wanderRadius == 0) {
            wanderRadius = Random.Range(0f,15f);
        }

        if (speed == 0) {
            speed = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing) { // following a preist
            if (Vector3.Distance(transform.position, targetTransform.position) < 3.5f) {
                followerNavMeshAgent.ResetPath();
            } else {
                followerNavMeshAgent.destination = targetTransform.position;
            }
        }  else if (startBirth) {  // give birth stuff
            do {
                generateFollowerTimer += Time.deltaTime;
            } while (generateFollowerTimer < timeToBirth);

            if (spawnNewFollower)
                Instantiate(follower, transform.position, Quaternion.identity);

            startBirth = false;
            generateFollowerTimer = 0f;
            giveBirthTimer = 0f;
            newPartnerCountdown = GeneratePartnerTimer();
            findPartner = false;

        } else { // not following a preist

            // our timers for new events
            giveBirthTimer += Time.deltaTime;
            villageTravelTimer += Time.deltaTime;

           

            if (villageTravelTimer >= newVillageCountdownTimer && !isTravelingToVillage) {
                isTravelingToVillage = true;

                if (currentVillage == null) {

                    currentVillage = villages[Random.Range(0, villages.Length)];
                    newVillage = currentVillage;

                } else {
                    newVillage = villages[Random.Range(0, villages.Length)];

                    if (newVillage == currentVillage) {
                        do {
                            newVillage = villages[Random.Range(0, villages.Length)];
                        } while (newVillage != currentVillage);
                    }
                }

                followerNavMeshAgent.SetDestination(newVillage.transform.position);
            }

            if (!isTravelingToVillage) {

                timer += Time.deltaTime;

                 // get movement velocity and pass it to the animation controller
                /*float movementVelocity = followerNavMeshAgent.velocity.magnitude / followerNavMeshAgent.speed;*/
                /*animController.Move(movementVelocity);*/


                if (timer >= wanderTimer) {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    followerNavMeshAgent.SetDestination(newPos);
                    timer = 0;
                    wanderTimer = GenerateWanderTimer();
                }

            } else if (isTravelingToVillage) {

                if (followerNavMeshAgent.remainingDistance <= 2f) {
                    isTravelingToVillage = false;
                    newVillageCountdownTimer = GenerateVillageTravelTimer();
                    villageTravelTimer = 0f;
                }
            }
        }
    }

    public void FollowNewTarget(Transform newTarget) {
        // set target
        Debug.Log("follower new leader!");
        targetTransform = newTarget;
        isFollowing = true;
        partnerCollider.enabled = false;
    }

    public void SetBirth(bool toSpawnNewFollower) {
        spawnNewFollower = toSpawnNewFollower;
        startBirth = true;
        followerNavMeshAgent.SetDestination(partnerTransform.position);
    }

    private void OnTriggerEnter(Collider c) {
        if (giveBirthTimer >= newPartnerCountdown && !findPartner) {
            if (c.gameObject.CompareTag("follower") && c.gameObject.GetComponent<FollowerController>().gender != gender) {
                partnerFollower = c.gameObject;
                partnerController = partnerFollower.GetComponent<FollowerController>();
                partnerController.findPartner = true;
                findPartner = true;
                partnerController.partnerTransform = transform;
                partnerTransform = partnerController.partnerTransform;
                partnerController.SetBirth(false);
                SetBirth(true);
            }

        }
    }

    

    private void FollowerWander () {

    }

    private void FollowerPath () {

    }

    private float GenerateVillageTravelTimer() {
        return Random.Range(5f, 15f);
    }

    private float GenerateWanderTimer() {
        return Random.Range(0, 10.0f);
    }

    private float GeneratePartnerTimer() {
        return Random.Range(30f, 120f);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


}
