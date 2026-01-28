using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HealthBarHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //The text for this respective health bar
    public TextMeshProUGUI text;

    //Show the text when this health bar is hovered over
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.enabled = true;
    }

    //Delete the text when this health bar is not hovered over
    public void OnPointerExit(PointerEventData eventData)
    {
        text.enabled = false;
    }

    //Every update, calculate the current health depending on if this the player or enemy health bar
    public void Update()
    {
        text.text = (gameObject.name == "PlayerSlider") ? (EncounterControl.Instance.currPlayer.health + " / " + EncounterControl.Instance.currPlayer.maxHealth) :
                                                        (EncounterControl.Instance.currEnemy.health + " / " + EncounterControl.Instance.currEnemy.maxHealth);
    }

    public void Awake()
    {
        text.enabled = false;
    }
}
