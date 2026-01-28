using UnityEngine;


public class Bandage : AbstractSkill
{
    private const int Heal = 10;

    public Bandage() : base("Bandage", 3, ImageLibrary.bandage_art, "Heal 10 HP.", ImageLibrary.bandageIcon) { }

    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        user.healDamage(Heal);
    }
}