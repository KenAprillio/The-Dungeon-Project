using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource mainAudioSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip _attackSound;
    [SerializeField] AudioClip _specialSound;
    [SerializeField] AudioClip _dashSound;
    [SerializeField] AudioClip _hurtSound;

    public void PlayAttackSound()
    {
        mainAudioSource.Stop();
        mainAudioSource.clip = _attackSound;
        mainAudioSource.Play();
    }

    public void PlaySpecialSound()
    {
        mainAudioSource.Stop();
        mainAudioSource.clip = _specialSound;
        mainAudioSource.Play();
    }

    public void PlayDashSound()
    {
        mainAudioSource.Stop();
        mainAudioSource.clip = _dashSound;
        mainAudioSource.Play();
    }

    public void PlayHurtSound()
    {
        mainAudioSource.Stop();
        mainAudioSource.clip = _hurtSound;
        mainAudioSource.Play();
    }
}
