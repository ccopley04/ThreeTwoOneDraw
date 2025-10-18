using UnityEngine;

public class CombatAnimations : MonoBehaviour
{
    public Animator anim; //Call the Animator

    //for Bill ONLY
    public void BillShoot()
    {
        anim.SetBool("IsShooting", true);
    }
    public void FinishShooting()
    {
        anim.SetBool("IsShooting", false);
    }

    //for Enemies ONLY
    public void EnemyShoot()
    {
        
    }
}
