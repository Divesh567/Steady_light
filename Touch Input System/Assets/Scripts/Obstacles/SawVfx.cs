using UnityEngine;

public class SawVfx : MonoBehaviour
{
    [SerializeField]
    private Vector3 _noFlip;
    [SerializeField]
    private Vector3 _flip;
    private Vector3 _hFlip;
    private bool _isFlipped;

    private void Start()
    {

    }
    public void FlipObject()
    {
        if (!_isFlipped)
        {
            transform.eulerAngles = _flip;
            _isFlipped = true;
        }
        else
        {
            transform.eulerAngles = _noFlip;
            _isFlipped = false;
        }
    }

    public void FlipOnlyX()
    {
        transform.eulerAngles = new Vector3(180, 180, 180);

    }
}
