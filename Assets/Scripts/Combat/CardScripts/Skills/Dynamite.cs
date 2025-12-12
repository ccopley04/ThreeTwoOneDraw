using UnityEngine;


public class Dynamite : AbstractSkill
{
    private const int Damage = 20;

    public Dynamite() : base("Dynamite", 3, ImageLibrary.dynamite_art, "Deal 20 damage to both yourself and the enemy.", ImageLibrary.dynamiteIcon) { }

    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        Debug.Log(this.ICON);
        SoundManager.playSound(SoundType.Explosion);
        EncounterControl.Instance.currPlayer.takeDamage(Damage);
        EncounterControl.Instance.currEnemy.takeDamage(Damage);
    }
}