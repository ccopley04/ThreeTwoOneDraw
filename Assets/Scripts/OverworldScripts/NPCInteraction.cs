using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NPCInteraction : MonoBehaviour
{
    //Elements in scene
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public GameObject interactPrompt;
    public GameObject enterPrompt;
    public GameObject player;
    private Boolean fighting;
    public bool tutorialNPC;


    public string[] lines;
    public Sprite[] images;
    public GameObject tutorialImage;
    private int lineNum;

    public bool playerIsNearby { get; private set; }
    public bool inDialogue { get; private set; }
    public string playerWinDialogue;
    public string playerLoseDialogue;

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

            if (tutorialNPC && images[0] != null)
            {
                tutorialImage.SetActive(true);
                tutorialImage.GetComponent<Image>().sprite = images[0];
            }

            SpriteMovement movement = player.GetComponent<SpriteMovement>();
            movement.isFrozen = true;
        }

        //Displays next line of dialogue and end dialogue when all lines read
        if (inDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                nextLine();
            }
        }
    }

    //Enter NPC hitbox
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !inDialogue)
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
        if (lineNum == lines.Length || (demoNPC && lineNum == lines.Length - 1))
        {
            dialogueText.gameObject.SetActive(false);
            enterPrompt.SetActive(false);
            dialogueBox.SetActive(false);


            SpriteMovement movement = player.GetComponent<SpriteMovement>();

            if (demoNPC && lineNum == lines.Length - 1)
            {
                if (gameObject.tag == "Cactus")
                {
                    OverworldManager.enemy = new Cactus();
                }
                else if (gameObject.tag == "BanditBoss")
                {
                    OverworldManager.enemy = new BanditBoss();
                }

                OverworldManager.isTutorial = false;
                fighting = true;
                StartCoroutine(OverworldManager.startCombat(OverworldManager.weapon, OverworldManager.starterDeck, OverworldManager.enemy));
            }
            else
            {
                dialogueText.gameObject.SetActive(false);
                enterPrompt.SetActive(false);
                dialogueBox.SetActive(false);
                if (tutorialNPC)
                {
                    tutorialImage.SetActive(false);
                }

                movement.isFrozen = false;
                inDialogue = false;
            }
        }
        else
        {
            if (tutorialNPC)
            {
                if (images[lineNum] != null)
                {
                    tutorialImage.SetActive(true);
                    tutorialImage.GetComponent<Image>().sprite = images[lineNum];
                }
                else
                {
                    tutorialImage.SetActive(false);
                }
            }
            enterPrompt.SetActive(true);
            dialogueText.text = lines[lineNum];
        }
    }

    private void OnEnable()
    {
        if (fighting && demoNPC)
        {
            fighting = false;
            lines[lines.Length - 1] = (EncounterControl.Instance.playerWonLast) ? playerWinDialogue : playerLoseDialogue;
            inDialogue = true;
            lineNum = lines.Length - 1;

            interactPrompt.SetActive(false);
            dialogueBox.SetActive(true);
            enterPrompt.SetActive(true);
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = lines[lineNum];

            SpriteMovement movement = player.GetComponent<SpriteMovement>();
            movement.isFrozen = true;
        }
    }
}