using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public Enemy enemy;
    Animator animator;
    public EncounterControl encounterController;
    public bool ready = false;
    public System.Random random = new System.Random();
    public RuntimeAnimatorController cactusState;
    public RuntimeAnimatorController bossBanditState;
    public RuntimeAnimatorController EnemySprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = bossBanditState;
        /*
        if (encounterController.currEnemy is Cactus)
        {
            animator.runtimeAnimatorController = cactusState;
        }
        else if (encounterController.currEnemy is BanditBoss)
        {
            //Instead get sprite library of boss BanditState;
            //animator.runtimeAnimatorController = bossBanditState;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready)
        {
            if ((animator != null) && (enemy != null) && encounterController != null)
            {
                ready = true;
            }
        }
        else
        {
            animator.SetInteger("IncomingBullets", BulletManager.Instance.playerBullet);
            animator.SetInteger("BulletCards", enemy.bulletCardCount);
            animator.SetInteger("SkillCards", enemy.skillCardCount);
            animator.SetInteger("DefendCards", enemy.defendCardCount);
            animator.SetFloat("Random", (float)random.NextDouble());
        }
    }

    public void SuggestType(string type)
    {
        enemy.suggestCardType(type);
    }
}
