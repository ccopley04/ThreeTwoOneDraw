using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Defend : AbstractDefend
{
    //0 argument constructor that makes a basic defense with size small
    public Defend() : base("Defend", 1, ImageLibrary.defend_art, "Negate any bullets within a SMALL window.", Type.Small, ImageLibrary.smallDefendIcon) { }

    //When the card is played, make the small defense invisible and destroy any bullets in the collider
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        DefenseManager.Instance.makeInvisible(this.TYPE);
        DefenseManager.Instance.defend(this.TYPE, user, this);
    }
}
