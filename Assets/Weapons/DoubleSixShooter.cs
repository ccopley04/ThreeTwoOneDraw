using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DoubleSixShooter : AbstractWeapon
{
    //0 arg constructor
    //Adds 6 Six Shooter bullet cards and creates two InfoNodes (one of which is special and adds one to costs)
    public DoubleSixShooter() : base(2f)
    {
        for (int i = 0; i < 3; i++)
        {
            this.bullets.Add(new DoubleSixShooterBullet());
        }

        this.nodes[0] = new InfoNode(0);
        this.nodes[1] = new SpecialNode(0);
    }
}
