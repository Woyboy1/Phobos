using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorTrigger : MonoBehaviour
{
    [SerializeField] private string triggerName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HorrorManager.instance.TriggerAudioCue(triggerName);
            Destroy(this.gameObject);
        }
    }
}
