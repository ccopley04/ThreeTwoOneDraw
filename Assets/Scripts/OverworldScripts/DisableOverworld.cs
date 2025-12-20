using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class DisableOverworld : MonoBehaviour
{

    [SerializeField]
    public string Combat = "CombatDemo";
    public string Overworld = "OverWEug";
    public GameObject player;


    public static DisableOverworld Instance { get; private set; }
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

    [SerializeField]
    private List<GameObject> allObjects = new List<GameObject>();
    public void enableOverworld(bool state)
    {
        player.GetComponent<SpriteMovement>().isFrozen = !state;
        foreach (GameObject singleObject in allObjects)
        {
            singleObject.SetActive(state);
        }

        if (state)
        {
            Scene overworldScene = SceneManager.GetSceneByName(Overworld);
            if (!overworldScene.isLoaded) //Checks if scene is not loaded
            {
                SceneManager.LoadScene(Overworld, LoadSceneMode.Additive);
            }
            else if (overworldScene.IsValid())  //Checks if scene exists
            {
                SceneManager.SetActiveScene(overworldScene);
            }
        }
        else
        {

            Scene combatScene = SceneManager.GetSceneByName(Combat);
            if (!combatScene.isLoaded) //Checks if scene is not loaded
            {
                // Load the scene additively (keeps current scene loaded)
                SceneManager.LoadScene(Combat, LoadSceneMode.Additive);
            }
            else if (combatScene.IsValid()) //Checks if scene exists
            {
                SceneManager.SetActiveScene(combatScene);
            }
        }
    }
}
