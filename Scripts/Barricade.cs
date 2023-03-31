using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : Interactable
{
    [Header("Audio")]
    [SerializeField] private AudioClip sfxBarricadeOpen;
    [SerializeField] private AudioClip sfxBarricadeDestroyed;
    [SerializeField] private int hits = 8;

    AudioSource source;

    new void Start()
    {
        base.Start();
        interactableID = "Barricade";
        source = GetComponent<AudioSource>();
    }

    new void Update()
    {
        base.Update();
    }
    
    private void StaminaCheck()
    {
        FindObjectOfType<UIStaminaBar>().DrainStamina(1.0f);
        if (FindFirstObjectByType<UIStaminaBar>().StartingStamina <= 0) { return; }
    }

    public void DestroyBarricade()
    {
        source.PlayOneShot(sfxBarricadeOpen);
        InstantiateParticles();

        StaminaCheck();

        hits--; 
        if (hits <= 0) 
        {
            AudioSource.PlayClipAtPoint(sfxBarricadeDestroyed, transform.position);
            InstantiateParticles();
            Destroy(this.gameObject); 
        }
    }

    public void DestroyBarricadeWithCrowbar() // condition checked in player interawctions script
    {
        AudioManager.instance.Play("Crowbar");
        InstantiateParticles();
        

        StaminaCheck();

        hits -= 4;

        if (hits <= 0)
        {
            AudioSource.PlayClipAtPoint(sfxBarricadeDestroyed, transform.position);
            InstantiateParticles();
            Destroy(this.gameObject);
        }
    }
}
