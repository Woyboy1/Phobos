using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorElement : MonoBehaviour
{
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
