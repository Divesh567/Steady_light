using System.Collections.Generic;
using UnityEngine;

public class MainBackgroundControl : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _allBackGrounds;
    private Sprite _selectedSprite;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _selectedSprite = _allBackGrounds[Random.Range(0, _allBackGrounds.Count)];
        _spriteRenderer.sprite = _selectedSprite;
    }
}
