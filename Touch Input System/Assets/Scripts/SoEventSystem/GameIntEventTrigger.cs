using System;
using UnityEngine;
using UnityEngine.Events;

public class GameIntEventTrigger : MonoBehaviour
{
    public IntEventChannel OnTrigger;
    public int param; 
    public bool doOnce = true;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
            OnTrigger.OnEventRaised(param);
        
        if(doOnce)
            gameObject.SetActive(false);
    }
}
