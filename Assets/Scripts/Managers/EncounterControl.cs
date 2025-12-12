using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using System.Runtime.CompilerServices;

public class EncounterControl : MonoBehaviour
{
    //Temp variable that says whether the mouse controls hand traversal or not
    public bool mouseMode = true;

    //Create a single, static instance of this manager that will be referenced 
    public static EncounterControl Instance { get; private set; }

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
    public GameObject playerSpritePlaceholder;
    public GameObject enemySpritePlaceholder;
    public GameObject playerHealthBarSprite;
    public GameObject enemyHealthBarSprite;

    public Slider playerHealthBar;
    public Slider enemyHealthBar;

    [SerializeField]
    private SpriteRenderer discardSpriteRenderer;

    //Smoke Screen
    public GameObject smokeScreen;

    public List<GameObject> allObjects = new List<GameObject>();

    //Holds the index of the card that is being selected
    public int position;

    //List of all cards that in the player hand
    public List<CardPrefab> visibleHand;
    public List<AbstractCard> deck;

    //Relays if the enemy can currently play a card or perform an action
    public bool enemyTurn;

    //Relays if the player can currently draw a card
    public bool drawTurn;

    //Variable to hold if the next bullet negates enemy defends
    public bool takeAimActive;

    //Holds if the enemy bullets must be half speed
    public bool focusedUp = false;

    public bool combat;

    //Determines if we are in the tutorial or not
    public bool tutorial;

    public bool bulletPlayed = false;

    public bool defendPlayed = false;

    public bool takeAimPlayed = false;

    public bool deckRanOut = false;
    public bool battleStarted = false;

    public GameObject popUp;

    public TextMeshProUGUI textPopUp;

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
        }
    }

    //Begin the passed Encounter instance
    public void startEncounter(Encounter encounter, bool tutorialActive) //when you start combat
    {
        MusicManager.playSound(MusicType.Tutorial, 0.4F);
        MusicManager.audioSource.loop = true;
        setUI(true);
        tutorial = tutorialActive;
        currEnemy = encounter.enemy;
        currPlayer = encounter.player;
        currEncounter = encounter;

        enemyObject.GetComponent<SpriteLibrary>().spriteLibraryAsset = (currEnemy is Cactus) ? cactusLibrary : banditBossLibrary;
        stageBackground.GetComponent<SpriteRenderer>().sprite = (currEnemy is Cactus) ? cactusBackground : banditBackground;

        //give player their chose weapon, bullets, and time slots
        currPlayer.addBullets(encounter.weapon.bullets);
        WeaponMono.Instance.activateWeapon(encounter.weapon);
        timeSlotInfo.text = encounter.weapon.timeSlotInfo;

        //EnemyStateMachine enemyStateMachine = enemySpritePlaceholder.GetComponent<EnemyStateMachine>();
        //enemyStateMachine.encounterController = this;
        //enemyStateMachine.enemy = currEnemy;

        position = -1;
        visibleHand = new List<CardPrefab>();

        enemyTurn = true;
        drawTurn = true;

        takeAimActive = false;

        reapplyHand();

        if (tutorial && !battleStarted)
        {
            popUp.SetActive(true);
            textPopUp.enabled = true;
            textPopUp.text = "\nWelcome to the duel! You can use your mouse to select cards and play them to your time slots (those" +
            " little circles on the left)! Just the press the corresponding number key! Your cards will " +
            "sit there until their time is up and then they'll activate! The time it takes is located on the top left of the cards!" +
            " Some time slots have special powers, they are detailed on the right! What are you waiting for? Try a few cards out! (Press Return)";
            battleStarted = true;
            StartCoroutine(endTutorialPopUp(popUp, textPopUp));
        }
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

    //Check every update whether the player draws or plays a card
    void Update()
    {
        if (combat)
        {
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
            drawText.text = currEncounter.player.deck.Count.ToString();
            discardPileText.text = currEncounter.player.discardPile.Count.ToString();

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Application.Quit();
            }

            //Calculate the current health of the enemy and player
            playerHealthBar.value = (float)currPlayer.health / currPlayer.maxHealth;
            enemyHealthBar.value = (float)currEnemy.health / currEnemy.maxHealth;

            if (currPlayer.health == 0)
            {
                EncounterControl.Instance.playerWonLast = false;
                DisableOverworld.Instance.enableOverworld(true);
                endEncounter(currEnemy);
            }
            else if (currEnemy.health == 0)
            {
                EncounterControl.Instance.playerWonLast = true;
                DisableOverworld.Instance.enableOverworld(true);
                endEncounter(currPlayer);
            }

            //If the enemy has a turn, randomly pick an action and pause the enemy turn for the returned seconds
            if (enemyTurn)
            {
                StartCoroutine(wait(currEnemy.trySomething() + currEnemy.costAdjust, currEnemy));
            }

            if (drawTurn && currPlayer.hand.Count < currPlayer.maxHandSize)
            {
                currPlayer.Draw();
                StartCoroutine(wait(currEncounter.weapon.drawDelay, currPlayer));
                if (tutorial)
                {
                    if (!deckRanOut && currPlayer.deck.Count == 0)
                    {
                        popUp.SetActive(true);
                        textPopUp.enabled = true;
                        textPopUp.text = "You've deck has run dry! You won't draw anymore cards because they are all discarded!" +
                        " Don't worry! Just press E and reload! All the cards in the discard will return to the draw pile! (Press Return)";
                        deckRanOut = true;
                        StartCoroutine(endTutorialPopUp(popUp, textPopUp));
                    }
                }
                reapplyHand();
            }
            if (currEncounter != null)
            {
                //Exit the card selection if the player clicks S
                if (Input.GetKeyDown(KeyCode.E))
                {
                    discardSpriteRenderer.sprite = null;
                    currPlayer.Shuffle();
                    SoundManager.playSound(SoundType.Reload);
                }
                //Exit the card selection if the player clicks S
                else if (Input.GetKeyDown(KeyCode.DownArrow) && !mouseMode)
                {
                    position = -1;
                    if (hoveredCard != null)
                    {
                        hoveredCard.deselected();
                        hoveredCard = null;
                    }
                }
                //Move the index of the selected card right when the playef clicks D
                else if (Input.GetKeyDown(KeyCode.RightArrow) && !mouseMode)
                {
                    if (position == -1 || position == currPlayer.hand.Count - 1)
                    {
                        position = 0;
                    }
                    else
                    {
                        position += 1;
                    }
                    //Move the index of the selected card left when the playef clicks A
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && !mouseMode)
                {
                    if (position == -1 || position == 0)
                    {
                        position = currPlayer.hand.Count - 1;
                    }
                    else
                    {
                        position -= 1;
                    }
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

            //Visually show which card is selected and set hoveredCard to the currently selected card
            if (position < visibleHand.Count && position >= 0)
            {
                if (hoveredCard != null)
                {
                    hoveredCard.deselected();
                }
                hoveredCard = visibleHand[position];
                hoveredCard.selected();
            }
        }

    }

    //Turn on or off all UI elements
    private void setUI(bool state)
    {
        //deckPlaceholder.SetActive(state);
        //discardPilePlaceholder.SetActive(state);

        combat = state;
        foreach (GameObject item in allObjects)
        {
            item.SetActive(state);
        }
    }

    //Turn off player turn for the passed cost
    public IEnumerator wait(float sec, AbstractPlayer player)
    {
        if (player is Enemy)
        {
            EncounterControl.Instance.enemyTurn = false;
        }
        else
        {
            EncounterControl.Instance.drawTurn = false;
        }

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
        if (player is Enemy)
        {
            EncounterControl.Instance.enemyTurn = true;
        }
        else
        {
            EncounterControl.Instance.drawTurn = true;
        }
    }

    //The encounter ends whenever player or enemy reach 0 health
    public void endEncounter(AbstractPlayer winner)
    {
        MusicManager.audioSource.Stop();
        MusicManager.playSound(MusicType.Theme, 0.5F);
        MusicManager.audioSource.loop = true;

        setUI(false);

        //Deactivate all visible cards
        GameObject[] visibleCards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject card in visibleCards)
        {
            Destroy(card);
        }
        discardSpriteRenderer.sprite = null;

        //Deactivate all time slots
        GameObject[] visibleSlots = GameObject.FindGameObjectsWithTag("TimeSlot");
        foreach (GameObject slot in visibleSlots)
        {
            Destroy(slot);
        }

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
        if (WeaponMono.Instance.allSlots[index] == null || WeaponMono.Instance.allSlots[index].occupied)
        {
            return;
        }

        if (tutorial)
        {
            if (!bulletPlayed && hoveredCard.thisCard.GetCardType() == "Bullet")
            {
                popUp.SetActive(true);
                textPopUp.enabled = true;
                textPopUp.text = "You've played a Bullet! This town ain't big enough for a cowboy and a cactus! " +
                "When the timer hits zero, you'll fire a bullet straight for that darn cactus! But, he might be able to block it! (Press Return)";
                bulletPlayed = true;
                StartCoroutine(endTutorialPopUp(popUp, textPopUp));
            }
            if (!defendPlayed && hoveredCard.thisCard.GetCardType() == "Defend")
            {
                popUp.SetActive(true);
                textPopUp.enabled = true;
                textPopUp.text = "You've played a Defend! When the timer hits zero, any bullets within your mouse cursor will be blocked!" +
                " No clicking required! Just make sure your mouse is in the right space when zero comes around! (Press Return)";
                defendPlayed = true;
                StartCoroutine(endTutorialPopUp(popUp, textPopUp));
            }
            if (!takeAimPlayed && hoveredCard.thisCard.NAME == "Take Aim")
            {
                popUp.SetActive(true);
                textPopUp.enabled = true;
                textPopUp.text = "You've played a Take Aim! Any bullets that are fired within the two seconds after this card activates are supercharged!" +
                " These superchared bullets won't be so easy for this cactus to defend! (Press Return)";
                takeAimPlayed = true;
                StartCoroutine(endTutorialPopUp(popUp, textPopUp));
            }
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

        //Reapply the visuals for the player's hand
        position = (position == 0) ? position + 1 : position - 1;
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

    //Update discard pile sprite
    public void updateDiscardPile(AbstractCard lastCard)
    {
        discardSpriteRenderer.sprite = lastCard.IMAGE;
        //Set size to match PlaceHolderDeck, remove this line to display full card size
        discardSpriteRenderer.size = new Vector2(3.875f, 5.85f);
    }
    public IEnumerator endTutorialPopUp(GameObject popUp, TextMeshProUGUI textPopUp)
    {
        Time.timeScale = 0;
        combat = false;
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        popUp.SetActive(false);
        textPopUp.enabled = false;
        Time.timeScale = 1;
        combat = true;
    }
}



