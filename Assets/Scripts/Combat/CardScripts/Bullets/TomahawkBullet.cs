using System;
using Unity.VisualScripting;
using UnityEngine;

public class TomahawkBullet : AbstractBullet
{
    //0 argument constructor that makes a basic six shooter bullet
    // TODO: Are we calling this a bullet?
    // TODO: change art
    public TomahawkBullet() : base("Tomahawk Bullet", 2, ImageLibrary.tomahawk_art,
        "Fire one FAST bullet that deals damage on duration it took to cast", 5, Speed.Average, ImageLibrary.tomahawk_bullet,
        ImageLibrary.tomahawk_bullet_super, SoundType.TomahawkBullet, ImageLibrary.tomahawkIcon)
    {
    }

    //When the card is played, call the fire method with this bullet passed
    //Also propogate the shooter to the fire() method to dictate where the bullet spawns/bullet direction
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        AddModifier(x => (int)Math.Floor(duration) * x);
        BulletManager.Instance.fire(user, this, this.sound);
    }

    public override Vector3 flightPath(float x, float y, float pixelPerSecond)
    {
        return new Vector3(pixelPerSecond / 50F, pixelPerSecond / 50F * -0.1F * (x - 2), 0);
    }

    public override Vector3 rotation(float x, float y, float pixelPerSecond)
    {
        return new Vector3(0, 0, -5F);
    }
}