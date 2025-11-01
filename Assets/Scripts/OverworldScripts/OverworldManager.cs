using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class OverworldManager : MonoBehaviour
{
    public static AbstractWeapon weapon = new SixShooter();
    public static Enemy enemy = new Cactus();
    public static bool isTutorial = false;
    public GameObject player;

    public static List<AbstractCard> starterDeck = new List<AbstractCard>();

    void Start()
    {
        if (!SceneManager.GetSceneByName("CombatDemo").isLoaded)
        {
            SceneManager.LoadScene("CombatDemo", LoadSceneMode.Additive);
        }


        for (int i = 0; i < 3; i++)
        {
            starterDeck.Add(new TakeAim());
            starterDeck.Add(new Defend());
        }

        MusicManager.playSound(MusicType.Theme, 0.5F);
        MusicManager.audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!SceneManager.GetSceneByName("CombatDemo").isLoaded)
            {
                SceneManager.LoadScene("CombatDemo", LoadSceneMode.Additive);
            }
            StartCoroutine(startCombat(weapon, starterDeck, enemy));
        }
    }

    public static IEnumerator startCombat(AbstractWeapon weapon, List<AbstractCard> deck, Enemy enemy)
    {
        MusicManager.audioSource.loop = true;
        MusicManager.audioSource.Stop();
        MusicManager.playSound(MusicType.Intro);

        float duration = 4F;
        while (duration > 0)
        {

            //Alter the time by the time since last frame
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                duration = 0;
            }

            yield return null;
        }

        SoundManager.playSound(SoundType.SixShooterBullet);
        DisableOverworld.Instance.enableOverworld(false);
        EncounterControl.Instance.startEncounter(new Encounter(new Player(deck, 100, 2, 2), enemy, weapon), isTutorial);
    }

}
