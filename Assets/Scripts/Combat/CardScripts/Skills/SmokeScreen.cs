using UnityEngine;

public class SmokeScreen : AbstractSkill
{
    public SmokeScreen() : base("Smoke Screen", 2, ImageLibrary.default_card, "Creates a smoke barrier between the player and enemy. Any bullet that enters has a 50% chance to be destroyed.") { }

    public override void use(AbstractPlayer user, float duration, TimeSlot slot) {
        EncounterControl.Instance.destroySmokeScreen();
    }
}