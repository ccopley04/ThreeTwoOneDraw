using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class Deflect : AbstractDefend
{
    //0 argument constructor that makes a basic defense with size small
    // NEEDS ART
    public Deflect() : base("Deflect", 2, ImageLibrary.reflect_art, "Deflect any normal bullets within a SMALL window BACK to sender, destroy super bullets.", Type.Small, ImageLibrary.reflectIcon) { }

    //When the card is played, make the small defense invisible and change movement of bullets within window
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        DefenseManager.Instance.makeInvisible(this.TYPE);
        DefenseManager.Instance.defend(this.TYPE, user, this);
    }

    public override void bulletBlocked(Collider2D other, BulletPrefab bullet)
    {
        if (bullet.isSuper)
        {
            DefenseManager.Destroy(other.gameObject);
        }
        else
        {
            // Change owner of bullet in order to deflect
            bullet.switchOwnership();
            bullet.rendr.flipX = !bullet.rendr.flipX;   // Flip the bullet to face the other direction.
        }
    }
}