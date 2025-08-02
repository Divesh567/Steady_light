using UnityEngine;

public class CrushDetector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ball"))
        {
            GetComponent<CrusherController>();
            GetComponent<AudioSource>().Play();
        }
    }

}
