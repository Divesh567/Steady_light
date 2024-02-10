using System.Collections;
using UnityEngine;

public class ElectricWaves : MonoBehaviour
{
    private Collider2D _collider2D;
    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        StartCoroutine("WaveEnable");
    }


    IEnumerator WaveEnable()
    {
        yield return new WaitForSeconds(0.5f);
        _collider2D.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            collision.gameObject.GetComponent<DisableSpike>().DisableCollider();
        }
    }
}
