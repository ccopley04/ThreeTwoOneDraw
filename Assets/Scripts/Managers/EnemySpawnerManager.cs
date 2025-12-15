using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using System.Runtime.CompilerServices;
using System.Diagnostics;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField]
    private SpriteLibraryAsset cactusLibrary;
    [SerializeField]
    private SpriteLibraryAsset banditBossLibrary;
    [SerializeField]
    private GameObject enemyObject;
    [SerializeField]
    private GameObject stageBackground;
    [SerializeField]
    private Sprite cactusBackground;
    [SerializeField]
    private Sprite banditBackground;

    void Start()
    {
        EncounterControl.start += SetEnemyVisuals;
    }

    private void SetEnemyVisuals(Encounter encounter)
    {
        Enemy currEnemy = encounter.enemy;
        enemyObject.GetComponent<SpriteLibrary>().spriteLibraryAsset = (currEnemy is Cactus) ? cactusLibrary : banditBossLibrary;
        stageBackground.GetComponent<SpriteRenderer>().sprite = (currEnemy is Cactus) ? cactusBackground : banditBackground;
    }
}