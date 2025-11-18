using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventoryButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cardModeObjects;

    [SerializeField]
    private GameObject[] weaponModeObjects;

    [SerializeField]
    private Image displayRenderer;


    [SerializeField]
    private Image card1Image;
    [SerializeField]
    private Image card2Image;
    [SerializeField]
    private Image card3Image;
    [SerializeField]
    private Image card4Image;
    [SerializeField]
    private TextMeshProUGUI card1Count;
    [SerializeField]
    private TextMeshProUGUI card2Count;
    [SerializeField]
    private TextMeshProUGUI card3Count;
    [SerializeField]
    private TextMeshProUGUI card4Count;
    [SerializeField]
    private Button defualtButton;
    [SerializeField]
    private Button destructButton;
    [SerializeField]
    private Button patientButton;

    [SerializeField]
    private Button sixShooterButton;
    [SerializeField]
    private Button tomahawkButton;
    [SerializeField]
    private Button winchesterButton;
    [SerializeField]
    private TextMeshProUGUI infoText;

    private bool isCardMode = true;
    private string currWeapon = "SixShooter";
    private Sprite currWeaponSprite = ImageLibrary.sixShooter_art;
    private Color thisColor;

    private List<AbstractCard> defaultDeck;
    private List<AbstractCard> destructionDeck;
    private List<AbstractCard> patientDeck;
    private String currDeck;

    void Start()
    {
        currWeaponSprite = ImageLibrary.sixShooter_art;
        thisColor = displayRenderer.color;
        currDeck = "Default";
        currWeapon = "SixShooter";
        infoText.text = "-Shuffles SIX Six Shooter Bullets into your deck.\n-Each card draw has a delay of "
            + OverworldManager.weapon.drawDelay + " seconds.\n-Gives you TWO time slots.\n\t-Time Slot 1: No special Features\n\t"
            + "-Time Slot 2: Bullets played here do double damage.";

        ColorBlock cb = defualtButton.colors;
        cb.normalColor = Color.green;
        cb.highlightedColor = Color.green;
        cb.pressedColor = Color.green;
        cb.selectedColor = Color.green;
        cb.selectedColor = Color.green;

        defualtButton.colors = cb;
        sixShooterButton.colors = cb;



        defaultDeck = new List<AbstractCard>();
        destructionDeck = new List<AbstractCard>();
        patientDeck = new List<AbstractCard>();

        for (int i = 0; i < 3; i++)
        {
            defaultDeck.Add(new TakeAim());
            defaultDeck.Add(new Defend());
            defaultDeck.Add(new Defend());
            destructionDeck.Add(new Dynamite());
            patientDeck.Add(new Defend());
        }
        defaultDeck.Add(new SweetRewards());
        defaultDeck.Add(new SweetRewards());
        defaultDeck.Add(new Bandage());

        for (int i = 0; i < 4; i++)
        {
            destructionDeck.Add(new Deflect());
            destructionDeck.Add(new AdrenalineShot());
            if ((i % 2) == 0)
            {
                destructionDeck.Add(new IronSteelPlate());
                patientDeck.Add(new Deflect());
                patientDeck.Add(new TakeAim());
            }
        }
        patientDeck.Add(new Deflect());
        patientDeck.Add(new SleightOfHand());


    }

    public void resetDeckColors()
    {
        ColorBlock cb = defualtButton.colors;
        cb.normalColor = Color.white;
        cb.normalColor = Color.white;
        cb.highlightedColor = Color.white;
        cb.pressedColor = Color.white;
        cb.selectedColor = Color.white;
        cb.selectedColor = Color.white;
        defualtButton.colors = cb;

        destructButton.colors = cb;

        patientButton.colors = cb;
    }

    public void resetWeaponColors()
    {
        ColorBlock cb = sixShooterButton.colors;
        cb.normalColor = Color.white;
        cb.normalColor = Color.white;
        cb.highlightedColor = Color.white;
        cb.pressedColor = Color.white;
        cb.selectedColor = Color.white;
        cb.selectedColor = Color.white;

        sixShooterButton.colors = cb;

        tomahawkButton.colors = cb;

        winchesterButton.colors = cb;
    }
    public void CardButtonClicked()
    {
        if (!isCardMode)
        {
            foreach (GameObject cardObject in cardModeObjects)
            {
                cardObject.SetActive(true);
            }

            foreach (GameObject weaponObject in weaponModeObjects)
            {
                weaponObject.SetActive(false);
            }
            displayRenderer.sprite = null;
            thisColor.a = 0f;
            displayRenderer.color = thisColor;

            isCardMode = true;
        }
    }

    public void WeaponButtonClicked()
    {
        if (isCardMode)
        {
            foreach (GameObject cardObject in cardModeObjects)
            {
                cardObject.SetActive(false);
            }

            foreach (GameObject weaponObject in weaponModeObjects)
            {
                weaponObject.SetActive(true);
            }

            thisColor.a = 1f;
            displayRenderer.color = thisColor;
            displayRenderer.sprite = currWeaponSprite;
            isCardMode = false;
        }
    }

    public void SixShooterButtonClicked()
    {
        ColorBlock cb = sixShooterButton.colors;
        cb.normalColor = Color.green;
        cb.highlightedColor = Color.green;
        cb.pressedColor = Color.green;
        cb.selectedColor = Color.green;
        cb.selectedColor = Color.green;
        sixShooterButton.colors = cb;

        if (!(currWeapon == "SixShooter"))
        {
            currWeapon = "SixShooter";
            currWeaponSprite = ImageLibrary.sixShooter_art;

            thisColor.a = 1f;
            displayRenderer.color = thisColor;
            displayRenderer.sprite = currWeaponSprite;
            OverworldManager.weapon = new SixShooter();
            infoText.text = "-Shuffles SIX Six Shooter Bullets into your deck.\n-Each card draw has a delay of "
            + OverworldManager.weapon.drawDelay + " seconds.\n-Gives you TWO time slots.\n\t-Time Slot 1: No special Features\n\t"
            + "-Time Slot 2: Bullets played here do double damage.";

        }
    }

    public void TomahawkButtonClicked()
    {
        ColorBlock cb = tomahawkButton.colors;
        cb.normalColor = Color.green;
        cb.highlightedColor = Color.green;
        cb.pressedColor = Color.green;
        cb.selectedColor = Color.green;
        cb.selectedColor = Color.green;
        tomahawkButton.colors = cb;

        if (!(currWeapon == "Tomahawk"))
        {
            currWeapon = "Tomahawk";
            currWeaponSprite = ImageLibrary.tomahawk_art;

            thisColor.a = 1f;
            displayRenderer.color = thisColor;
            displayRenderer.sprite = currWeaponSprite;
            OverworldManager.weapon = new Tomahawk();
            infoText.text = "-Shuffles FOUR Tomahawk Bullets into your deck.\n-Each card draw has a delay of "
            + OverworldManager.weapon.drawDelay + " seconds.\n-Gives you THREE time slots.\n\t-Time Slot 1: No special Features\n\t"
            + "-Time Slot 2: Add 1 second to cards played here.\n\t-Time Slot 3: Add 2 seconds to cards played here.";
        }
    }

    public void WinchesterButtonClicked()
    {
        ColorBlock cb = winchesterButton.colors;
        cb.normalColor = Color.green;
        cb.highlightedColor = Color.green;
        cb.pressedColor = Color.green;
        cb.selectedColor = Color.green;
        cb.selectedColor = Color.green;
        winchesterButton.colors = cb;

        if (!(currWeapon == "Winchester"))
        {
            currWeapon = "Winchester";
            currWeaponSprite = ImageLibrary.winchester_art;

            thisColor.a = 1f;
            displayRenderer.color = thisColor;
            displayRenderer.sprite = currWeaponSprite;
            OverworldManager.weapon = new Winchester();
            infoText.text = "-Shuffles FOURTEEN Winchester Bullets into your deck.\n-Each card draw has a delay of "
            + OverworldManager.weapon.drawDelay + " seconds.\n-Gives you FOUR time slots.\n\t-Time Slot 1 and 2: Add 1 second to cards played here."
            + "\n\t-Time Slot 3 and 4: Add 3 seconds to cards played here. Bullets played here go faster.";
        }
    }

    public void DefaultDeckSelected()
    {

        ColorBlock cb = defualtButton.colors;
        cb.normalColor = Color.green;
        cb.highlightedColor = Color.green;
        cb.pressedColor = Color.green;
        cb.selectedColor = Color.green;
        cb.selectedColor = Color.green;
        defualtButton.colors = cb;

        if (currDeck != "Default")
        {
            card1Image.sprite = ImageLibrary.defend_art;
            card2Image.sprite = ImageLibrary.takeAim_art;
            card3Image.sprite = ImageLibrary.bandage_art;
            card4Image.sprite = ImageLibrary.sweetReward_art;

            card1Count.text = "x6";
            card2Count.text = "x3";
            card3Count.text = "x1";
            card4Count.text = "x2";

            OverworldManager.starterDeck = defaultDeck;

            currDeck = "Default";
        }
    }

    public void DestructionDeckSelected()
    {
        ColorBlock cb = destructButton.colors;
        cb.normalColor = Color.green;
        cb.highlightedColor = Color.green;
        cb.pressedColor = Color.green;
        cb.selectedColor = Color.green;
        cb.selectedColor = Color.green;
        destructButton.colors = cb;

        if (currDeck != "Destruction")
        {
            card1Image.sprite = ImageLibrary.reflect_art;
            card2Image.sprite = ImageLibrary.dynamite_art;
            card3Image.sprite = ImageLibrary.ironSteelPlate_art;
            card4Image.sprite = ImageLibrary.adrenaline_art;

            card1Count.text = "x4";
            card2Count.text = "x3";
            card3Count.text = "x2";
            card4Count.text = "x4";

            OverworldManager.starterDeck = destructionDeck;
            currDeck = "Destruction";
        }
    }

    public void PatientDeckSelected()
    {
        ColorBlock cb = patientButton.colors;
        cb.normalColor = Color.green;
        cb.highlightedColor = Color.green;
        cb.pressedColor = Color.green;
        cb.selectedColor = Color.green;
        cb.selectedColor = Color.green;
        patientButton.colors = cb;

        if (currDeck != "Patient")
        {
            card1Image.sprite = ImageLibrary.takeAim_art;
            card2Image.sprite = ImageLibrary.sleight_art;
            card3Image.sprite = ImageLibrary.defend_art;
            card4Image.sprite = ImageLibrary.reflect_art;

            card1Count.text = "x2";
            card2Count.text = "x1";
            card3Count.text = "x3";
            card4Count.text = "x3";

            OverworldManager.starterDeck = patientDeck;
            currDeck = "Patient";
        }
    }

}
