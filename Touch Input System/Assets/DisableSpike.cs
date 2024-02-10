using System.Collections;
using UnityEngine;


public class DisableSpike : MonoBehaviour
{
    private float _disableTime = 6.5f;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;
    private Color32 _defaultcolor;
    private Color32 _disablecolor = new Color32(135, 135, 135, 150);
    private UnityEngine.Rendering.Universal.Light2D _light2D;
    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultcolor = _spriteRenderer.color;
        if (GetComponent<UnityEngine.Rendering.Universal.Light2D>() != null)
        {
            _light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        }
    }

    public void DisableCollider()
    {
        StartCoroutine("Disable");
    }

    IEnumerator Disable()
    {
        _collider2D.enabled = false;
        if (_light2D != null)
        {
            _light2D.enabled = false;
        }
        _spriteRenderer.color = _disablecolor;
        yield return new WaitForSeconds(_disableTime);
        _collider2D.enabled = true;
        if (_light2D != null)
        {
            _light2D.enabled = true;
        }
        _spriteRenderer.color = _defaultcolor;
        StopCoroutine("Disable");
    }
}
