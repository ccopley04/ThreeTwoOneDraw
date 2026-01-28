using UnityEngine;

public abstract class AbstractSkill : AbstractCard
{
    //Constructor that calls the AbstractCard constructor
    public AbstractSkill(string name, int cost, Sprite image, string desc, Sprite icon = null) : base(name, cost, image, desc, icon) { }
}
