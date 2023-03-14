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
    [SerializeField] private float startingBattery = 60f;

    [Header("Audio")]
    [SerializeField] private AudioClip sfxFlashlight;
    [SerializeField] private AudioClip sfxDoorBudge;

    [Header("Outline Text")]
    [SerializeField] private GameObject interactionText;

    private bool flashlightActive = true;
    private Camera cam;
    private AudioSource source;
    private bool hasKey = false;
    private bool hasBattery = true;
    void Start()
    {
        cam = Camera.main;
        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        InteractableFinder();
        ToggleFlashlight();

        
        if (hasBattery)
        {
            if (flashlightActive)
            {
                DrainBattery();
            }
        }
    }

    void InteractableFinder()
    {
        RaycastHit hit;
        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distanceReach, interactableMask))
        {
            EnableText();
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetStringID(hit);
            }
        }
        else
        {
            DisableText();
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
                }
                else
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
        if (hasBattery)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (flashlightActive)
                {
                    FlashlightOff();
                    return;
                }

                flashlightObject.SetActive(true);
                source.PlayOneShot(sfxFlashlight);
                flashlightActive = true;
            }
        }
    }

    void FlashlightOff()
    {
        flashlightActive = false;
        flashlightObject.SetActive(false);
        source.PlayOneShot(sfxFlashlight);
    }

    void DrainBattery()
    {
        startingBattery -= 1 * Time.deltaTime;
        Debug.Log("Battery - " + startingBattery);

        if (startingBattery <= 0)
        {
            hasBattery = false;
            FlashlightOff();
        }
    }

    public void RechargeBatter(float value)
    {
        if (value > 100)
        {
            value = 100;
        } else if (value < 0)
        {
            Debug.LogWarning("Cannot recharge battery less than 0, is this a mistake?");
        }

        hasBattery = true;
        startingBattery += value;
    }

    public void EnableText()
    {
        interactionText.SetActive(true);
    }

    public void DisableText()
    {
        interactionText.SetActive(false);
    }
}
