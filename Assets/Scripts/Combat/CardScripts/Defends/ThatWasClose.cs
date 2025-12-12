using UnityEngine;

public class ThatWasClose : AbstractDefend
{
    public ThatWasClose() : base("That Was Close", 1, ImageLibrary.default_card,
        "Negate any bullets in a SMALL area. Can override occupied slots.",
        Type.Small, ImageLibrary.smallDefendIcon)
    { }

    public override void use(AbstractPlayer user, float duration, TimeSlot slot)
    {
        DefenseManager.Instance.makeInvisible(this.TYPE);
        DefenseManager.Instance.defend(this.TYPE, user, this);
    }
}
