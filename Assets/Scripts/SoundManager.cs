using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    void Awake() { Instance = this; }

    public AudioSource audioSource;
    public AudioSource PitchAudioSource;

    public AudioClip HitSound;
    public AudioClip GoalSound;
    public AudioClip StartSound;
    public AudioClip StopSound;
    public AudioClip WallAreaHitSound;

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(HitSound);
    }

    public void PlayGoalSound()
    {
        audioSource.PlayOneShot(GoalSound);
    }

    public void PlayStartSound()
    {
        audioSource.PlayOneShot(StartSound);
    }

    public void PlayStopSound()
    {
        audioSource.PlayOneShot(StopSound);
    }

    public void PlayWallAreaHitSound()
    {
        PitchAudioSource.pitch = Random.Range(0.5f, 1f);
        PitchAudioSource.PlayOneShot(WallAreaHitSound);
    }

}
