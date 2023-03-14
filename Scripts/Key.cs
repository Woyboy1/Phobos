using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    [SerializeField] private ParticleSystem pickupFX;

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
        Instantiate(pickupFX, transform.position, Quaternion.identity);
        AudioManager.instance.Play("Interact");
        Destroy(this.gameObject);
    }
}
