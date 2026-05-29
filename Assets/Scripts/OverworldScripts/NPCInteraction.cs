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
    public Image portrait;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogueText;
    public GameObject interactPrompt;
    public GameObject enterPrompt;
    public GameObject player;
    private Boolean fighting;
    public bool tutorialNPC;
    public int sheriffWin;
    public int banditWin;
    [SerializeField] public DialogueButtons dialogueButtons;
    

    
    public DialogueLine[] lines;
    public DialogueLine[] lines2;
    public Sprite[] images;
    public GameObject tutorialImage;
    public int lineNum;
    private bool runNextLine = true;
    private bool npcInteractedWith = false;

    public bool playerIsNearby { get; private set; }
    public bool inDialogue { get; private set; }
    public DialogueLine[] postSheriffWinDialogue;
    public DialogueLine[] postBanditWinDialogue;
    public DialogueLine[] playerWinDialogue;
    public DialogueLine[] playerLoseDialogue;

    [Header("Speakers (define MC + other characters here)")]
    public SpeakerDefinition[] speakers;

    [Header("Choices (indexed by dialogue line index)")]
    public DialogueChoice[] choices;
    public DialogueChoice[] choices2;
    public DialogueChoice[] postSheriffWinChoices;
    public DialogueChoice[] postBanditWinChoices;
    public DialogueChoice[] playerWinChoices;
    public DialogueChoice[] playerLoseChoices;

    public DialogueLine[] resultText;

    public bool demoNPC = false;
    public TutorialFight tutorialScript;


    // Update is called once per frame
    void Update()
    {
        // if (lines2.Length != 0 && npcInteractedWith) {
        //     lines = lines2;
        //     demoNPC = true;
        // }
        
        // if (choices2.Length != 0 && npcInteractedWith) {
        //     choices = choices2;
        // }
        // if (playerWinDialogue.Length != 0 && lines == playerWinDialogue && demoNPC == true)
        // {
        //     lines = playerWinDialogue;
        //     //lines = playerLoseDialogue;

        //     //nextLine();
        // }
        // if (playerLoseDialogue.Length != 0 && lines == playerLoseDialogue && demoNPC == true)
        // {
        //     lines = playerLoseDialogue;


        //     //nextLine();
        // }
        if (lines2.Length != 0 && npcInteractedWith && demoNPC == false) {
            lines = lines2;
            demoNPC = true;
        }
        
        if (choices2.Length != 0 && npcInteractedWith && lines == lines2) {
            choices = choices2;
        }
        if (postSheriffWinDialogue.Length != 0 && sheriffWin == 1)
        {
            lines = postSheriffWinDialogue;
        }
        if (lines == postSheriffWinDialogue)
        {
            choices = postSheriffWinChoices;
        }
        if (postBanditWinDialogue.Length != 0 && banditWin == 1)
        {
            lines = postBanditWinDialogue;
        }
        if (lines == postBanditWinDialogue)
        {
            choices = postBanditWinChoices;
        }
        //If NPC is interacted with: sets up UI and first line, freezes player
        if (playerIsNearby && !inDialogue && Input.GetKeyDown(KeyCode.E))
        {
            inDialogue = true;
            OverworldManager.canExit = false;
            lineNum = 0;
            interactPrompt.SetActive(false);
            dialogueBox.SetActive(true);
            enterPrompt.SetActive(true);
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = lines[0].text;
            runNextLine = true;
            if (dialogueButtons != null) {
                if (dialogueButtons.SetTextButton(0)) {
                    runNextLine = false;
                    enterPrompt.SetActive(false);
                    Debug.Log("Beginning runNextLine: " + runNextLine);
                }
            }
            foreach (SpeakerDefinition speaker in speakers) {
                if (speaker.id == lines[0].speakerId) {
                    portrait.sprite = speaker.portrait;
                    speakerName.text = speaker.displayName;
                }
            }

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
            if (Input.GetKeyDown(KeyCode.Return) && runNextLine)
            {
                nextLine();
            }
        }
    }
    public IEnumerator PlayAtIndex(int index)
    {
        lineNum = index;

        dialogueText.text = lines[lineNum].text;

        foreach (SpeakerDefinition speaker in speakers)
        {
            if (speaker.id == lines[lineNum].speakerId)
            {
                portrait.sprite = speaker.portrait;
                speakerName.text = speaker.displayName;
            }
        }
        if (dialogueButtons != null)
        {
            if (dialogueButtons.SetTextButton(lineNum))
            {
                runNextLine = false;
                enterPrompt.SetActive(false);

            }
            else
            {
                runNextLine = true;
                enterPrompt.SetActive(true);

            }
        }

        yield return null;

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
            if (lineNum > 0 && lineNum == lines.Length) {
                npcInteractedWith = true;
            }
        }
    }

    public int getLineNum() {
        return lineNum;
    }

    public void setRunNextLine(bool value) {
        runNextLine = value;
    }
    private void nextLine()
    {
        if (!runNextLine)
        {
            Debug.Log("Blocked dialogue progression");
            return;
        }

        Debug.Log("RunNextLine in NextLine: " + runNextLine);
        if (runNextLine) {
            ++lineNum;
        }    
        //runNextLine = true;
        Debug.Log("Linenum: " + lineNum);
        if ((demoNPC && lineNum == lines.Length - 1) || lines[lineNum - 1].quit == 1)
        {
            dialogueText.gameObject.SetActive(false);
            enterPrompt.SetActive(false);
            dialogueBox.SetActive(false);
            runNextLine = true;

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
                else if (gameObject.tag == "Sheriff")
                {
                    OverworldManager.enemy = new Sheriff();
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
                OverworldManager.canExit = true;
                inDialogue = false;
                runNextLine = true;
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
            dialogueText.text = lines[lineNum].text;
            if (dialogueButtons != null) {
                if (dialogueButtons.SetTextButton(lineNum)) {
                    runNextLine = false;
                    Debug.Log("Setting false in next line" + runNextLine);
                }
            }
            foreach (SpeakerDefinition speaker in speakers) {
                if (speaker.id == lines[lineNum].speakerId) {
                    portrait.sprite = speaker.portrait;
                    speakerName.text = speaker.displayName;
                }
            }
        }
    }

    private void OnEnable()
    {
        if (fighting && demoNPC)
        {
            fighting = false;
            DialogueLine[] resultText = (EncounterControl.Instance.playerWonLast) ? playerWinDialogue : playerLoseDialogue;
            if (resultText == playerWinDialogue)
            {
                if (EncounterControl.Instance.currEnemy is Sheriff) {
                    sheriffWin = 1;
                } 
                else
                {
                    banditWin = 1;
                }
            } 
            else
            {
                if (EncounterControl.Instance.currEnemy is Sheriff)
                {
                    sheriffWin = -1;
                }
                else
                {
                    banditWin = -1;
                }
            }
            DialogueChoice[] resultChoices = (EncounterControl.Instance.playerWonLast) ? playerWinChoices : playerLoseChoices;
            lines = resultText;
            choices = resultChoices;
            lineNum = 0;
            EncounterControl.Instance.tutorialScript.currentState = TutorialFight.TutorialState.None;
            

            inDialogue = true;
            //lineNum = lines.Length - 1;

            interactPrompt.SetActive(false);
            dialogueBox.SetActive(true);
            enterPrompt.SetActive(true);
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = lines[lineNum].text;
            runNextLine = true;

            SpriteMovement movement = player.GetComponent<SpriteMovement>();
            movement.isFrozen = true;
        }
    }
}