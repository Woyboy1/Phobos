using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Raycasting Neccesities")]
    [SerializeField] private float distanceReach = 5.0f;
    [SerializeField] private LayerMask interactableMask;

    [Header("Flashlight")]
    [SerializeField] private GameObject flashlightObject;

    [Header("Audio")]
    [SerializeField] private AudioClip sfxFlashlight;
    [SerializeField] private AudioClip sfxDoorBudge;

    private bool flashlightActive = true;
    private Camera cam;
    private AudioSource source;
    private bool hasKey = false;
    void Start()
    {
        cam = Camera.main;
        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        InteractableFinder();
        ToggleFlashlight();
    }

    void InteractableFinder()
    {
        RaycastHit hit;
        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distanceReach, interactableMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetStringID(hit);
            }
        }
    }


    void GetStringID(RaycastHit hit)
    {
        Interactable interactableObject = hit.collider.GetComponent<Interactable>();
        string targetID = interactableObject.InteractableID;

        switch (targetID)
        {
            case "Door":
                // open the door
                // get the component called door inheriting from the interactable class and call the open method
                interactableObject.GetComponent<Door>().OpenDoor();
                break;
            case "Key":
                // pick up the Key
                interactableObject.GetComponent<Key>().PickUpKey();
                hasKey = true;
                break;
            case "LockedDoor":
                if (hasKey)
                {
                    interactableObject.GetComponent<LockedDoor>().OpenDoor(hasKey);
                } else
                {
                    source.PlayOneShot(sfxDoorBudge);
                }
                break;
            default:
                Debug.LogWarning("PlayerInteractions: Cannot find ID or is object not assigned correct ID?");
                break;

        }
    }

    void ToggleFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlightActive)
            {
                FlashlightOff();
                flashlightActive = false;
                return;
            }

            flashlightObject.SetActive(true);
            source.PlayOneShot(sfxFlashlight);
            flashlightActive = true;
        }
    }

    void FlashlightOff()
    {
        if (flashlightObject != true)
        {
            flashlightObject.SetActive(false);
        }
        source.PlayOneShot(sfxFlashlight);
    }
}
