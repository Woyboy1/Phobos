using UnityEngine.Audio;
using System;
using UnityEngine;

// Attach this to an empty GameObject (and them make a prefab)
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    void Awake()
    {
        // Use a Singletion pattern
        // If there is already an instance, destroy the instance we are trying to build
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            // outputAudioMixerGroup is  an AudioSource property of type AudioMixerGroup 
            // representing the target group to which the AudioSource should route its signal.			
            s.source.outputAudioMixerGroup = mixerGroup;
        }

        Play("MainMenuMusic");
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Adjust pitch and volume with some random values
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    // Can add other methods such as PlayOneShot, etc...

    public void Stop(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == soundName)
            {
                sound.source.Stop();
                return;
            }

            /*
            if ((sound.name = soundName) != null)
            {
                if (sound.name == soundName)
                {
                    sound.source.Stop();
                    return;
                }
            } else if ((sound.name = soundName) == null)
            {
                Debug.Log("Dumb");
            }
            */
        }
    }

}