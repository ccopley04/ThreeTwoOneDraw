using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractBullet : AbstractCard
{
    //Bullet specific instance variables
    private List<Func<int, int>> damageModifiers = new();
    private readonly int baseDamage;
    public Speed speed { get; private set; }
    public readonly Sprite bulletSprite;
    public readonly Sprite superBulletSprite;
    public readonly SoundType sound;
    public readonly string timeSlotInfo;

    //Constructor that calls the AbstractCard constructor
    public AbstractBullet(string name, int cost, Sprite image, string desc, int baseDamage, Speed speed, Sprite bullet,
        Sprite superBullet, SoundType sound, Sprite icon = null) : base(name, cost, image, desc, icon)
    {
        this.baseDamage = baseDamage;
        this.speed = speed;
        this.sound = sound;
        bulletSprite = bullet;
        superBulletSprite = superBullet;
    }

    public int GetDamage()
    {
        return damageModifiers.Aggregate(baseDamage, (acc, fn) => fn(acc));
    }

    public void AddModifier(Func<int, int> modifier)
    {
        damageModifiers.Add(modifier);
    }

    public void UpgradeSpeed()
    {
        this.speed = speed.Upgrade();
    }

    public void DowngradeSpeed()
    {
        this.speed = speed.Downgrade();
    }

    public virtual Vector3 flightPath(float x, float y, float pixelPerSecond)
    {
        return new Vector3(pixelPerSecond / 50F, 0, 0);
    }
    public virtual Vector3 rotation(float x, float y, float pixelPerSecond)
    {
        return new Vector3(0, 0, 0);
    }

    public void setSpeed(Speed newSpeed)
    {
        this.speed = newSpeed;
    }
}

//All possible bullet speeds
public enum Speed
{
    Sluggish = 10, //10 ft per second
    Slow = 15, //15 ft per second
    Average = 20, //20 ft per second
    Fast = 30, // 30 ft per second
    Lightning = 50, // 50 ft per second
}

internal static class SpeedMethods
{
    public static Speed Upgrade(this Speed s1)
    {
        return s1 switch
        {
            Speed.Sluggish => Speed.Slow,
            Speed.Slow => Speed.Average,
            Speed.Average => Speed.Fast,
            _ => Speed.Lightning
        };
    }

    public static Speed Downgrade(this Speed s1)
    {
        return s1 switch
        {
            Speed.Lightning => Speed.Fast,
            Speed.Fast => Speed.Average,
            Speed.Average => Speed.Slow,
            _ => Speed.Sluggish
        };
    }
}
