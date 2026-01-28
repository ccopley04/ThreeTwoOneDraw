using UnityEngine;

public class SixShooterBullet : AbstractBullet
{

    //0 argument constructor that makes a basic six shooter bullet
    public SixShooterBullet(Speed overridenSpeed = Speed.Slow) : base("Six Shooter Bullet", 3, ImageLibrary.sixShooter_art,
        "Fire one SLOW bullet that deals 25 damage", 15, overridenSpeed, ImageLibrary.default_bullet_concept_art,
        ImageLibrary.default_superBullet_concept_art, SoundType.SixShooterBullet, ImageLibrary.sixShooterIcon)
    {
    }

    //When the card is played, call the fire method with this bullet passed
    //Also propogate the shooter to the fire() method to dictate where the bullet spawns/bullet direction
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        BulletManager.Instance.fire(user, this, this.sound);


    }
}