using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // for looping audio
    [SerializeField] private AudioSource sfxSource; // for sfx

    [Header("Audio Clips")]
    [SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private AudioClip gameplayClip;
    [SerializeField] private AudioClip footsteps;
    [SerializeField] private AudioClip doorUnlock;
    [SerializeField] private AudioClip openBook;
    [SerializeField] private AudioClip enterText;

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    [Header("Audio Image Settings")]
    [SerializeField] private Image sfxImage;
    [SerializeField] private Image musicImage;
    [SerializeField] private Image masterImage;
    [SerializeField] private Gradient gradient;

    public bool playSound;

    private void Start()
    {
        audioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("SFX").GetComponent<AudioSource>();

        SetVolume("Master");
        SetVolume("Music");
        SetVolume("SFX");
    }

    private void Update()
    {
        if(playSound)
        {
            PlaySfxAudio("Door");
            Debug.Log("played");
            playSound = !playSound;
        }

    }

    // This should be used for looping audio.
    public void PlayAudio(string audio)
    {
        switch (audio)
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
            case "PlayerWalk": sfxSource.PlayOneShot(footsteps, 1f);
            Debug.Log("Walk played"); break;
            case "Door": sfxSource.PlayOneShot(doorUnlock, 1f);  
            Debug.Log("door played"); break;
            case "Book": sfxSource.PlayOneShot(openBook, 1f);
            Debug.Log("Book played");  break;
            case "EnterText": sfxSource.PlayOneShot(enterText, 1f);
            Debug.Log("Text played"); break;
        }
        //sfxSource.PlayOneShot(sfxSource.clip, 1f);
    }

    public void StopSFXAudio()
    {
        sfxSource.Stop();
    }

    // Sets volume
    public void SetVolume(string slider)
    {
        switch (slider)
        {
            case "SFX":
                audioMixer.SetFloat(slider, Mathf.Log10(sfxSlider.value) * 20);
                sfxImage.color = gradient.Evaluate(sfxImage.fillAmount);
                break;
            case "Music":
                audioMixer.SetFloat(slider, Mathf.Log10(musicSlider.value) * 20);
                musicImage.color = gradient.Evaluate(musicImage.fillAmount);
                break;
            case "Master":
                audioMixer.SetFloat(slider, Mathf.Log10(masterSlider.value) * 20);
                masterImage.color = gradient.Evaluate(masterImage.fillAmount);
                break;
            default: Debug.Log(slider + " doesnt exist"); break;
        }
    }

    // used for increase volume button
    public void IncreaseAudio(AudioMixerGroup mixerGroup)
    {
        switch (mixerGroup.name)
        {
            case "SFX":
                sfxSlider.value += 0.1f;
                break;
            case "Music":
                musicSlider.value += 0.1f;
                break;
            case "Master":
                masterSlider.value += 0.1f;
                break;
        }

    }

    // used for decrease volume button
    public void DecreaseAudio(AudioMixerGroup mixerGroup)
    {
        switch (mixerGroup.name)
        {
            case "SFX":
                sfxSlider.value -= 0.1f;
                break;
            case "Music":
                musicSlider.value -= 0.1f;
                break;
            case "Master":
                masterSlider.value -= 0.1f;
                break;
        }
    }
}
