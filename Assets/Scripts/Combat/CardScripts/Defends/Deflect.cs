using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class Deflect : AbstractDefend
{
    //0 argument constructor that makes a basic defense with size small
    // NEEDS ART
    public Deflect() : base("Deflect", 4, ImageLibrary.reflect_art, "Deflect any normal bullets within a SMALL window BACK to sender, destroy super bullets.", Type.Small) { }

    //When the card is played, make the small defense invisible and change movement of bullets within window
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        DefenseManager.Instance.makeInvisible(this.TYPE);
        PlayerDefense.DefenseType deflectType = PlayerDefense.DefenseType.Deflect;
        DefenseManager.Instance.defend(this.TYPE, user, deflectType);
    }
}