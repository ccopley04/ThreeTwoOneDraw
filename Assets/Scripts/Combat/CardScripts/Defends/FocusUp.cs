using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class FocusUp : AbstractDefend
{
    //0 argument constructor that makes a basic defense with size small
    public FocusUp() : base("Focus Up", 3, ImageLibrary.default_card, "While this DEFEND is occupying a time slot, all enemy bullets are slowed down. Negate any bullets within a SMALL window.", Type.Small, ImageLibrary.smallDefendIcon) { }

    //When the card is played, make the small defense invisible and destroy any bullets in the collider
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        EncounterControl.Instance.focusedUp = false;
        DefenseManager.Instance.makeInvisible(this.TYPE);
        DefenseManager.Instance.defend(this.TYPE, user, PlayerDefense.DefenseType.Defend);
    }
}
