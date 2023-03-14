using System;
using UnityEngine.Audio;
using UnityEngine;

[Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = .75f;

    [Range(0f, 1f)]
    public float volumeVariance = .1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;

    [Range(0f, 1f)]
    public float pitchVariance = .1f;

    public bool loop = false;

    // AudioMixerGroup is class representing a group for the mixer
    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;
}