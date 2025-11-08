using UnityEngine;
using UnityEngine.Events;
public class onTriggerBounds : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onTriggerEnter.Invoke();
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onTriggerExit.Invoke();
        }
    }
}
