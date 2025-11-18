using UnityEngine;

public class SleightOfHand : AbstractSkill
{

    //0 argument constructor that calls the AbstractSkill constructor
    //Pass the arguments in that order to base
    public SleightOfHand() : base("Sleight of Hand", 2, ImageLibrary.sleight_art, "Discard your hand and draw that many cards minus 1.") { }

    //Discard the hand of the passed user and draw that many cads minus 1 back
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        int count = user.hand.Count;

        user.DiscardHand();
        for (int i = 0; i < count - 1; i++)
        {
            user.Draw();
        }

        EncounterControl.Instance.reapplyHand();
    }
}
