using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip mainTheme;
    [SerializeField]
    private AudioClip intro;
    [SerializeField]
    private AudioClip tutorialTheme;

    private static List<AudioClip> sounds = new List<AudioClip>();
    public static AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
        sounds.Add(mainTheme);
        sounds.Add(intro);
        sounds.Add(tutorialTheme);

        EncounterControl.start += PlayCombatTheme;
        EncounterControl.end += EndCombatTheme;

        MusicManager.playSound(MusicType.Theme, 0.5F);
    }

    public static void playSound(MusicType sound, float volume = 1)
    {
        if ((int)sound >= sounds.Count)
        {
            return;
        }
        audioSource.volume = volume;
        audioSource.clip = sounds[(int)sound];
        audioSource.Play();
    }

    private void PlayCombatTheme(Encounter encounter)
    {
        MusicManager.playSound(MusicType.Tutorial, 0.4F);
        MusicManager.audioSource.loop = true;
    }

    private void EndCombatTheme(Encounter encounter)
    {
        MusicManager.audioSource.Stop();
        MusicManager.playSound(MusicType.Theme, 0.5F);
        MusicManager.audioSource.loop = true;
    }

}

public enum MusicType
{
    Theme,
    Intro,
    Tutorial
}
