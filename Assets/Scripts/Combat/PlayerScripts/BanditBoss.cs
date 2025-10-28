using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class BanditBoss : Enemy
{
    System.Random random = new System.Random();

    public BanditBoss() : base(new List<AbstractCard>(), 125, 1, "Bandit Boss", 1.0F, 1.0F, 1.0F)
    {
        for (int i = 0; i < 6; i++)
        {
            deck.Add(new Defend());
            deck.Add(new Bandage());
            deck.Add(new DoubleSixShooterBullet(Speed.Fast));
        }

        deck.Add(new Dynamite());

    }
}
