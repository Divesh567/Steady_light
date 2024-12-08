using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField]
    private float _time;
    [SerializeField]
    private bool _timer;

    private bool _triggered;

    [SerializeField]
    private List<GameObject> _objects;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (!_triggered)
            {
                if (_objects.Count > 0)
                {
                    for (int i = 0; i < _objects.Count; i++)
                    {
                        _objects[i].gameObject.SetActive(true);
                    }
                }
            }
            _triggered = true;
        }
    }

    private void Update()
    {
        if (_timer)
        {
            _time -= Time.deltaTime;
        }
        if(_time < 0)
        {
            _timer = false;
            if(_objects.Count > 0)
            {
                for (int  i = 0; i < _objects.Count; i++)
                {
                    _objects[i].gameObject.SetActive(true);
                }
            }
            gameObject.SetActive(false);
        }
    }

}
