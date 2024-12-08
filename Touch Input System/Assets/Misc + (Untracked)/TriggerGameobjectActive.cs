using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameobjectActive : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _objects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            GetComponent<Collider2D>().enabled = false;
            foreach(GameObject obj in _objects)
            {
                obj.SetActive(true);
            }
        }
    }
}
