using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;

public class BulletManager : MonoBehaviour
{
    public CombatAnimations combat_Anim;


    //Create a single, static instance of this manager that will be referenced 
    public static BulletManager Instance { get; private set; }

    public int playerBullet;

    [SerializeField]
    private BulletPrefab bulletBlueprint;

    //If the instance is the first one, it becomes the Instance.
    //Otherwise is is destroyed
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            playerBullet = 0;
        }
    }

    //Creates a new bullet prefab depending of the type of bullet and who fired
    public void fire(AbstractPlayer shooter, AbstractBullet bullet, SoundType sound)
    {
        //Spawns the bullet on the head of the player
        if (!(shooter is Enemy))
        {
            combat_Anim.BillShoot();
            StartCoroutine(delayShooting(0.5F, shooter, bullet, sound));
        }

        //Spawns the bullet on the head of the enemy
        else
        {
            //combat_Anim.Shoot();
            SoundManager.playSound(sound);
            BulletPrefab newBullet = Instantiate(bulletBlueprint,
                EncounterControl.Instance.enemySpritePlaceholder.transform.position + new Vector3(0, 0.5F, 0), Quaternion.Euler(0f, 180f, 0f)) as BulletPrefab;
            newBullet.setData(bullet, shooter, false);
        }
    }

    private IEnumerator delayShooting(float sec, AbstractPlayer shooter, AbstractBullet bullet, SoundType sound)
    {
        float duration = sec;
        //While there is time left
        while (duration > 0)
        {
            //Alter the time by the time since last frame
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                duration = 0;
            }
            yield return null;
        }

        SoundManager.playSound(sound);
        BulletPrefab newBullet = Instantiate(bulletBlueprint,
            EncounterControl.Instance.playerSpritePlaceholder.transform.position + new Vector3(0, 0.5F, 0), Quaternion.identity) as BulletPrefab;
        newBullet.setData(bullet, shooter, EncounterControl.Instance.takeAimActive);
        EncounterControl.Instance.takeAimActive = false;
        playerBullet++;
    }
}
