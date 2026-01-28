using UnityEngine;

public class SmokeScreenPrefab : MonoBehaviour
{
    //If bullet enters smoke screen, 50% chance to be destoryed
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (Random.Range(0,2) == 1) {
                Destroy(other.gameObject);
            }
        }
    }
}