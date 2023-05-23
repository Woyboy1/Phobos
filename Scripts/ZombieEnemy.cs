using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieEnemy : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField] private float speedUpDistance = 15.0f;
    [SerializeField] private float refreshTime = 0.3f;

    [Header("NavMesh Agent")]
    [SerializeField] private float runningSpeed = 4.2f;
    [SerializeField] private float walkingSpeed = 1.0f;

    [Header("Audio")]
    [SerializeField] private float audioTimer = 4.0f;
    [SerializeField] private AudioClip[] sfxGroans;

    private NavMeshAgent agent;
    Animator animator;
    Transform target;
    AudioSource source;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = FindAnyObjectByType<Player>().gameObject.transform;
        source = GetComponent<AudioSource>();

        ApplyDefaultSettings();

        StartCoroutine(RefreshPath());
        StartCoroutine(PlaySound());
    }

    #region Audio

    public IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(audioTimer);
        PlayAudio();
        StartCoroutine(PlaySound());
    }

    private void PlayAudio()
    {
        int randomIndex = Random.Range(0, sfxGroans.Length);
        source.PlayOneShot(sfxGroans[randomIndex]);
    }

    #endregion

    #region Pathfinding
    public IEnumerator RefreshPath()
    {
        CheckDistance();
        animator.SetTrigger("iswalking");
        agent.SetDestination(target.transform.position);
        yield return new WaitForSeconds(refreshTime);
        StartCoroutine(RefreshPath());
    }

    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, target.transform.position) >= speedUpDistance)
        {
            StartCoroutine(Run());
        }
    }

    IEnumerator Run()
    {
        ApplyRunningSpeed();
        yield return new WaitForSeconds(3.0f);
        ApplyDefaultSettings();
    }

    #endregion

    #region NavMesh Agent Properties

    void ApplyDefaultSettings()
    {
        agent.speed = walkingSpeed;
    }

    void ApplyRunningSpeed()
    {
        agent.speed = runningSpeed;
    }

    #endregion

    #region Collision

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Door>()) { other.GetComponent<Door>().OpenDoor(); }

        if (other.CompareTag("Player")) // Finds player
        {
            GameHandling.instance.PlayerDiesToZombie();
            Destroy(this.gameObject);
        }
    }

    #endregion
}
