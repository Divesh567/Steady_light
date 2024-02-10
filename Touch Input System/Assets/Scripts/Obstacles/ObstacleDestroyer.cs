using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BoostField") || collision.CompareTag("Spike"))
        {
            Destroy(collision.gameObject);
        }
    }
}
