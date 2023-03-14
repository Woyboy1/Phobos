using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Door : Interactable
{
    [Tooltip("You should change this only if you're making double doors")]
    [SerializeField] private float offsetAngle = -90f;
    [SerializeField] private bool canBeOpen = true;


    [Header("Audio")]
    [SerializeField] AudioClip sfxDoor;

    private bool doorOpen = false;
    private AudioSource source;
    new void Start()
    {
        base.Start();
        source = GetComponent<AudioSource>();
        interactableID = "Door";
    }


    new void Update()
    {
        base.Update();
    }

    public void OpenDoor()
    {
        if (canBeOpen)
        {
            if (doorOpen)
            {
                CloseDoor();
                doorOpen = false;
                return;
            }
            source.PlayOneShot(sfxDoor);
            Vector3 offset = new Vector3(0, offsetAngle, 0);
            transform.Rotate(offset);
            doorOpen = true;
        } else
        {
            Debug.LogWarning("Door is disabled.");
        }
    }

    public void CloseDoor()
    {
        source.PlayOneShot(sfxDoor);
        transform.localEulerAngles = Vector3.zero;
    }
}
