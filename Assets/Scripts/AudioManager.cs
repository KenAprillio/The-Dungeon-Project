using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Clip Files")]
    public AudioClip mainMusic;
    public AudioClip inbetweenMusic;

    [Header("Audio Source")]
    public AudioSource mainMusicSource;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayMusic(AudioClip audioClip)
    {
        StartCoroutine(FadeInClips(audioClip));
    }

    private IEnumerator FadeInClips(AudioClip audioClip)
    {
        float timeToFade = 3f;
        float timeElapsed = 0;

        while(timeElapsed < timeToFade)
        {
            mainMusicSource.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
            yield return null;
            

            timeElapsed += Time.unscaledDeltaTime;
        }

        mainMusicSource.Stop();
        mainMusicSource.clip = audioClip;
        mainMusicSource.Play();

        yield return StartCoroutine(FadeOutClips());
    }

    private IEnumerator FadeOutClips()
    {
        float timeToFade = 3f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            mainMusicSource.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            yield return null;


            timeElapsed += Time.unscaledDeltaTime;
        }
        yield break;
    }
}
