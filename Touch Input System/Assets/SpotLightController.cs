using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    public List<Transform> _followPoints;
    [SerializeField]
    private GameObject _triggers;
    public int CurrentFollowPoint;

    [SerializeField]
    private GameObject _ball;
    [SerializeField]
    private float _currentDistance;
    [SerializeField]
    private float _allowedDistance;
    [SerializeField]
    private float _closeDistance;
    public float _followSpeed;
    [SerializeField]
    public bool _follow = false;
    public bool _stop = false;
    [SerializeField]
    private bool _loop;
    [SerializeField]
    private float _defaultWaitingTime;
    [SerializeField]
    private float _newWaitingTime;

    private void Start()
    {
        for(int i = 0; i < _triggers.transform.childCount; i++)
        {
            _followPoints.Add(_triggers.transform.GetChild(i).transform);
        }
        StartCoroutine(WaitInPosition(_defaultWaitingTime));
    }

    private void Update()
    {
        if(_follow == true && _stop == false)
        {
            StartFollowing();
        }
        _currentDistance = Vector2.Distance(transform.position, _ball.transform.position);
        if(_currentDistance > _allowedDistance)
        {
            _stop = true;
        }
        if(_stop == true)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                       _ball.transform.position, _followSpeed * Time.deltaTime);
            if (_currentDistance < _closeDistance)
            {
                _stop = false;
            }
        }
    }
    private void StartFollowing() 
    {
        if (CurrentFollowPoint <= _triggers.transform.childCount - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                        _followPoints[CurrentFollowPoint].position, _followSpeed * Time.deltaTime);
        }
        else
        {
            if (_loop)
            {
                CurrentFollowPoint = 0;
            }
            else
            {
                _follow = false;
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpotTrigger"))
        {
            NextFollowPoint();
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        
    }

    public void NextFollowPoint()
    {
        CurrentFollowPoint++;
    }

    IEnumerator WaitInPosition(float _time)
    {
        _follow = false;
        yield return new WaitForSeconds(_time);

        _follow = true;
        StopCoroutine(WaitInPosition(0));
    }

    public void TiggerFollow(bool _trigger)
    {
        _follow = _trigger;
    }

    public void Wait(float newWaitingTime)
    {
        StartCoroutine(WaitInPosition(newWaitingTime));
    }

    public void ResetLight(int _fPoint)
    {
        CurrentFollowPoint = _fPoint;
        _follow = true;
      foreach(Transform trigger in _followPoints)
      {
            trigger.gameObject.GetComponent<Collider2D>().enabled = true;
      }
    }
}
