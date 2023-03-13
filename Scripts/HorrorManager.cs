using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorManager : MonoBehaviour
{
    public static HorrorManager instance;

    [SerializeField] private HorrorElement[] horrorElements; 

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
            if (horrorElements[i].ElementID == horrorID)
            {
                horrorElements[i].PlaySoundAndDestroy();
            }
            else
            {
                Debug.LogWarning("HorrorManager: Could not find the horrorID");
            }
        }
    }

    
}
