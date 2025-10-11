using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    public AbstractBullet thisBullet { get; private set; }
    public SpriteRenderer rendr { get; private set; }

    private float pixelPerSecond;

    //The length of the field in pixels and feet
    private static float pixelDistance = 15.91F;
    private static float feetDistance = 50F;
    private static float conversion = pixelDistance / feetDistance;

    public AbstractPlayer shooter { get; private set; }

    //Holds whether the bullet negates the first defend, this means a Take Aim was played
    public bool isSuper { get; private set; }

    //Assigns this bullet based on the passed argument.
    //Assigns the speed and bullet sprite based on bullet type (speed is negative is it is the enemy firing)
    public void setData(AbstractBullet bullet, AbstractPlayer shooter, bool isSuper)
    {
        thisBullet = bullet;
        this.shooter = shooter;
        this.isSuper = isSuper;

        rendr = gameObject.GetComponent<SpriteRenderer>();

        //Loads the correct sprite depending on if the bullet is super
        if (isSuper)
        {
            rendr.sprite = thisBullet.superBulletSprite;
        }
        else
        {
            rendr.sprite = thisBullet.bulletSprite;
        }

        setPixelPerSecond();
    }

    //Calls this update every 0.02 seconds.
    //Moves the bullet by the speed and destroys the bullet when it hits its target
    void FixedUpdate()
    {
        if (EncounterControl.Instance == null)
        {
            return;
        }

        if (EncounterControl.Instance.focusedUp == true && (shooter is Enemy))
        {
            gameObject.transform.position += (thisBullet.flightPath(gameObject.transform.position.x, gameObject.transform.position.y, pixelPerSecond)/2);
        } 
        else 
        {
            gameObject.transform.position += thisBullet.flightPath(gameObject.transform.position.x, gameObject.transform.position.y, pixelPerSecond);
        }

        gameObject.transform.Rotate(thisBullet.rotation(gameObject.transform.position.x, gameObject.transform.position.y, pixelPerSecond));

        if (!(shooter is Enemy) && this.transform.position.x >= EncounterControl.Instance.enemySpritePlaceholder.transform.position.x)
        {
            EncounterControl.Instance.currEnemy.takeDamage(thisBullet.GetDamage());
            BulletManager.Instance.playerBullet--;
            Destroy(gameObject);
        }
        else if ((shooter is Enemy) && this.transform.position.x <= EncounterControl.Instance.playerSpritePlaceholder.transform.position.x - 1)
        {
            EncounterControl.Instance.currPlayer.takeDamage(thisBullet.GetDamage());
            Destroy(gameObject);
        }
    }

    //Converts feet to actual pixel
    private static float feetToPixel(float feet)
    {
        return feet * conversion;
    }

    //Removes super status by altering sprite and boolean
    public void loseSuper()
    {
        isSuper = false;
        rendr.sprite = thisBullet.bulletSprite;
    }

    public void switchOwnership()
    {
        if (shooter is Enemy)
        {
            shooter = EncounterControl.Instance.currPlayer;
        }
        else
        {
            shooter = EncounterControl.Instance.currEnemy;
        }
        setPixelPerSecond();
    }

    private void setPixelPerSecond()
    {
        pixelPerSecond = !(shooter is Enemy) ? feetToPixel((float)thisBullet.speed) : -1 * feetToPixel((float)thisBullet.speed);
    }

}
