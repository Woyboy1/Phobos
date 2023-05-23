using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorrorTrigger : MonoBehaviour
{
    /// <summary>
    /// The HorrorTrigger class works independently. Whenever a player collides with a GameObject with
    /// this script attached, the class will call the HorrorManager singleton class and trigger the 
    /// basic audio cue.
    /// 
    /// You should NOT attach this GameObject to a HorrorManager singleton, only elements will handle that.
    /// 
    /// Note:
    /// - There may be more mechanics rather than to work with audio
    /// - Future implementations for events using Unity Events?
    /// </summary>

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
                Debug.Log("Successfully play audio cue, destroying object.");
                Destroy(this.gameObject);
            } else
            {
                AudioManager.instance.Play(globalSoundName);
                Destroy(this.gameObject);
            }
        }
    }
}
