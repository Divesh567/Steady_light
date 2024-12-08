using UnityEngine;

public class FlashingLed : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite2;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
