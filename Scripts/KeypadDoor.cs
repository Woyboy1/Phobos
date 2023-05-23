using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadDoor : Interactable
{
    [Tooltip("You should change this only if you're making double doors")]
    [SerializeField] private float offsetAngle = -90f;

    new void Start()
    {
        base.Start();
        interactableID = "KeypadDoor";
    }

    new void Update()
    {
        base.Update();
    }

    public void UnlockDoor()
    {
        Vector3 offset = new Vector3(0, 0, offsetAngle);
        transform.Rotate(offset);
    }

}
