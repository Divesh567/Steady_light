using UnityEngine;

public class MoveFoward : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
}
