using UnityEngine;

public class ColDectFaces : MonoBehaviour
{
    [SerializeField]
    private GameObject _faceBall;
    private FaceAnimation _faceAnimation;

    private void Start()
    {
        _faceAnimation = _faceBall.gameObject.GetComponent<FaceAnimation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            _faceAnimation.MakeConfusedFace();
        }
        else if (collision.CompareTag("BoostField"))
        {
            _faceAnimation.MakeHappyFace();
        }
        else if (collision.CompareTag("ForceField"))
        {
            _faceAnimation.MakeSmileFace();
        }
        else if (collision.CompareTag("Spike") || collision.CompareTag("Star"))
        {
            _faceAnimation.MakeAngryFace();
        }
        else if (collision.CompareTag("Objective"))
        {
            _faceAnimation.MakeSuprisedFace();
        }
        else if (collision.CompareTag("Portal"))
        {
            _faceAnimation.MakeConfusedFace();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _faceAnimation.MakeExpressionlessFace();
    }
}
