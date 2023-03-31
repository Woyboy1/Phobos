using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class HorrorManager : MonoBehaviour
{
    /// <summary>
    /// HorrorManager is a static singleton class that manages all the horror elements 
    /// within the scene. DO NOT ASSIGN any GameObjects with HorrorTrigger classes, this
    /// will return a null reference error type if you ever try to play an event.
    /// 
    /// Summarized:
    /// - Singleton class managing horror element classes
    /// - Only assign HorrorElements not triggers
    /// 
    /// </summary>

    public static HorrorManager instance;

    [SerializeField] private GameObject[] horrorElements; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        
    }

    public void TriggerAudioCue(string horrorID)
    {
        for (int i = 0; i < horrorElements.Length; i++)
        {
            HorrorElement elements = horrorElements[i].GetComponent<HorrorElement>();
            if (elements.ElementID == horrorID)
            {
                elements.PlaySoundAndDestroy();
                break;
            } else
            {
                // return;
                // Debug.LogWarning("HorrorManager: Could not find the horrorID");
            }
        }
    }
}
