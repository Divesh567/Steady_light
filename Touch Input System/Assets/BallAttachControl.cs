using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttachControl : MonoBehaviour
{
    [SerializeField]
    private float _followSpeed;
    [SerializeField]
    private Transform _ballPos;
    public bool _follow;
    private bool _attached = false;

    private GameObject _ballAttachment;
    [SerializeField]
    private GameObject _weight;

    private void Start()
    {
        _ballAttachment = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (_follow )
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                                                    _ballPos.position, _followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            AttachObject(); 
        }
    }

    private void AttachObject()
    {
        transform.parent = _ballPos;
        if (_ballAttachment != null)
        {
            _ballAttachment.GetComponent<HingeJoint2D>().connectedBody = _ballPos.gameObject.GetComponent<Rigidbody2D>();
            _weight.GetComponent<Rigidbody2D>().mass = 3f;
        }
        _follow = false;
        _attached = true;
    }

    public void DetachObject()
    {
        if (_attached)
        {
            transform.parent = null;
            if (_ballAttachment != null)
            {
                _ballAttachment.GetComponent<HingeJoint2D>().connectedBody = null;
                for (int i = 0; i <= transform.childCount - 1; i++)
                {
                    if (i != transform.childCount - 1)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                }
                transform.DetachChildren();
            }
        }
    }
}
