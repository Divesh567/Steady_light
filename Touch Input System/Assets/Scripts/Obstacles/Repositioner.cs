using System.Collections.Generic;
using UnityEngine;

public class Repositioner : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> _positions;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            collision.transform.position = _positions[1];
        }
        else if (collision.CompareTag("Star"))
        {
            collision.transform.position = _positions[0];
        }
        else if (collision.CompareTag("Ball"))
        {
            collision.transform.position = _positions[0];
        }
    }
}
