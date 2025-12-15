using UnityEngine;
using System.Collections;

public class CardPrefab : MonoBehaviour
{
    public AbstractCard thisCard;
    public SpriteRenderer rendr;

    private int originalOrder;

    //Sets what card it is and its order in the hand.
    //Uses the passed card to set sprite.
    public void setData(AbstractCard card, int num)
    {

        thisCard = card;
        rendr = gameObject.GetComponent<SpriteRenderer>();
        rendr.size += new Vector2(10f, 10f);

        rendr.sortingOrder = num;
        originalOrder = rendr.sortingOrder;
        rendr.sprite = thisCard.IMAGE;
    }

    public override string ToString()
    {
        return thisCard.ToString();
    }

    //This is called when the player's position in the hand matches the index of this card
    public void selected()
    {
        //Visually indicate this card is selected
        gameObject.transform.position += new Vector3(0, 3F, 0);
        rendr.sortingOrder = 100;
    }

    //This is called when the player's position in the hand NO LONGER matches the index of this card
    public void deselected()
    {
        //Set visuals back to default
        gameObject.transform.position -= new Vector3(0, 3F, 0);
        rendr.sortingOrder = originalOrder;

        //Make any defense sprite no longer visible
        if (thisCard is AbstractDefend)
        {
            DefenseManager.Instance.makeInvisible(((AbstractDefend)thisCard).TYPE);
        }
    }

    void OnMouseEnter()
    {
        EncounterControl.Instance.hoveredCard = this;
        selected();
    }

    void OnMouseExit()
    {
        EncounterControl.Instance.hoveredCard = null;
        deselected();
    }



}
