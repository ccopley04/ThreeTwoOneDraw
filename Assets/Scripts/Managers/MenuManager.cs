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

    public static MenuManager Instance { get; private set; }

    public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
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
