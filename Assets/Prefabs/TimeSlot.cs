using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;

public class TimeSlot : MonoBehaviour
{
    //The game objects to visually show what card is occupying this slot and current time
    private TextMesh timerText;
    private SpriteRenderer rendr;

    //The info related to this slot
    private InfoNode thisInfo;

    //Boolean for if the slot is open to another card or not
    public bool occupied { get; private set; }

    //Dictionary to store duration multipliers
    public Dictionary<int, float> durationMultipliers = new Dictionary<int, float>();

    //Count what card is playing next to apply multipliers
    public int counter = 0;

    public Coroutine currentWaitCoroutine;

    private Sprite defaultSprite;

    //Set all the data associated with this slot
    public void setData(InfoNode info, Sprite numberedSprite)
    {
        thisInfo = info;

        GameObject ImageRender = transform.GetChild(0).gameObject;
        GameObject TempTimer = transform.GetChild(1).gameObject;

        timerText = TempTimer.GetComponent<TextMesh>();
        rendr = ImageRender.GetComponent<SpriteRenderer>();
        rendr.sprite = numberedSprite;
        defaultSprite = numberedSprite;
        occupied = false;
    }

    public AbstractCard occupyingCard;
    
    //Public method to reset slot state when overriding
    public void ResetSlot()
    {
        occupied = false;
        occupyingCard = null;
        if (rendr != null)
        {
            rendr.sprite = defaultSprite;
        }
        currentWaitCoroutine = null;
    }
    
    //Start this slot's timer based on the provided cards cost
    public IEnumerator wait(float sec, AbstractPlayer user, AbstractCard selectedCard)
    {
        //Make the slot occupied
        occupied = true;
        occupyingCard = selectedCard;

        //If its a bullet, call the node's corresponding method
        if (selectedCard is AbstractBullet)
        {
            thisInfo.ifBullet((AbstractBullet)selectedCard);
        }

        //If card is a Smoke Screen, set up the smoke screen
        if (occupyingCard.NAME == "Smoke Screen")
        {
            EncounterControl.Instance.setUpSmokeScreen();
        }

        //Set the image to the sprite of the occupying card
        rendr.sprite = selectedCard.ICON;

        //Calculate duration
        float duration = sec + thisInfo.diff;
        if (durationMultipliers.ContainsKey(counter))
        {
            duration *= durationMultipliers[counter];
        }

        var totalDuration = duration;

        //While there is time left
        while (duration > 0)
        {
            //Alter the time by the time since last frame
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                duration = 0;
            }

            //Updated the temp timer
            if (timerText != null)
            {
                timerText.text = Math.Round(duration, 1) + "";
            }

            yield return null;
        }

        //Use the passed cards method
        if (selectedCard != null && EncounterControl.Instance.combat)
        {
            selectedCard.use(user, totalDuration, this);
            ++counter;
            EncounterControl.Instance.currPlayer.addToDiscardPile(selectedCard);
            EncounterControl.Instance.updateDiscardPile(selectedCard);
        }

        //Reset the slot
        ResetSlot();
    }

}
