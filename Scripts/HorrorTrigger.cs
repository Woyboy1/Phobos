using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorTrigger : MonoBehaviour
{
    [SerializeField] private bool playGlobalSound = false;
    [SerializeField] private string globalSoundName;
    [SerializeField] private string triggerName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!playGlobalSound)
            {
                HorrorManager.instance.TriggerAudioCue(triggerName);
                Destroy(this.gameObject);
            } else
            {
                AudioManager.instance.Play(globalSoundName);
            }
        }
    }
}
