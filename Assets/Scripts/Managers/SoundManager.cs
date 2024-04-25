using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // for looping audio
    [SerializeField] private AudioSource sfxSource; // for sfx

    [Header("Audio Clips")]
    [SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private AudioClip gameplayClip;
    [SerializeField] private AudioClip footsteps;
    [SerializeField] private AudioClip sfx2;
    [SerializeField] private AudioClip sfx3;

    private void Start()
    {
        audioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }

    // This should be used for looping audio.
    public void PlayAudio(string audio)
    {
        //audioSource.Stop();
        switch(audio)
        {
            case "MainMenu": audioSource.clip = mainMenuClip; break;
            case "Gameplay": audioSource.clip = gameplayClip; break;
        }
        audioSource.loop = true;
        audioSource.Play();

    }

    // This should be used for sfx?
    public void PlaySfxAudio(string audio)
    {
        switch (audio)
        {
            case "PlayerWalk": sfxSource.clip = footsteps; break;
        } 
        sfxSource.PlayOneShot(footsteps, 1f);
    }
    public void StopSFXAudio()
    {
        sfxSource.Stop();
    }
}
