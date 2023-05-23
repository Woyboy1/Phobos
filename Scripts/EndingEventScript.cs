using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingEventScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().SprintAbility = false;
            other.GetComponent<PlayerMovement>().CanSprint = false;
            other.GetComponent<PlayerMovement>().IsSprinting = false;
        }
    }
}
