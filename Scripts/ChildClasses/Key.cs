using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    new void Start()
    {
        base.Start();
        interactableID = "Key";
    }

    new void Update()
    {
        base.Update();
    }

    public void PickUpKey()
    {
        InstantiateParticles();
        AudioManager.instance.Play("Interact");
        Destroy(this.gameObject);
    }
}
