using UnityEngine;
using System.Collections;

public class DefenseManager : MonoBehaviour
{
    //Create a single, static instance of this manager that will be referenced 
    public static DefenseManager Instance { get; private set; }

    //Store the three types of defends, and the enemy defend
    public GameObject smallPlayerDefense;
    public GameObject mediumPlayerDefense;
    public GameObject largePlayerDefense;
    public GameObject smallEnemyDefense;

    //Store all sprite renderers for different defenses
    SpriteRenderer smallDefenseSprite;
    SpriteRenderer mediumDefenseSprite;
    SpriteRenderer largeDefenseSprite;
    SpriteRenderer smallEnemyDefenseSprite;

    //Starting position of enemy defend
    Vector3 enemySmallDefendPos;

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

            //Set the default position of the enmy defense to right in front of the sprite
            enemySmallDefendPos = new Vector3(0, 0, 0);
            enemySmallDefendPos = smallEnemyDefense.transform.position;

            //Retrieve the SpriteRenderersw for all types of defends
            smallDefenseSprite = smallPlayerDefense.GetComponent<SpriteRenderer>();
            mediumDefenseSprite = mediumPlayerDefense.GetComponent<SpriteRenderer>();
            largeDefenseSprite = largePlayerDefense.GetComponent<SpriteRenderer>();
            smallEnemyDefenseSprite = smallEnemyDefense.GetComponent<SpriteRenderer>();
        }
    }

    //Make the defense sprite invisible of the passed size
    public void makeInvisible(Type size)
    {
        if (size == Type.Small)
        {
            smallDefenseSprite.enabled = false;
        }
        else if (size == Type.Medium)
        {
            mediumDefenseSprite.enabled = false;
        }
        else if (size == Type.Large)
        {
            largeDefenseSprite.enabled = false;
        }
    }

    /// <summary>
    /// Defending cards that utilize a hitbox run through this method, which uses PlayerDefense's defend method.
    /// </summary>
    /// <param name="size">Window size</param>
    /// <param name="user">User, player or enemy, doing defense</param>
    /// <param name="defenseType">Type of defense, i.e. deflect / defend etc.</param>
    //Activate the associated Defend() method of the passed defense size, this is called by a card's use() method
    public void defend(Type size, AbstractPlayer user, AbstractDefend defend)
    {
        if (user is Enemy)
        {
            //Retrieve all bullets and set the enmy defense to the correct starting position
            GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
            smallEnemyDefense.transform.position = enemySmallDefendPos;

            //If any bullet is a player bullet, the defend teleports to it
            for (int i = 0; i < allBullets.Length; i++)
            {
                if (!(allBullets[i].GetComponent<BulletPrefab>().shooter is Enemy))
                {
                    smallEnemyDefense.transform.position = allBullets[i].transform.position;
                    break;
                }
            }

            //Activate the defense hitbox and sprite for a short time
            smallEnemyDefense.GetComponent<PlayerDefense>().defend(defend);
            StartCoroutine(show(0.5F, smallEnemyDefenseSprite));
        }
        else
        {
            if (size == Type.Small)
            {
                smallPlayerDefense.GetComponent<PlayerDefense>().defend(defend);
            }
            else if (size == Type.Medium)
            {
                mediumPlayerDefense.GetComponent<PlayerDefense>().defend(defend);
            }
            else if (size == Type.Large)
            {
                largePlayerDefense.GetComponent<PlayerDefense>().defend(defend);
            }
        }
    }

    //Helper method that enables the passed sprite for specified number of seconds
    //Used to flash the sprite of enemy defends when played
    public IEnumerator show(float num, SpriteRenderer sprite)
    {
        sprite.enabled = true;
        yield return new WaitForSeconds(num);
        sprite.enabled = false;
    }

    void Update()
    {
        //Check if time slot to see if it holds a Defend card
        if (WeaponMono.Instance.allSlots != null)
        {
            foreach (TimeSlot slot in WeaponMono.Instance.allSlots)
            {
                if (slot != null && slot.occupyingCard != null && slot.occupyingCard is AbstractDefend)
                {
                    //Save the type of the occupying card
                    Type size = ((AbstractDefend)slot.occupyingCard).TYPE;

                    //Pass the correct defense game object depending on type of hovered card
                    if (size == Type.Small)
                    {
                        defenseFollowMouse(smallPlayerDefense);
                    }
                    else if (size == Type.Medium)
                    {
                        defenseFollowMouse(mediumPlayerDefense);
                    }
                    else if (size == Type.Large)
                    {
                        defenseFollowMouse(largePlayerDefense);
                    }
                }
            }
        }
    }

    //Activates the sprites and forces the passed defense Game Object to follow the mouse
    void defenseFollowMouse(GameObject defense)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        defense.GetComponent<SpriteRenderer>().enabled = true;
        defense.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }
    void OnEnable()
    {
        smallEnemyDefenseSprite.enabled = false;
    }

    public static void Destory(GameObject other)
    {
        Destory(other);
    }
}
