using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public UnityEvent<Collider2D> onTriggerEnterEvent;
    public string obstacleName;

    public void InitObstacle(string Name)
    {
        obstacleName = name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            onTriggerEnterEvent?.Invoke(collision);
        }
    }
}
