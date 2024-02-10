using System.Collections;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _traps;
    private AudioSource _audioSource;
    private Collider2D _collider2D;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            TriggerTrap();
        }
    }

    private void TriggerTrap()
    {
        StartCoroutine(TriggeredVisualFeedback());
        _audioSource.Play();
        foreach (GameObject trap in _traps)
        {
            trap.GetComponent<Trap>().Triggered();
        }
        _collider2D.enabled = false;
    }

    IEnumerator TriggeredVisualFeedback()
    {
        if (GetComponent<SpriteRenderer>().enabled == false)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
