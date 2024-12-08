using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TriggerSpotLight : MonoBehaviour
{
    [SerializeField]
    public bool _startTrigger;
    [SerializeField]
    private SpotLightController _spotLight;

    [SerializeField]
    public bool _changeSpeed;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _resizeLight;
    [SerializeField]
    private float _resizeInner;
    [SerializeField]
    private float _resizeOuter;
    [SerializeField]
    private float _resizeSpeed;
    [SerializeField]
    private bool _wait;
    [SerializeField]
    private float _newWaitingTime;

    public void TriggerStartSpotlight()
    {
        if (_startTrigger)
        {
            _spotLight.TiggerFollow(_startTrigger);
        }
    }
  
    private void ChangeLightSpeed()
    {
        _spotLight._followSpeed = _speed;
    }
    public void TriggerResizeSpotlight()
    {
        var Light2D = _spotLight.gameObject.GetComponent<Light2D>();
        StartCoroutine(ResizeLightInner(Light2D.pointLightInnerRadius, _resizeInner, _resizeSpeed));
        StartCoroutine(ResizeLightOuter(Light2D.pointLightOuterRadius, _resizeOuter, _resizeSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike") || collision.CompareTag("Ball"))
        {
            if (_startTrigger)
            {
                TriggerStartSpotlight();
            }
        }
        else if (collision.CompareTag("SpotLight")) 
        {
            if (_resizeLight)
            {
                TriggerResizeSpotlight();
            }
            if (_changeSpeed)
            {
                ChangeLightSpeed();
            }
            if (_wait)
            {
                _spotLight.Wait(_newWaitingTime);
            }
        }
    }

    IEnumerator ResizeLightInner(float v_start, float v_end, float duration)
    {
        var Light2D = _spotLight.gameObject.GetComponent<Light2D>();
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            Light2D.pointLightInnerRadius = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Light2D.pointLightInnerRadius = v_end;
    }
    IEnumerator ResizeLightOuter(float v_start, float v_end, float duration)
    {
        var Light2D = _spotLight.gameObject.GetComponent<Light2D>();

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            Light2D.pointLightOuterRadius = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Light2D.pointLightOuterRadius = v_end;
    }
}
