using UnityEngine;

public class SharkSpike : MonoBehaviour
{
    [SerializeField]
    private Transform _ballPos;
    private float _movePos;
    [SerializeField]
    private float _speed;
    public bool _stop = false;
    private Vector2 _localScale;

    private void Start()
    {
        _localScale = transform.localScale;
    }
    private void Update()
    {
        if (!_stop)
        {
            _movePos = Mathf.Lerp(transform.position.x, _ballPos.position.x, _speed * Time.deltaTime);
            transform.position = new Vector2(_movePos, transform.position.y);
        }
        if (_ballPos.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(_localScale.x, _localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-_localScale.x, _localScale.y);

        }
    }


}
