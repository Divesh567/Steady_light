using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMagic : MonoBehaviour
{
    [SerializeField]
    private GameObject _object;
    private Collider2D _objectCollider;
    private SpriteRenderer _objectSR;

    [SerializeField]
    private List<Transform> points;
    [SerializeField]
    private float _waitTime;

    private void Start()
    {
        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            points.Add(transform.GetChild(i).transform);
        }
        _objectCollider = _object.GetComponent<Collider2D>();
        _objectSR = _object.GetComponent<SpriteRenderer>();

        StartCoroutine(StartMagic());
    }

    IEnumerator StartMagic()
    {
        if (_object != null)
        {
            yield return new WaitForSeconds(_waitTime);

            _objectCollider.enabled = false;
            _objectSR.enabled = false;

            yield return new WaitForSeconds(_waitTime);

            int _objPos = Random.Range(0, points.Count - 1);
            _object.transform.position = transform.GetChild(_objPos).transform.position;

            _objectCollider.enabled = true;
            _objectSR.enabled = true;

            StartCoroutine(StartMagic());
        }
    }
}
