using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerDefense : MonoBehaviour
{
    PolygonCollider2D hitBox;

    private AbstractDefend defendCard;

    //Retrieve the collider attached to this game object
    void Awake()
    {
        hitBox = gameObject.GetComponent<PolygonCollider2D>();
    }

    //Turn on the collider when this method is called by the DefenseManager, which is called by a card's use() method
    public void defend(AbstractDefend defendCard)
    {
        this.defendCard = defendCard;
        StartCoroutine(colliderActivate(0.3F));
    }

    //Destroy any bullet in the collider when it is activated
    void OnTriggerEnter2D(Collider2D other)
    {
        defendCard.onBulletEnter(other);
    }

    public void disableHitbox(Encounter encounter) {
        hitBox.enabled = false;
    }


    //Activates collider for passed seconds (should usually be as short as possible)
    private IEnumerator colliderActivate(float num)
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        hitBox.enabled = true;
        sprite.enabled = true;
        Color color = sprite.color;
        color.a = 1F;
        sprite.color = color;
        yield return new WaitForSeconds(num);
        color.a = 0.5F;
        sprite.color = color;
        sprite.enabled = false;
        hitBox.enabled = false;
    }
}
