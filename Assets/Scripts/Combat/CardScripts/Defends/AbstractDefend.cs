using UnityEngine;

public abstract class AbstractDefend : AbstractCard
{
    //Skill specific type
    public readonly Type TYPE;
    //Constructor that calls the AbstractCard constructor
    public AbstractDefend(string name, int cost, Sprite image, string desc, Type type, Sprite icon = null) : base(name, cost, image, desc, icon)
    {
        TYPE = type;
    }

    public void onBulletEnter(Collider2D other)
    {
        BulletPrefab bullet = other.GetComponent<BulletPrefab>();
        if (!(bullet.shooter is Enemy))
        {
            BulletManager.Instance.playerBullet--;
        }
        SoundManager.playSound(SoundType.Defend);
        this.bulletBlocked(other, bullet);
    }

    public virtual void bulletBlocked(Collider2D other, BulletPrefab bullet)
    {
        if (bullet.isSuper)
        {
            bullet.loseSuper();
        }
        else
        {
            DefenseManager.Destroy(other.gameObject);
        }
    }
}

//All possible types of skills, including all sizes of defenses
public enum Type
{
    Small,
    Medium,
    Large,
    Other
}