using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TutorialFight : MonoBehaviour
{
    public EncounterControl combatScript;
    
    public DialogueLine[] explainBoardLines;
    public DialogueLine[] explainAttackLines;
    public DialogueLine[] explainDefendLines;
    public DialogueLine[] epxlainSkillLines;
    public DialogueLine[] winLines;
    public DialogueLine[] currentLines;
    public GameObject dialogueBox;
    public Image portrait;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogueText;
    public SpeakerDefinition[] speakers;
    public int lineNum;
    public bool attackDone = false;
    public bool defendDone = false;
    public bool skillDone = false;
    private bool tutorialEnded = false;

    public enum TutorialState {
        None, ExplainBoard, ExplainAttack, ExplainDefend, ExplainSkill, Win
    }
    public TutorialState currentState = TutorialState.ExplainBoard;
    public bool isTutorialPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        SetState(TutorialState.None);
        Debug.Log("Ran set state");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Linenum: " + lineNum);
        //CheckTutorialInput();
        Debug.Log(SceneManager.GetActiveScene().name);
        if (isTutorialPaused) {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (lineNum < currentLines.Length && currentLines[lineNum].quit != 1)
                {
                    NextLine();

                } 
                else
                {
                    DialogueEnded();
                }
            }
        } 
        else
        {
            CheckTutorialInput();

        }
    }
    public void SetState(TutorialState newState)
    {
        Debug.Log("new state: " + newState);
        currentState = newState;
        isTutorialPaused = true;
        dialogueBox.SetActive(true);
        lineNum = 0;
        switch (newState)
        {
            case TutorialState.ExplainBoard:
                currentLines = explainBoardLines;
                dialogueBox.SetActive(true);
                break;
            case TutorialState.ExplainAttack:
                currentLines = explainAttackLines;
                dialogueBox.SetActive(true);

                break;
            case TutorialState.ExplainDefend:
                currentLines = explainDefendLines;
                dialogueBox.SetActive(true);
                break;
            case TutorialState.ExplainSkill:
                currentLines = epxlainSkillLines;
                dialogueBox.SetActive(true);
                break;
            case TutorialState.Win:
                currentLines = winLines;
                dialogueBox.SetActive(true);
                break;
            case TutorialState.None:
                isTutorialPaused = false;
                dialogueBox.SetActive(false);
                return;
        }
        Debug.Log("current state: " + currentState);
        Debug.Log("current lines: " + currentLines);
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        if (currentLines != null && lineNum < currentLines.Length)
        {
            dialogueText.text = currentLines[lineNum].text;
            speakerName.text = speakers[0].displayName;
            portrait.sprite = speakers[0].portrait;

        }

    }
    void NextLine()
    {
        lineNum++;
        Debug.Log("LineNum: " + lineNum);
        if (lineNum >= currentLines.Length)
        {

            //lineNum = 0;
            DialogueEnded();
        }
        else
        {
            DisplayCurrentLine();
        }
        //lineNum++;

    }

    void DialogueEnded()
    {
        if (currentState == TutorialState.ExplainBoard)
        {

            Debug.Log("Dialogue ended: set new state");
            //lineNum = 0;

            SetState(TutorialState.ExplainAttack);

        } else if (currentState == TutorialState.Win)
        {
            tutorialEnded = true;
            combatScript.PlayerWin();

        }
        else
        {
            lineNum = 0;
            Debug.Log("Dialogue ended: proceed to next step");
            ProceedToNextStep();
        }
    }

    void CheckTutorialInput() 
    {
        if (currentState == TutorialState.ExplainAttack) {
            if (attackDone) {
                lineNum = 0;

                SetState(TutorialState.ExplainDefend);
            }
        }
        if (currentState == TutorialState.ExplainDefend) {
            if (defendDone) {
                lineNum = 0;

                SetState(TutorialState.ExplainSkill);
            }
        }
        if (currentState == TutorialState.ExplainSkill) {
            if (skillDone) {
                lineNum = 0;

                SetState(TutorialState.Win);
            }
        }
       if (currentState == TutorialState.Win) {
            if (currentLines == winLines && lineNum == currentLines.Length - 1) {
                SetState(TutorialState.None);
            }
        }
    }
    void ProceedToNextStep() {
        isTutorialPaused = false;
        dialogueBox.SetActive(false);
    }
}
