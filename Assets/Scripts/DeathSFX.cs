using UnityEngine;
using System.Collections.Generic;

public class DeathSFX : MonoBehaviour
{

    public List<AudioClip> deathSounds;
    public AudioSource audioSource;

    void Start()
    {
        int index = Random.Range(0, deathSounds.Count);
        audioSource.PlayOneShot(deathSounds[index]);
        Destroy(gameObject, 1.5f);
    }

}
