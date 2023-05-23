using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Audio Placeholders")]
    [SerializeField] private AudioSource sfxBreathing;
    [SerializeField] private AudioSource movementSource;
    [SerializeField] private AudioClip sfxPlayerWalking;
    [SerializeField] private AudioClip sfxPlayerRunning;

    PlayerMovement movementScript;
    MouseLook mouseScript;

    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        mouseScript = GetComponentInChildren<MouseLook>();
    }

    public void StopBreathingSFX()
    {
        sfxBreathing.Stop();
        sfxBreathing.loop = false;
    }

    public void KeepBreathingSFX()
    {
        sfxBreathing.Play();
        sfxBreathing.loop = true;
    }

    public void DisableMovement()
    {
        movementScript.CanMove = false;
    }

    public void DisableLook()
    {
        mouseScript.CanLook = false;
    }

    public void EnableMovement()
    {
        movementScript.CanMove = true;
    }

    public void EnableLook()
    {
        mouseScript.CanLook = true; 
    }

    public void DisableMovementAndLook()
    {
        movementScript.CanMove = false;
        mouseScript.CanLook = false;
    }

    public void EnableMovementAndLook()
    {
        movementScript.CanMove = true;
        mouseScript.CanLook = true;
    }

    public void TeleportPlayerTo(Transform location)
    {
        DisableMovementAndLook();
        StartCoroutine(Teleport(location));
    }

    IEnumerator Teleport(Transform location)
    {
        Vector3 destination = new Vector3(location.position.x, location.position.y, location.position.z);
        gameObject.transform.position = destination;
        Debug.Log(gameObject.transform.position);
        yield return new WaitForSeconds(0.1f);
        EnableMovementAndLook();

    }

    public void SwitchRunningAudio()
    {
        movementSource.clip = sfxPlayerRunning;
    }

    public void SwitchWalkingAudio()
    {
        movementSource.clip = sfxPlayerWalking;
    }
}
