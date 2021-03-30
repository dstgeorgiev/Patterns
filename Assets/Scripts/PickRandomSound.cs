using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRandomSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] sounds;

    public void PlayRandom()
    {
        if (source != null)
            source.PlayOneShot(GetRandom());
    }

    public AudioClip GetRandom()
    {
        return sounds[Random.Range(0, sounds.Length)];
    }
}
