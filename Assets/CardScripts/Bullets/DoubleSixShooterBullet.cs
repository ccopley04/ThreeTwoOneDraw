using UnityEngine;

public class DoubleSixShooterBullet : AbstractBullet
{
    //0 argument constructor that makes a basic six shooter bullet
    public DoubleSixShooterBullet() : base("Six Shooter Bullet", 4, ImageLibrary.sixShooter_art,
        "Fire one SLOW bullet that deals 25 damage", 15, Speed.Slow, ImageLibrary.default_bullet_concept_art,
        ImageLibrary.default_superBullet_concept_art, SoundType.SixShooterBullet)
    {
    }

    //When the card is played, call the fire method with this bullet passed
    //Also propogate the shooter to the fire() method to dictate where the bullet spawns/bullet direction
    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        BulletManager.Instance.fire(user, this, this.sound);
        
        // make this spawn lower?
        BulletManager.Instance.fire(user, this, this.sound);
    }
}