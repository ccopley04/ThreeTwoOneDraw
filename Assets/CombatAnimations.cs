using UnityEngine;

public class CombatAnimations : MonoBehaviour
{
    public Animator anim; //Call the Animator
    public void Shoot()
    {
        anim.SetBool("IsShooting", true);
    }
    public void FinishShooting()
    {
        anim.SetBool("IsShooting", false);
    }
}
