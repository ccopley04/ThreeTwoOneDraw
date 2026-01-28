using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DefendMedium : AbstractDefend
{
    //0 argument constructor that makes a basic defense with size small
    public DefendMedium() : base("DefendMedium", 3, ImageLibrary.defend_art, "Negate any bullets within a MEDIUM window.", Type.Medium) { }

    //When the card is played, make the MEDIUM defense invisible and destroy any bullets in the collider
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        DefenseManager.Instance.makeInvisible(this.TYPE);
        DefenseManager.Instance.defend(this.TYPE, user, this);
    }
}
