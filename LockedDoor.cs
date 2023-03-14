using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable
{
    [Tooltip("You should change this only if you're making double doors")]
    [SerializeField] private float offsetAngle = -90f;

    [Header("Audio")]
    [SerializeField] AudioClip sfxDoor;

    private AudioSource source;
    private bool isOpened = false;

    new void Start()
    {
        base.Start();
        source = GetComponent<AudioSource>();
        interactableID = "LockedDoor";
    }

    new void Update()
    {
        base.Update();
    }

    public void OpenDoor(bool hasKey)
    {
        if (!isOpened)
        {
            if (hasKey)
            {
                source.PlayOneShot(sfxDoor);
                Vector3 offset = new Vector3(0, 0, offsetAngle);
                transform.Rotate(offset);
                isOpened = true;
            }
        } else
        {
            Debug.Log("It's already open");
        }
    }
}
