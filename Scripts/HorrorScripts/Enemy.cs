using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{


    [Header("Enemy Behavior")]
    [SerializeField] private float raycastingDistance = 5.0f;
    [SerializeField] private float yMultiplier = 0.7f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float chaseTimer = 12.0f;

    [Header("PathFinding")]
    [SerializeField] private Transform[] patrolPoints;

    [Header("General")]
    [SerializeField] private float refreshSync = 0.3f;
    [SerializeField] private float chasingSpeed = 6.54f;
    [SerializeField] private float patrolSpeed = 3.0f;

    [Header("Audio Names")]
    [SerializeField] private AudioSource sfxWalking;
    [SerializeField] private AudioSource sfxRunning; // an audio source from a separate game object
    [SerializeField] private AudioClip sfxLaugh;

    // Private Properties
    Animation currentAnimation;
    AudioSource source;
    NavMeshAgent pathFinder;
    Transform target;
    private bool canLaugh = true;
    private bool foundTarget = true;

    // Pathfinding
    public bool isPatroling = true;
    private int destPoint = 0;

    // Get and Set
    public bool FoundTarget
    {
        get { return foundTarget; }
        set { foundTarget = value; }
    }

    void Start()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        source = GetComponent<AudioSource>();
        currentAnimation = GetComponent<Animation>();

        pathFinder.updateRotation = false;
        sfxRunning.Stop();
        sfxWalking.Play();

        ApplyDefaultSettings();
        GoToNextPoint();
        StartCoroutine(LaughTimer());
    }
    void Update()
    {
        // Movement and Rotation
        FaceTarget();
        AdjustingAgentProperties();
        CheckDestination();

        // Interation
        ScanningRaycast();
    }

    #region Raycasting to find target 

    private void ScanningRaycast()
    {
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;
        direction.y = yMultiplier;
        Ray ray = new Ray(transform.position, direction * raycastingDistance);
        Vector3 start = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

        Debug.DrawRay(start, direction * raycastingDistance);

        if (Physics.Raycast(ray, out hit, mask))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                StartCoroutine(ChaseTimer()); // Execute Chase Sequence
            } 
        } 
    }

    IEnumerator ChaseTimer()
    {
        isPatroling = false;
        sfxRunning.Play();
        sfxWalking.Stop();
        yield return new WaitForSeconds(chaseTimer);
        sfxRunning.Stop();
        sfxWalking.Play();
        isPatroling = true;
    }

    #endregion

    #region Adjust Navmesh Agent Properties

    void ApplyDefaultSettings()
    {
        pathFinder.speed = patrolSpeed;
    }

    void ApplyChasingSpeed()
    {
        pathFinder.speed = chasingSpeed;
    }

    #endregion

    #region Movement and Rotation

    void CheckDestination()
    {
        if (isPatroling)
        {
            if (!pathFinder.pathPending && pathFinder.remainingDistance < 0.5f)
            {
                GoToNextPoint();
                return;
            }
        }
        else if (!isPatroling)
        {
            if (!pathFinder.pathPending)
            {
                StartCoroutine(RefreshPath());
            }
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) { return; }

        // Set the agent to go to the currently selected destination
        pathFinder.destination = patrolPoints[destPoint].position;

        // Choose the next point in the array as the destination
        // Cycling to the start if necessary.
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    private void AdjustingAgentProperties()
    {
        if (isPatroling) // Doesn't see the player
        {
            ApplyDefaultSettings();
            currentAnimation.Play("Walk");
        }
        else if (!isPatroling) // Sees the player and chases
        {
            ApplyChasingSpeed();
            currentAnimation.Play("Run");
        }
    }

    IEnumerator RefreshPath()
    {
        while (!isPatroling)
        {
            pathFinder.destination = target.position;
            // pathFinder.SetDestination(target.position);
            yield return new WaitForSeconds(refreshSync);
            // StartCoroutine(RefreshPath());
        }

        GoToNextPoint();
    }

    void FaceTarget()
    {
        if (pathFinder.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(pathFinder.velocity.normalized);
        }
    }
    #endregion

    #region Audio
    public void PlayLaugh()
    {
        source.PlayOneShot(sfxLaugh);
    }

    #endregion

    #region Couroutines 

    IEnumerator LaughTimer()
    {
        float refreshTime = 6.5f;

        while (canLaugh)
        {
            PlayLaugh();
            yield return new WaitForSeconds(refreshTime);
        }
    }
    #endregion

    #region Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call method for the player to die and destroy the enemy object
            GameHandling.instance.PlayerDiesToGhoul();

            StopCoroutine(LaughTimer());
            Destroy(this.gameObject);
        }

        if (other.GetComponent<Door>())
        {
            other.GetComponent<Door>().OpenDoor();
        }

        if (other.gameObject.CompareTag("Barricade"))
        {
            other.GetComponent<Barricade>().AIDestroyBarricade();
        }
    }
    #endregion
}
