using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using NUnit.Framework;

public class MenuManager : MonoBehaviour
{

    public List<GameObject> buttons = new List<GameObject>();
    public GameObject startButton;
    public List<GameObject> UIObjects = new List<GameObject>();
    public List<GameObject> credits = new List<GameObject>();

    public static MenuManager Instance { get; private set; }

    public Animator startAnimator;

    public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                MusicManager.playSound(MusicType.Theme, 0.5F);
                MusicManager.audioSource.loop = true;
            }
        }

    public void Quit() {
        StartCoroutine(WaitToQuitGame());
    }

    public IEnumerator WaitToQuitGame() {
            bool shot = false;

            float duration = 1.2F;
            while (duration > 0)
            {
                //Alter the time by the time since last frame
                duration -= Time.deltaTime;
                if (duration <= 0)
                {
                    duration = 0;
                } else if (duration <= 0.7F && !shot) {
                    shot = true;
                    SoundManager.playSound(SoundType.SixShooterBullet);
                }
                yield return null;
            }
            Application.Quit();
        }


    public void StartGame() {
        StartCoroutine(WaitToStartGame());
    }

    public IEnumerator WaitToStartGame() {
        bool shot = false;
        Sprite firstSprite = startButton.GetComponent<SpriteRenderer>().sprite;
        float duration = 2.50F;
        while (duration > 0)
        {
            //Alter the time by the time since last frame
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                duration = 0;
            } else if (duration <= 1.45F && !shot) {
                shot = true;
                SoundManager.playSound(SoundType.SixShooterBullet);
            }
            yield return null;
        }
        if (!SceneManager.GetSceneByName("CombatDemo").isLoaded)
        {
            SceneManager.LoadScene("CombatDemo", LoadSceneMode.Additive);
        }
        if (!SceneManager.GetSceneByName("OverWEug").isLoaded)
        {
            SceneManager.LoadScene("OverWEug", LoadSceneMode.Additive);
        } else {
            DisableOverworld.Instance.enableOverworld(true);
        }

        foreach (GameObject button in buttons) {
            button.SetActive(false);
        }
        startButton.GetComponent<SpriteRenderer>().sprite = firstSprite;
    }

    public void ReturnToMenu() {
        foreach (GameObject button in buttons) {
                    button.SetActive(true);
       }
    }


    public void StartCredits() {
        foreach (GameObject credit in credits) {
            credit.SetActive(true);
        }
        foreach (GameObject uiObject in UIObjects) {
            uiObject.SetActive(false);
        }
    }

    public void EndCredits() {
            foreach (GameObject credit in credits) {
            credit.SetActive(false);
            }
            foreach (GameObject uiObject in UIObjects) {
                uiObject.SetActive(true);
            }
        }
}
