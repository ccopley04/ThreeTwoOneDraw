using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;

public class BulletManager : MonoBehaviour
{
    public CombatAnimations combat_Anim;
    public CombatAnimations enemy_Anim;

    public GameObject enemySpritePlaceholder;
    public GameObject playerSpritePlaceholder;


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
            EncounterControl.start += reset;
        }
    }

    //Creates a new bullet prefab depending of the type of bullet and who fired
    public void fire(AbstractPlayer shooter, AbstractBullet bullet, SoundType sound)
    {
        //Spawns the bullet on the head of the player
        if (!(shooter is Enemy))
        {
            combat_Anim.BillShoot();
            StartCoroutine(delayPlayerShooting(0.5F, shooter, bullet, sound));
        }

        //Spawns the bullet on the head of the enemy
        else
        {
            BulletPrefab newBullet;
            enemy_Anim.EnemyShoot();
            if (bullet is DoubleSixShooterBullet)
            {
                SoundManager.playSound(sound);
                newBullet = Instantiate(bulletBlueprint,
                enemySpritePlaceholder.transform.position + new Vector3(0, 0.4F, 0), Quaternion.Euler(0f, 180f, 0f)) as BulletPrefab;
                newBullet.setData(bullet, shooter, false);
                bullet.setSpeed(Speed.Average);
                StartCoroutine(delayEnemyShooting(1.1F, shooter, bullet, sound));

            }
            else
            {
                SoundManager.playSound(sound);
                newBullet = Instantiate(bulletBlueprint,
                    enemySpritePlaceholder.transform.position + new Vector3(0, 0.4F, 0), Quaternion.Euler(0f, 180f, 0f)) as BulletPrefab;
                newBullet.setData(bullet, shooter, false);
            }
        }
    }

    private IEnumerator delayPlayerShooting(float sec, AbstractPlayer shooter, AbstractBullet bullet, SoundType sound)
    {
        bool wasTakeAim = EncounterControl.Instance.takeAimActive;
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
        BulletPrefab newBullet2 = Instantiate(bulletBlueprint,
            playerSpritePlaceholder.transform.position + new Vector3(0, 0.5F, 0), Quaternion.identity) as BulletPrefab;
        newBullet2.setData(bullet, shooter, wasTakeAim);
        EncounterControl.Instance.takeAimActive = false;
        playerBullet++;
    }

    private IEnumerator delayEnemyShooting(float sec, AbstractPlayer shooter, AbstractBullet bullet, SoundType sound)
    {
        bool wasTakeAim = EncounterControl.Instance.takeAimActive;
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
        BulletPrefab newBullet2 = Instantiate(bulletBlueprint,
            enemySpritePlaceholder.transform.position + new Vector3(0, 0.5F, 0), Quaternion.Euler(0f, 180f, 0f)) as BulletPrefab;
        newBullet2.setData(bullet, shooter, false);
    }

    private void reset(Encounter encounter) {
        playerBullet = 0;
    }

}
