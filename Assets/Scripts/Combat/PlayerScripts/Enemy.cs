using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public abstract class Enemy : AbstractPlayer
{
    public float defendChance = 1.0f;
    public float bulletChance = 1.0f;
    public float skillChance = 1.0f;
    System.Random random = new System.Random();

    //The value that will offset the seconds it takes for an enemy to play a card
    public int costAdjust { get; private set; }

    //2 arg constuctor that set name to "Enemy"
    public Enemy(List<AbstractCard> deck, int maxHealth, int costAdjust,
    string name, float defendChance, float bulletChance, float skillChance) : base(deck, maxHealth, name)
    {
        this.costAdjust = costAdjust;
        this.defendChance = defendChance;
        this.bulletChance = bulletChance;
        this.skillChance = skillChance;
        maxHandSize = 20;
    }

    int num = 0;
    float cost = 0;

    //Randomly selects a card in the enemy deck, plays it, and returns the amount of seconds until the next enemy turn
    //If the deck runs low on cards, the discard pile is shuffled back into the deck
    public float trySomething()
    {

        if (BulletManager.Instance.playerBullet > 0)
        {
            defendChance *= 2;
        }
        else
        {
            defendChance *= 0;
        }
        string type = RollType();
        suggestCardType(type);
        if ((BulletManager.Instance.playerBullet == 0 && deck[num] is AbstractDefend)) {
            type = "Bullet";
            suggestCardType(type);
        }

        if (deck.Count <= 1 || num >= deck.Count || num < 0)
        {
            this.Shuffle();
            return 1;
        }

        cost = deck[num].COST;
        deck[num].use(this, 0, null);
        discardPile.Add(deck[num]);
        deck.RemoveAt(num);
        updateCardTypeCounts();
        return cost;
    }

    public int bulletCardCount = 0;
    public int skillCardCount = 0;
    public int defendCardCount = 0;

    public void updateCardTypeCounts()
    {
        bulletCardCount = CountCardsOfType("Bullet");
        skillCardCount = CountCardsOfType("Skill");
        defendCardCount = CountCardsOfType("Defend");
    }

    public void suggestCardType(string type)
    {

        if (!"Bullet Skill Defend".Contains(type) || GetCardsOfType(type).Count == 0)
            return;
        List<AbstractCard> options = GetCardsOfType(type);
        num = deck.IndexOf(options[rand.Next(options.Count)]);
    }

    public string RollType()
    {
        float chanceSum = defendChance + bulletChance + skillChance;
        float chanceValue = (float)random.NextDouble();
        if (chanceValue < defendChance / chanceSum)
            return "Defend";
        if (chanceValue < (bulletChance + defendChance) / chanceSum)
            return "Bullet";
        return "Skill";

    }
}
