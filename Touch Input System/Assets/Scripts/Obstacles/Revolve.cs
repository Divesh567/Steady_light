using UnityEngine;

public class Revolve : MonoBehaviour
{
    public bool _triggered;
    private Transform _child;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _childSpeed;

    private void Start()
    {
        _child = transform.GetChild(0);
    }
    private void Update()
    {
        if (_triggered)
        {
            transform.Rotate(0, 0, _speed * Time.deltaTime);
            _child.transform.Rotate(0, 0, _childSpeed * Time.deltaTime, Space.Self);
        }
    }
}
