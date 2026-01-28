using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Tomahawk : AbstractWeapon
{
    //0 arg constructor
    //Adds 6 Six Shooter bullet cards and creates two InfoNodes (one of which is special and adds one to costs)
    public Tomahawk() : base(1f, "Time Slot 1: No Effect\nTime Slot 2: Add 1 second to cards played here\n" +
    "Time Slot 3: Add 2 seconds to cards played here")
    {
        for (int i = 0; i < 4; i++)
        {
            this.bullets.Add(new TomahawkBullet());
        }

        this.nodes[0] = new InfoNode(0);
        this.nodes[1] = new InfoNode(1);
        this.nodes[2] = new InfoNode(2);
    }
}