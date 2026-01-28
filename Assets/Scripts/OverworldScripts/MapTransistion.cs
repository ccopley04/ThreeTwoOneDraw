using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Assertions.Must;
using System.Collections;
public class MapTransistion : MonoBehaviour
{
    public Animator fadeAnim;

    [SerializeField] Transform teleportTargetPosition;
    [SerializeField] PolygonCollider2D mapBoundary;
    CinemachineConfiner2D confiner2D;
    [SerializeField] float changePos;

    [SerializeField] Direction direction;

    [SerializeField] float delayDuration;
    enum Direction { Up, Down, Left, Right, Teleport}

    private void Awake()
    {
        changePos = 5;
        delayDuration = 1f;
        confiner2D = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fadeAnim.Play("FadeToBlack");

            StartCoroutine(DelayFade(collision));

            

        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        if (direction == Direction.Teleport)
        {
            player.transform.position = teleportTargetPosition.position;

            return;
        }

        else
        {
            Vector3 newPos = player.transform.position;

            switch (direction)
            {
                case Direction.Up:
                    newPos.y += changePos;
                    break;
                case Direction.Down:
                    newPos.y -= changePos;
                    break;
                case Direction.Left:
                    newPos.x -= changePos;
                    break;
                case Direction.Right:
                    newPos.x += changePos;
                    break;
            }
            player.transform.position = newPos;
        }
    }

    IEnumerator DelayFade(Collider2D collision)
    {
        yield return new WaitForSeconds(delayDuration);
        confiner2D.BoundingShape2D = mapBoundary;
        UpdatePlayerPosition(collision.gameObject);
        yield return new WaitForSeconds(delayDuration);
        fadeAnim.Play("FadeBack");
    }

}
