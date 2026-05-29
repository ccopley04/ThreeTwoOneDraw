using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Sheriff : Enemy
{
    System.Random random = new System.Random();

    public Sheriff() : base(new List<AbstractCard>(), 10000, 1, "Sheriff", 1.0F, 1.0F, 0.0F)
    {
        for (int i = 0; i < 6; i++)
        {
            deck.Add(new SixShooterBullet(Speed.Slow));
        }


    }
}
