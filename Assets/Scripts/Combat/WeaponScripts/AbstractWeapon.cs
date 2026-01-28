using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractWeapon
{
    //Create instance variables to holg the data of all time slots, all bullets, and the draw timer
    public InfoNode[] nodes;
    public readonly List<AbstractBullet> bullets;
    public readonly float drawDelay;
    public readonly string timeSlotInfo;

    //1 arg constructor
    public AbstractWeapon(float drawDelay, string info)
    {
        this.bullets = new List<AbstractBullet>();
        this.nodes = new InfoNode[10];
        this.drawDelay = drawDelay;
        timeSlotInfo = info;
    }
}

//This class holds the data of a specific time slot
public class InfoNode
{

    //Time slot specific data
    //Added cost
    public readonly int diff;

    //3 arg constructor
    public InfoNode(int diff)
    {
        this.diff = diff;
    }

    //Virtual method that is overridden by any subclass which has behaviour when a bullet occupies this slot
    public virtual void ifBullet(AbstractBullet bullet) { }
}
