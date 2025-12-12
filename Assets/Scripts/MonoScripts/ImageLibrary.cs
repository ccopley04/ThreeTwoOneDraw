using Unity.VisualScripting;
using UnityEngine;

public class ImageLibrary : MonoBehaviour
{

    public static Sprite takeAim_art;
    public static Sprite sixShooter_art;
    public static Sprite defend_art;
    public static Sprite default_bullet_concept_art;
    public static Sprite default_superBullet_concept_art;
    public static Sprite default_card;
    public static Sprite dynamite_art;
    public static Sprite tomahawk_art;
    public static Sprite winchester_art;
    public static Sprite adrenaline_art;
    public static Sprite bandage_art;
    public static Sprite ironSteelPlate_art;
    public static Sprite winchester_bullet;
    public static Sprite winchester_bullet_super;
    public static Sprite tomahawk_bullet;
    public static Sprite tomahawk_bullet_super;
    public static Sprite smallDefendIcon;
    public static Sprite sixShooterIcon;
    public static Sprite takeAimIcon;
    public static Sprite winchesterIcon;
    public static Sprite tomahawkIcon;
    public static Sprite sweetReward_art;
    public static Sprite reflect_art;
    public static Sprite adrenalineIcon;
    public static Sprite dynamiteIcon;
    public static Sprite reflectIcon;
    public static Sprite sleightIcon;
    public static Sprite bandageIcon;
    public static Sprite sweetRewardsIcon;



    public static Sprite sleight_art;

    //Created static Sprites for all images located in the CardArt folder 
    void Awake()
    {
        takeAim_art = Resources.Load<Sprite>("CardArt/takeAimCard");
        sixShooter_art = Resources.Load<Sprite>("CardArt/sixShooter");
        defend_art = Resources.Load<Sprite>("CardArt/smallDefendCard");
        default_bullet_concept_art = Resources.Load<Sprite>("CharSprites/Bullets/Six Shooter/SixShooter");
        default_superBullet_concept_art = Resources.Load<Sprite>("CharSprites/Bullets/Six Shooter/TakeAimSixShooter");
        default_card = Resources.Load<Sprite>("CardArt/default_card");
        dynamite_art = Resources.Load<Sprite>("CardArt/dynamiteCard");
        tomahawk_art = Resources.Load<Sprite>("CardArt/tomahawkCard");
        winchester_art = Resources.Load<Sprite>("CardArt/winchestersRifleCard");
        adrenaline_art = Resources.Load<Sprite>("CardArt/adrenalineCard");
        ironSteelPlate_art = Resources.Load<Sprite>("CardArt/ironSteelPlateCard");
        winchester_bullet = Resources.Load<Sprite>("CharSprites/Bullets/Winchester/WinchesterBullet");
        winchester_bullet_super = Resources.Load<Sprite>("CharSprites/Bullets/Winchester/WinchesterBulletSuper");
        tomahawk_bullet = Resources.Load<Sprite>("CharSprites/Bullets/Tomahawk/TomahawkPixel");
        tomahawk_bullet_super = Resources.Load<Sprite>("CharSprites/Bullets/Tomahawk/ChargedTomahawkPixel");
        bandage_art = Resources.Load<Sprite>("CardArt/bandage_art");

        smallDefendIcon = Resources.Load<Sprite>("TimeSlots/Icons/smallDefendIcon");
        sixShooterIcon = Resources.Load<Sprite>("TimeSlots/Icons/sixShooterIcon");
        takeAimIcon = Resources.Load<Sprite>("TimeSlots/Icons/takeAimIcon");
        winchesterIcon = Resources.Load<Sprite>("TimeSlots/Icons/winchesterRifleIcon");
        tomahawkIcon = Resources.Load<Sprite>("TimeSlots/Icons/TomahawkIcon");

        sweetReward_art = Resources.Load<Sprite>("CardArt/sweetRewardsCard");
        reflect_art = Resources.Load<Sprite>("CardArt/reflectCard");
        sleight_art = Resources.Load<Sprite>("CardArt/sleightOfHandCard");

        adrenalineIcon = Resources.Load<Sprite>("TimeSlots/Icons/BlueAdrenalineIcon");
        dynamiteIcon = Resources.Load<Sprite>("TimeSlots/Icons/BlueDynamiteIcon");
        reflectIcon = Resources.Load<Sprite>("TimeSlots/Icons/BlueReflectIcon");
        sleightIcon = Resources.Load<Sprite>("TimeSlots/Icons/BlueSlightOfHandIcon");
        bandageIcon = Resources.Load<Sprite>("TimeSlots/Icons/BlueBandageIcon");
        sweetRewardsIcon = Resources.Load<Sprite>("TimeSlots/Icons/BlueSweetRewardsIcon");

        DontDestroyOnLoad(gameObject);
    }
}
