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

public class EncounterControl : MonoBehaviour
{
    //Event system that gets called whenever the player draws a card
    public delegate void Draw();
    public Draw draw;

    //Event system that is called when E is pressed. Includes methods added here and the Sound Manager's PlayReload
    public delegate void PlayerInput();
    public static PlayerInput EPressed;

    //Event system that is called when either combat starts or ends
    //Many of the methods in the system are from EncounterControl, but other classes can add their own without EncounterControl being coupled to those files
    public delegate void CombatState(Encounter encounter);
    public static CombatState start;
    public static CombatState end;

    //Create a single, static instance of this manager that will be referenced 
    public static EncounterControl Instance { get; private set; }

    //Variables dictated by the passed Encounter
    public Encounter currEncounter;
    public Enemy currEnemy;
    public Player currPlayer;

    public bool playerWonLast;

    //Card Prefab
    [SerializeField]
    private CardPrefab cardBlueprint;

    //Currently selected card
    public CardPrefab hoveredCard { get; set; }

    //All UI elements
    public Slider playerHealthBar;
    public Slider enemyHealthBar;

    [SerializeField]
    private SpriteRenderer discardSpriteRenderer;

    //Smoke Screen
    public GameObject smokeScreen;

    public List<GameObject> allObjects = new List<GameObject>();

    //List of all cards that in the player hand
    public List<CardPrefab> visibleHand;
    public List<AbstractCard> deck;

    //Variable to hold if the next bullet negates enemy defends
    public bool takeAimActive;

    //Holds if the enemy bullets must be half speed
    public bool focusedUp = false;

    public bool combat;

    public TextMeshProUGUI timeSlotInfo;

    [SerializeField]
    private TextMeshProUGUI drawText;
    [SerializeField]
    private TextMeshProUGUI discardPileText;
    [SerializeField]
    private SpriteRenderer drawPile;
    private Sprite cardBack;
    [SerializeField]
    private GameObject drawPrompt;

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
            combat = false;
            playerWonLast = false;
            cardBack = drawPile.sprite;
            draw += AttemptDraw;

            start += setUI;
            end += setUI;
        }
    }

    //Begin the passed Encounter instance
    public void startEncounter(Encounter encounter, bool tutorialActive) //when you start combat
    {
        start?.Invoke(encounter);
        currEnemy = encounter.enemy;
        currPlayer = encounter.player;
        currEncounter = encounter;

        //give player their chosen weapon's bullets
        currPlayer.addBullets(encounter.weapon.bullets);

        visibleHand = new List<CardPrefab>();
        takeAimActive = false;

        currPlayer.damageTaken += updateHealth;
        currEnemy.damageTaken += updateHealth;

        currPlayer.playerDeath += PlayerLoss;
        currEnemy.playerDeath += PlayerWin;


        EPressed += currPlayer.Shuffle;
        EPressed += updateDiscardPile;
        EPressed += updateDeck;

        draw?.Invoke();
        updateHealth();
        reapplyHand();
        StartCoroutine(wait(currEnemy.trySomething() + currEnemy.costAdjust, currEnemy));
    }

    //Destroy all card prefabs and created a new list of prefabs to visually represent the current hand
    public void reapplyHand()
    {
        foreach (CardPrefab card in visibleHand)
        {
            Destroy(card.gameObject);
        }
        visibleHand = new List<CardPrefab>();

        for (int i = 0; i < currEncounter.player.hand.Count; i++)
        {
            CardPrefab newCard = Instantiate(cardBlueprint, cardPosition(i), Quaternion.identity) as CardPrefab;
            newCard.setData(currPlayer.hand[i], i);
            visibleHand.Add(newCard);
        }
    }

    //Calculate the card prefab position depending on hand size
    public Vector2 cardPosition(int num)
    {
        //x-axis offset for UI layout placement
        int offset = -5;

        if (currEncounter.player.hand.Count >= 1)
        {
            return new Vector2(((8 * 1.45F) / (float)(8 - 1) * (2 * num - ((float)(8) / 3)) + num) + offset, -15);
        }
        else
        {
            return new Vector2(0 + offset, -8);
        }

    }

    //Called whenever damage is taken or healed by either player
    private void updateHealth()
    {
        //Calculate the current health of the enemy and player
        playerHealthBar.value = (float)currPlayer.health / currPlayer.maxHealth;
        enemyHealthBar.value = (float)currEnemy.health / currEnemy.maxHealth;

    }

    //Attempt to draw a card, is called every time currEncounter.weapon.drawDelay seconds passes
    private void AttemptDraw()
    {
        //If a draw is possible, draw a card and update visuals
        if (currPlayer.hand.Count < currPlayer.maxHandSize)
        {
            currPlayer.Draw();
            updateDeck();
            reapplyHand();
        }

        //Whether or not a draw was performed, start the timer for the next AttemptDraw() call
        StartCoroutine(wait(currEncounter.weapon.drawDelay, currPlayer));
    }

    //Private method called whenever the deck has card returned or taken from it
    private void updateDeck()
    {
        //Change the sprite to the appropiate visual indicator of the deck size
        if (currEncounter.player.deck.Count == 0)
        {
            drawPrompt.SetActive(true);
            drawPile.sprite = null;
        }
        else
        {
            drawPrompt.SetActive(false);
            drawPile.sprite = cardBack;
        }

        //Change the text to reflect the current deck count
        drawText.text = currEncounter.player.deck.Count.ToString();
    }

    //Method called when the current player's health is zero
    private void PlayerLoss()
    {
        EncounterControl.Instance.playerWonLast = false;
        DisableOverworld.Instance.enableOverworld(true);
        endEncounter(currEnemy);
    }

    //Method called when the current enemy's health is zero
    private void PlayerWin()
    {
        EncounterControl.Instance.playerWonLast = true;
        DisableOverworld.Instance.enableOverworld(true);
        endEncounter(currPlayer);
    }

    //Check every update whether the player draws or plays a card
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Application.Quit();
        }
        if (currEncounter != null && combat)
        {
            //Reshuffle the deck if the player clicks E
            if (Input.GetKeyDown(KeyCode.E))
            {
                EPressed?.Invoke();
            }
            //If a card is selected, the player has an action, and the user clicks the mouse  => Call the card's use() method and discard it
            else if (hoveredCard != null)
            {
                //For any number key pressed (0-9), call the time slot with the associated index
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    playCardToSlot(0);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    playCardToSlot(1);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    playCardToSlot(2);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    playCardToSlot(3);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    playCardToSlot(4);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    playCardToSlot(5);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    playCardToSlot(6);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    playCardToSlot(7);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    playCardToSlot(8);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    playCardToSlot(9);
                }

            }
        }
    }

    //Turn on or off all UI elements
    private void setUI(Encounter encounter)
    {
        combat = !combat;
        bool state = combat;
        foreach (GameObject item in allObjects)
        {
            item.SetActive(state);
        }
    }

    //Turn off player turn for the passed cost
    public IEnumerator wait(float sec, AbstractPlayer player)
    {
        //While there is time left
        float duration = sec;
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
        if (player is Enemy && combat)
        {
            StartCoroutine(wait(currEnemy.trySomething() + currEnemy.costAdjust, currEnemy));
        }
        else if (combat)
        {
            draw?.Invoke();
        }
    }

    //The encounter ends whenever player or enemy reach 0 health
    public void endEncounter(AbstractPlayer winner)
    {
        end?.Invoke(this.currEncounter);

        //Deactivate all visible cards
        GameObject[] visibleCards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject card in visibleCards)
        {
            Destroy(card);
        }
        discardSpriteRenderer.sprite = null;

        //Deactivate all visible bullets
        GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in allBullets)
        {
            Destroy(bullet);
        }

        //Deactive smoke screen
        smokeScreen.SetActive(false);

        DefenseManager.Instance.makeInvisible(Type.Small);
    }

    //Play the current card to the time slot at the provided index
    public void playCardToSlot(int index)
    {

        //If the time slot does not exist or if it has a card already in it
        if (WeaponMono.Instance == null || WeaponMono.Instance.allSlots == null ||
            WeaponMono.Instance.allSlots[index] == null || WeaponMono.Instance.allSlots[index].occupied)
        {
            return;
        }

        if (hoveredCard.thisCard.NAME == "Focus Up")
        {
            EncounterControl.Instance.focusedUp = true;
        }

        TimeSlot targetSlot = WeaponMono.Instance.allSlots[index];

        // Check if slot exists
        if (targetSlot == null)
        {
            return;
        }

        // Handle override for cards that can override occupied slots
        if (targetSlot.occupied)
        {
            if (hoveredCard.thisCard.CanOverrideSlot())
            {
                // Stop the current timer
                if (targetSlot.currentWaitCoroutine != null)
                {
                    StopCoroutine(targetSlot.currentWaitCoroutine);
                }

                // Discard the overridden card without activation
                if (targetSlot.occupyingCard != null)
                {
                    currPlayer.addToDiscardPile(targetSlot.occupyingCard);
                    updateDiscardPile(targetSlot.occupyingCard);
                }

                // Reset slot state
                targetSlot.ResetSlot();
            }
            else
            {
                // Can't play to occupied slot
                return;
            }
        }

        // Start the time slot's timer with the selected card
        targetSlot.currentWaitCoroutine = StartCoroutine(targetSlot.wait(hoveredCard.thisCard.COST, currPlayer, hoveredCard.thisCard));

        //Discard the card
        currPlayer.removeFromHand(hoveredCard.thisCard);
        reapplyHand();

    }

    //Activate smoke screen
    public void setUpSmokeScreen()
    {
        smokeScreen.SetActive(true);
    }

    //Deactive smoke screen
    public void destroySmokeScreen()
    {
        smokeScreen.SetActive(false);
    }

    //Update discard pile sprite with default arguments
    public void updateDiscardPile()
    {
        updateDiscardPile(null);
    }

    //Update discard pile sprite
    public void updateDiscardPile(AbstractCard lastCard)
    {
        discardSpriteRenderer.sprite = lastCard == null ? null : lastCard.IMAGE;
        //Set size to match PlaceHolderDeck, remove this line to display full card size
        discardSpriteRenderer.size = new Vector2(3.875f, 5.85f);

        //Update the current discard pile text to reflect the current count
        discardPileText.text = currEncounter.player.discardPile.Count.ToString();
    }
}



