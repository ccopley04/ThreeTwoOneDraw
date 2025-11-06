using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    //Elements in scene
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public GameObject interactPrompt;
    public GameObject enterPrompt;
    public GameObject player;

    public string[] lines;
    private int lineNum;

    public bool playerIsNearby { get; private set; }
    public bool inDialogue { get; private set; }

    //Temp variable to indicate if we are testing weapon selection
    //The number corresponds to what line is the one to check, -1 means no line should be checked
    public int weaponSelectLine = 3;
    public int enemySelectLine = 4;
    public bool demoNPC = true;


    // Update is called once per frame
    void Update()
    {
        //If NPC is interacted with: sets up UI and first line, freezes player
        if (playerIsNearby && !inDialogue && Input.GetKeyDown(KeyCode.E))
        {
            inDialogue = true;
            lineNum = 0;

            interactPrompt.SetActive(false);
            dialogueBox.SetActive(true);
            enterPrompt.SetActive(true);
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = lines[0];

            SpriteMovement movement = player.GetComponent<SpriteMovement>();
            movement.isFrozen = true;
        }

        //Displays next line of dialogue and end dialogue when all lines read
        if (inDialogue)
        {
            if (weaponSelectLine == lineNum)
            {
                enterPrompt.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    OverworldManager.weapon = new Tomahawk();
                    nextLine();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    OverworldManager.weapon = new Winchester();
                    nextLine();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    OverworldManager.weapon = new SixShooter();
                    nextLine();
                }
            }
            else if (enemySelectLine == lineNum)
            {
                enterPrompt.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    OverworldManager.enemy = new Cactus();
                    OverworldManager.isTutorial = true;
                    EncounterControl.Instance.bulletPlayed = false;
                    EncounterControl.Instance.defendPlayed = false;
                    EncounterControl.Instance.takeAimPlayed = false;
                    EncounterControl.Instance.deckRanOut = false;
                    EncounterControl.Instance.battleStarted = false;
                    nextLine();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    OverworldManager.enemy = new Cactus();
                    OverworldManager.isTutorial = false;
                    nextLine();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    OverworldManager.enemy = new BanditBoss();
                    OverworldManager.isTutorial = false;
                    nextLine();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                nextLine();
            }
        }
    }

    //Enter NPC hitbox
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = true;
            interactPrompt.SetActive(true);
        }
    }

    //Leave NPC hitbox
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = false;
            interactPrompt.SetActive(false);
        }
    }

    private void nextLine()
    {
        ++lineNum;
        if (lineNum == lines.Length)
        {
            dialogueText.gameObject.SetActive(false);
            enterPrompt.SetActive(false);
            dialogueBox.SetActive(false);


            SpriteMovement movement = player.GetComponent<SpriteMovement>();

            if (demoNPC)
            {
                StartCoroutine(OverworldManager.startCombat(OverworldManager.weapon, OverworldManager.starterDeck, OverworldManager.enemy));
            }
            movement.isFrozen = false;
            inDialogue = false;
        }
        else
        {
            enterPrompt.SetActive(true);
            dialogueText.text = lines[lineNum];
        }
    }
}