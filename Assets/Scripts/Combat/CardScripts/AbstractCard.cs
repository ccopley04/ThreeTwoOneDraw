using System;
using UnityEngine;
public abstract class AbstractCard
{
    //All instance variables needed by all cards
    public readonly string NAME;
    public readonly float COST;
    public readonly Sprite IMAGE;
    private readonly string DESC;

    public readonly Sprite ICON;

    public readonly string[] SkillCards = {
        "Take Aim",
        "Grenade"
    };
    public readonly string[] BulletCards = {
        "Six Shooter Bullet",
        "Tomahawk Bullet",
        "Winchester Bullet"
    };
    public readonly string[] DefendCards = {
        "Defend",
        "That Was Close"
    };

    public readonly string[] OverrideCards = {
        "That Was Close"
    };

    //4 argument constructor
    public AbstractCard(string name, float cost, Sprite image, string description, Sprite icon = null)
    {
        NAME = name;
        COST = cost;
        IMAGE = image;
        DESC = description;
        ICON = icon;
    }

    //The tostring for all cards, simply returns the card name
    public override string ToString()
    {
        return NAME;
    }

    public string GetCardType()
    {
        if (Array.IndexOf(SkillCards, NAME) != -1)
            return "Skill";
        if (Array.IndexOf(BulletCards, NAME) != -1)
            return "Bullet";
        if (Array.IndexOf(DefendCards, NAME) != -1)
            return "Defend";
        return "Unknown";
    }

    public bool IsCardType(String type)
    {
        return type == GetCardType();
    }

    public bool CanOverrideSlot()
    {
        return System.Array.IndexOf(OverrideCards, NAME) != -1;
    }

    //Abstract method to be implement by specific cards
    //The method requires the user of the card (could be player or enemies) 
    // so card behaviour can depend on type of user playing said card
    public abstract void use(AbstractPlayer user, float duration, TimeSlot slot);

}
