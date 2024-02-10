using UnityEngine;
using Unity;
using System.Collections;

public class RotateSelf : MonoBehaviour
{
    [SerializeField]
    private float _waitingTime = 0;
    private bool _waiting = true;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _dir;

    private void Start()
    {
        StartCoroutine(WaitToStart(_waitingTime));
    }
    void Update()
    {
        if (!_waiting)
        {
            transform.Rotate(0, 0, _dir * _speed * Time.deltaTime);
        }
    }

    IEnumerator WaitToStart(float _time)
    {
        yield return new WaitForSeconds(_time);
        _waiting = false;
        StopCoroutine(WaitToStart(_waitingTime));
    }
}
