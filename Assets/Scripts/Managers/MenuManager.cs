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
    public List<GameObject> UIObjects = new List<GameObject>();
    public List<GameObject> credits = new List<GameObject>();

    public static MenuManager Instance;
    private Sprite firstSprite;

    public void Awake() {
        if (Instance == null) {
            Instance = gameObject.GetComponent<MenuManager>();
        }

        firstSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void Quit() {
        Application.Quit();
    }

    public void StartGame() {
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
        gameObject.GetComponent<SpriteRenderer>().sprite = firstSprite;
    }

    public void ReturnToMenu() {
        foreach (GameObject button in buttons) {
                    button.SetActive(true);
       }
    }

    public void PlayShot() {
        SoundManager.playSound(SoundType.SixShooterBullet);
    }


    public void StartCredits() {
        foreach (GameObject credit in credits) {
                        credit.SetActive(true);
                    }
                    foreach (GameObject uiObject in UIObjects) {
                        uiObject.SetActive(false);
                    }
        gameObject.GetComponent<SpriteRenderer>().sprite = firstSprite;
    }



    public void EndCredits() {
            foreach (GameObject credit in credits) {
                                           credit.SetActive(false);
                                           }
                                           foreach (GameObject uiObject in UIObjects) {
                                               uiObject.SetActive(true);
                                           }
    gameObject.GetComponent<SpriteRenderer>().sprite = firstSprite;
    }
}
