using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Door : Interactable
{
    /// <summary>
    /// This is my own custom script of making a functioning door through code. I don't use animations to work
    /// the doors. Yes, it may be easier to do it through animations but I wanted a fast simple mechanic to not
    /// make this game super detailed. And because I'm pretty lazy with animations and not very experienced. 
    /// 
    /// This door cannot be exceptionally locked but it can be "blocked" through the use of the canBeOpen boolean
    /// 
    /// There is a separate script for locked doors with keys that the player will require. 
    /// 
    /// Note:
    /// - This may be the final build for the door class but more may come when needed
    /// 
    /// </summary>

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
            AudioManager.instance.Play("DoorBudge");
        }
    }

    public void CloseDoor()
    {
        source.PlayOneShot(sfxDoor);
        transform.localEulerAngles = Vector3.zero;
    }
}
