using UnityEngine;

public class FlipObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _objToFlip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            _objToFlip.GetComponent<SawVfx>().FlipOnlyX();
        }
    }
}
