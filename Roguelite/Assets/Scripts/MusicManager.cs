using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;  // Esta es la instancia Singleton

    public List<AudioClip> backgroundMusicList; // Lista de música de fondo

    private AudioSource audioSource;

    void Awake()
    {
        // Configura el Singleton
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

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Inicia la música de fondo
        PlayRandomBackgroundMusic();
    }

    public void PlayRandomBackgroundMusic()
    {
        if (backgroundMusicList.Count > 0)
        {
            AudioClip clip = backgroundMusicList[Random.Range(0, backgroundMusicList.Count)];
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No se han asignado pistas de música de fondo.");
        }
    }
}
