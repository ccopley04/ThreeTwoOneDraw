using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Assertions.Must;

public class MapTransistion : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundary;
    CinemachineConfiner2D confiner2D;
    [SerializeField] float changePos;
    
    [SerializeField] Direction direction;


    enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        changePos = 5;
        confiner2D = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner2D.BoundingShape2D = mapBoundary;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
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
