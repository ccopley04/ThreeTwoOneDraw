using UnityEngine;
using System.Collections.Generic;
using System;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] sixShooterSounds;
    [SerializeField]
    private AudioClip[] defendSounds;
    [SerializeField]
    private AudioClip[] cardDrawSounds;
    [SerializeField]
    private AudioClip[] tomahawkSounds;
    [SerializeField]
    private AudioClip[] winchesterSounds;
    [SerializeField]
    private AudioClip[] reloadSounds;
    [SerializeField]
    private AudioClip[] wildWestWalking;
    [SerializeField]
    private AudioClip[] dynamiteExplosion;

    private static List<AudioClip[]> sounds = new List<AudioClip[]>();
    public static AudioSource audioSource;

    private void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = gameObject.GetComponent<AudioSource>();

        EncounterControl.EPressed += PlayReload;

        sounds.Add(sixShooterSounds);
        sounds.Add(defendSounds);
        sounds.Add(cardDrawSounds);
        sounds.Add(tomahawkSounds);
        sounds.Add(winchesterSounds);
        sounds.Add(reloadSounds);
        sounds.Add(wildWestWalking);
        sounds.Add(dynamiteExplosion);
    }

    public static void playSound(SoundType sound, float volume = 1)
    {
        if ((int)sound >= sounds.Count || sounds[((int)sound)].Length == 0)
        {
            return;
        }
        audioSource.PlayOneShot(sounds[((int)sound)][UnityEngine.Random.Range(0, sounds[((int)sound)].Length)], volume);
    }

    //Play the reload sound, used in the event system when E is pressed
    private void PlayReload()
    {
        playSound(SoundType.Reload);
    }
}

public enum SoundType
{
    SixShooterBullet,
    Defend,
    CardDraw,
    TomahawkBullet,
    WinchesterBullet,
    Reload,
    WildWestWalking,
    Explosion
}