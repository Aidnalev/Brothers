using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioSource audioSource2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        AudioSource[] audioSources = GetComponents<AudioSource>();

        audioSource = audioSources[0];

        if (audioSources.Length < 2)
        {
            audioSource2 = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource2 = audioSources[1];
        }
    }

    public void ReproducirSonido(AudioClip clip)
    {
        audioSource2.PlayOneShot(clip);
    }

    public void ReproducirSonidoEnBucle(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void DetenerSonido()
    {
        audioSource.Stop();
    }
}
