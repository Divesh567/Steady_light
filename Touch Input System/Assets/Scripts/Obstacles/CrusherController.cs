using System.Collections;
using UnityEngine;

public class CrusherController : MonoBehaviour
{
    [SerializeField]
    private float _defaultTimer;
    [SerializeField]
    private float _timer;

    [SerializeField]
    private float _force;
    [SerializeField]
    private float _resetSpeed;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(StartCrushing());
    }

    private void Update()
    {


    }

    IEnumerator StartCrushing()
    {

        yield return new WaitForSeconds(_timer);

        _rigidbody2D.AddForce(new Vector2(0, _force), ForceMode2D.Impulse);
        _timer = _defaultTimer;
    }

    IEnumerator ResetCrusher()
    {
        StopCoroutine(StartCrushing());
        yield return new WaitForSeconds(0f);

        if (transform.position != _startPos)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _resetSpeed * Time.deltaTime);
            if (transform.position != _startPos)
            {
                StartCoroutine(ResetCrusher());
            }
            else
            {
                StopCoroutine(ResetCrusher());
                StartCoroutine(StartCrushing());
            }
        }
    }


    public void StartResetCrusher()
    {
        StartCoroutine(ResetCrusher());
    }


}
