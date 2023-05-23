using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryRecharger : Interactable
{
    [SerializeField] private float batteryCharge = 35.0f;

    new void Start()
    {
        base.Start();
        interactableID = "Battery";
    }

    new void Update()
    {
        base.Update();
    }

    public void Recharge()
    {
        AudioManager.instance.Play("BatteryCharge");
        FindAnyObjectByType<PlayerInteractions>().RechargeBattery(batteryCharge);
        InstantiateParticles();
        Destroy(this.gameObject);
    }
}
