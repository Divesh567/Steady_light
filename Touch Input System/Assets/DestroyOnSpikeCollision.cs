using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSpikeCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            if(transform.parent.GetComponent<BallAttachControl>() != null)
            {
                transform.parent.GetComponent<BallAttachControl>().DetachObject();
            }
            Destroy(gameObject);
        }
    }
}

