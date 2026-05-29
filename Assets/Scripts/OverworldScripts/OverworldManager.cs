using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using NUnit.Framework;

public class OverworldManager : MonoBehaviour
{
    [SerializeField]
    public TutorialFight tutorialScript;
    private GameObject tempInventory;
    public static AbstractWeapon weapon = new SixShooter();
    public static Enemy enemy = new Sheriff();
    public static bool isTutorial = false;
    public GameObject player;
    private SpriteMovement movement;
    public static bool canExit = true;


    public static List<AbstractCard> starterDeck = new List<AbstractCard>();
    public List<GameObject> pauseButtons = new List<GameObject>();

    void Start()
    {
        if (!SceneManager.GetSceneByName("TutorialCombat").isLoaded)
        {
           SceneManager.LoadScene("TutorialCombat", LoadSceneMode.Additive);
        }
        if (!SceneManager.GetSceneByName("CombatDemo").isLoaded)
        {
            SceneManager.LoadScene("CombatDemo", LoadSceneMode.Additive);
        }


        for (int i = 0; i < 3; i++)
        {
            starterDeck.Add(new TakeAim());
            starterDeck.Add(new Defend());
        }
        starterDeck.Add(new SweetRewards());
        starterDeck.Add(new SweetRewards());
        starterDeck.Add(new Bandage());
        OverworldManager.weapon = new SixShooter();
        movement = player.GetComponent<SpriteMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!SceneManager.GetSceneByName("TutorialCombat").isLoaded)
            {
                SceneManager.LoadScene("TutorialCombat", LoadSceneMode.Additive);
            }
            StartCoroutine(startCombat(weapon, starterDeck, enemy));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!SceneManager.GetSceneByName("CombatDemo").isLoaded)
            {
                SceneManager.LoadScene("CombatDemo", LoadSceneMode.Additive);
            }
            StartCoroutine(startCombat(weapon, starterDeck, enemy));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            tempInventory.SetActive(!tempInventory.activeSelf);
            movement.isFrozen = !movement.isFrozen;
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
        Debug.Log(EncounterControl.Instance);
        ///Debug.Log(tutorialScript);
        //Debug.Log(encounter);
        //Debug.Log(encounter.enemy);
        Debug.Log("startCombat deck: " + deck);
        Debug.Log("startCombat enemy: " + enemy);
        Debug.Log("startCombat weapon: " + weapon);
        Debug.Log("startCombat tutorial: " + isTutorial);

        EncounterControl.Instance.startEncounter(new Encounter(new Player(deck, 100, 2, 2), enemy, weapon), isTutorial);
    }

}
