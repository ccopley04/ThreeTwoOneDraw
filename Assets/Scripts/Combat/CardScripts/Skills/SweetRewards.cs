using UnityEngine;

public class SweetRewards : AbstractSkill
{
    public SweetRewards() : base("Sweet Rewards", 4, ImageLibrary.sweetReward_art, "Draw three cards instantly.", ImageLibrary.sweetRewardsIcon) { }

    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        for (int i = 0; i < 3; i++)
        {
            EncounterControl.Instance.currPlayer.Draw();

        }
        EncounterControl.Instance.reapplyHand();
    }
}