using UnityEngine;

public class SharkVisionControl : MonoBehaviour
{
    private GameObject childObject;

    private void Start()
    {
        childObject = transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (childObject != null)
            {
                if (childObject.GetComponent<SharkSpike>() != null)
                {
                    childObject.GetComponent<SharkSpike>()._stop = false;
                }
                else if (childObject.GetComponent<BallAttachControl>() != null)
                {
                    childObject.GetComponent<BallAttachControl>()._follow = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (childObject != null)
            {
                if (childObject.GetComponent<SharkSpike>() != null)
                {
                    childObject.GetComponent<SharkSpike>()._stop = true;
                }
                else if (childObject.GetComponent<BallAttachControl>() != null)
                {
                    childObject.GetComponent<BallAttachControl>()._follow = false;
                }
            }
        }
    }
}
