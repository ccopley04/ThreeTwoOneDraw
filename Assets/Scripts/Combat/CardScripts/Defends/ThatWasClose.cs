using UnityEngine;

public class ThatWasClose : AbstractDefend
{
    public ThatWasClose() : base("That Was Close", 1, ImageLibrary.defend_art, 
        "Negate any bullets in a SMALL area. Can override occupied slots.", 
        Type.Small, ImageLibrary.smallDefendIcon) { }

    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        DefenseManager.Instance.makeInvisible(this.TYPE);
        DefenseManager.Instance.defend(this.TYPE, user, PlayerDefense.DefenseType.Defend);
    }
}
