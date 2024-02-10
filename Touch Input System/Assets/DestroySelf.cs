using System.Collections;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    [SerializeField]
    private float _timerTime;


    void Start()
    {
        StartCoroutine(StartDestroySelf());
    }

    IEnumerator StartDestroySelf()
    {
        yield return new WaitForSeconds(_timerTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }
    }
}
