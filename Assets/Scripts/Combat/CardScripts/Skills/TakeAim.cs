using UnityEngine;
using System;
using System.Timers;

public class TakeAim : AbstractSkill
{
    private static Timer myTimer;

    //0 argument constructor that makes a basic take aim card
    public TakeAim() : base("Take Aim", 3, ImageLibrary.takeAim_art, "The next bullet you fire ignores any defense.", ImageLibrary.takeAimIcon)
    {
    }

    //When this card is played, make the next bullet played "super"
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        EncounterControl.Instance.takeAimActive = true;
        myTimer = new Timer(2000);

        myTimer.Elapsed += OnTimedEvent;

        myTimer.Enabled = true;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        EncounterControl.Instance.takeAimActive = false;
        myTimer.Enabled = false;
    }
}