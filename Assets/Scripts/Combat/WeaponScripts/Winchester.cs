public class Winchester : AbstractWeapon
{
    public Winchester() : base(0.8f, "Time Slot 1 and 2: Add 1 second to cards played here\n" +
    "Time Slot 3 and 4: Add 3 seconds to cards played here, and speed up bullets played here")
    {
        for (var i = 0; i < 14; i++)
        {
            this.bullets.Add(new WinchesterBullet());
        }

        this.nodes[0] = new InfoNode(1);
        this.nodes[1] = new InfoNode(1);
        this.nodes[2] = new SpeedUpNode(3);
        this.nodes[3] = new SpeedUpNode(3);
    }
}

public class SpeedUpNode : InfoNode
{
    //3 arg constructor that calls base constructor
    public SpeedUpNode(int diff) : base(diff)
    {
    }

    //If a bullet is played to this node, that bullet does double damage
    public override void ifBullet(AbstractBullet bullet)
    {
        bullet.UpgradeSpeed();
    }
}