using UnityEngine;

public class ColDectFaces : MonoBehaviour
{
    private FaceAnimation _faceAnimation;

    private void Start()
    {
        _faceAnimation = GetComponent<FaceAnimation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {

        }
        else if (collision.CompareTag("BoostField"))
        {

        }
        else if (collision.CompareTag("ForceField"))
        {

        }
        else if (collision.CompareTag("Spike") || collision.CompareTag("Star"))
        {

        }
        else if (collision.CompareTag("Objective"))
        {

        }
        else if (collision.CompareTag("Portal"))
        {

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }
}
