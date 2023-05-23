using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform teleportLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().TeleportPlayerTo(teleportLocation);
            AudioManager.instance.Play("Breath");
            Destroy(this.gameObject, 1.0f);
        }    
    }
}
