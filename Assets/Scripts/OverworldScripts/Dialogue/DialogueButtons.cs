using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueButtons : MonoBehaviour
{
    public NPCInteraction npcScript;
    public Button[] buttons;
    public TMP_Text[] buttonText;

    void Start()
    {
        HideButtons();
    }

    /// <summary>
    /// Show choices for a specific dialogue line index.
    /// NPC should call this using the line index that just finished typing.
    /// </summary>
    public bool SetTextButton(int lineIndex)
    {
        if (npcScript == null || npcScript.lines == null)
        {
            Debug.LogWarning("[DialogueButtons] npcScript/dialogueData not assigned.");
            HideButtons();
            return false;
        }

        var choicesArr = npcScript.choices;

        Debug.Log($"[DialogueButtons] SetTextButton(lineIndex={lineIndex}) " +
                  $"choicesNull={(choicesArr == null)} " +
                  $"choicesLen={(choicesArr != null ? choicesArr.Length : -1)}");

        if (choicesArr == null ||
            lineIndex < 0 ||
            choicesArr.Length <= lineIndex ||
            choicesArr[lineIndex] == null ||
            choicesArr[lineIndex].options == null ||
            choicesArr[lineIndex].options.Length == 0)
        {
            HideButtons();
            return false;
        }

        DialogueOption[] options = choicesArr[lineIndex].options;

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
    
            if (buttons[i] == null) continue;

            if (i < options.Length)
            {
                buttons[i].gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                npcScript.setRunNextLine(false);

                
                if (buttonText != null && i < buttonText.Length && buttonText[i] != null)
                {
                    buttonText[i].text = options[i].text;
                    Debug.Log(options[i].text);
                }
                buttons[i].onClick.RemoveAllListeners();
                DialogueOption option = options[index];
                //int nextIndex = options[i].nextLineIndex;
                //int nextNextIndex = options[i].nextNextLineIndex;

                buttons[index].onClick.AddListener(() =>
                {
                    HideButtons();
                    npcScript.setRunNextLine(true);
                    StartCoroutine(PlayDialogue(option.nextLineIndex, option.nextNextLineIndex));
                });
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }

        return true;
    }

    /// <summary>
    /// Backwards compatible overload (not recommended).
    /// Uses npcScript.dialogueIndex and can be wrong if NPC increments early.
    /// </summary>
    public bool SetTextButton()
    {
        return SetTextButton(npcScript != null ? npcScript.getLineNum() : -1);
    }

    public bool AnyButtonActive()
    {
        foreach (var button in buttons)
        {
            if (button != null && button.gameObject.activeSelf)
                return true;
        }
        return false;
    }

    void HideButtons()
    {
        foreach (var button in buttons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners();
            }
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    private IEnumerator PlayDialogue(int nextIndex, int nextNextIndex)
    {
        if (npcScript == null)
            yield break;

        // Play the immediate branch response line
        yield return StartCoroutine(npcScript.PlayAtIndex(nextIndex));

        // Allow continuing (NPC will block advancement itself if it shows more choices)
        npcScript.setRunNextLine(true);
    }
}