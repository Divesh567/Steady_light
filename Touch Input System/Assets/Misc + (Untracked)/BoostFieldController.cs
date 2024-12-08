using System.Collections;
using UnityEngine;


public class BoostFieldController : MonoBehaviour
{
    [SerializeField]
    private Color _activeColor;
    [SerializeField]
    private Color _inactiveColor;

    private SpriteRenderer _spriteRenderer;
    private UnityEngine.Rendering.Universal.Light2D _light2D;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            _spriteRenderer.color = _activeColor;
            _light2D.intensity = 0.5f;
            StartCoroutine(ChangeColorToInActive());
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayBoostFieldSfx();
            }
        }
    }

    IEnumerator ChangeColorToInActive()
    {
        yield return new WaitForSeconds(1f);
        _light2D.intensity = 0;
        _spriteRenderer.color = _inactiveColor;
    }
}
