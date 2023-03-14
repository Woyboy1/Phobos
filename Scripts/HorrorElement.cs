using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorElement : MonoBehaviour
{
    /// <summary>
    /// This is a HorrorElement class that effectively only execute a sound. This class
    /// communicates with the HorrorManager singleton class, this should not be used anywhere else 
    /// but only the HorrorManager. 
    /// 
    /// In Short:
    /// - HorrorElement functions to only play local sounds in the scene, NOT GLOBAL.
    /// </summary>

    [SerializeField] private string elementID;
    [SerializeField] private AudioClip sfxHorror;
    private AudioSource source;

    public string ElementID
    {
        get { return elementID; }
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        source.PlayOneShot(sfxHorror);
    }

    public void PlaySoundAndDestroy()
    {
        AudioSource.PlayClipAtPoint(sfxHorror, transform.position);
        Destroy(this.gameObject);
    }
}
