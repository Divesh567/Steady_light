using UnityEngine;

public class ThrowSpike : MonoBehaviour
{
    [SerializeField]
    private GameObject _object;

    private Transform _throwPos;

    [SerializeField]
    private float _timer;

    [SerializeField]
    private float _defaultTimer;

    [SerializeField]
    private float _force;

    [SerializeField]
    private Vector2 _xRange;

    [SerializeField]
    private Vector2 _yRange;

    private void Start()
    {
        _throwPos = transform.GetChild(0).transform;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            Vector2 _throwDirection = new Vector2(Random.Range(_xRange.x, _xRange.y), Random.Range(_yRange.x, _yRange.y));
            GameObject newSpike = Instantiate(_object, _throwPos.position, transform.rotation);
            if (newSpike.GetComponent<Rigidbody2D>())
            {
                newSpike.GetComponent<Rigidbody2D>().AddForce(_throwDirection * _force, ForceMode2D.Impulse);
            }
            _timer = _defaultTimer;
        }
    }
}
