using System.Collections;
using UnityEngine;


public class DisableSpike : MonoBehaviour
{
    private float _disableTime = 6.5f;
    private Collider2D _collider2D;
    private UnityEngine.Rendering.Universal.Light2D _light2D;
    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
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
        yield return new WaitForSeconds(_disableTime);
        _collider2D.enabled = true;
        if (_light2D != null)
        {
            _light2D.enabled = true;
        }
        StopCoroutine("Disable");
    }
}
