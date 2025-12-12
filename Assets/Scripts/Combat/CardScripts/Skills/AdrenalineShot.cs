using UnityEngine;

public class AdrenalineShot : AbstractSkill
{
    public AdrenalineShot() : base("Adrenaline Shot", 2, ImageLibrary.adrenaline_art, "The next card played to this time slot goes twice as fast.",
    ImageLibrary.adrenalineIcon)
    { }

    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        //Change future cards for the number of future card you want it to affect.
        int futureCards = 1;
        for (int i = 0; i < futureCards; ++i)
        {
            //Compounds pre-existing multipliers
            if (!slot.durationMultipliers.TryAdd(slot.counter + i + 1, 0.5f))
            {
                slot.durationMultipliers[slot.counter + i + 1] = slot.durationMultipliers[slot.counter + 1] * 0.5f;
            }
        }
    }
}