using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SixShooter : AbstractWeapon
{
    //0 arg constructor
    //Adds 6 Six Shooter bullet cards and creates two InfoNodes (one of which is special and adds one to costs)
    public SixShooter() : base(1.5f, "Time Slot 1: No Effect\nTime Slot 2: Add 1 second to cards played here, bullets played here do double damage")
    {
        for (int i = 0; i < 6; i++)
        {
            this.bullets.Add(new SixShooterBullet());
        }

        this.nodes[0] = new InfoNode(0);
        this.nodes[1] = new SpecialNode(1);
    }
}

//A InfoNode subclass test that overrides ifBullet(()
public class SpecialNode : InfoNode
{
    //3 arg constructor that calls base constructor
    public SpecialNode(int diff) : base(diff)
    {
    }

    //If a bullet is played to this node, that bullet does double damage
    public override void ifBullet(AbstractBullet bullet)
    {
        bullet.AddModifier(x => x * 2);
    }
}