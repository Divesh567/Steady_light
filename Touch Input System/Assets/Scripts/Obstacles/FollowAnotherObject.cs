using UnityEngine;

public class FollowAnotherObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToFollow;
    [SerializeField]
    private Vector2 _offset;

    private void Update()
    {
        transform.position = new Vector2(_objectToFollow.transform.position.x + _offset.x, _objectToFollow.transform.position.y + _offset.y);
    }
}
