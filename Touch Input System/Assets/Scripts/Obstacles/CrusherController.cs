using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CrusherController : MonoBehaviour
{
    [Header("Crusher Timing")]
    [SerializeField] private float defaultTimer = 1f;
    private float timer;

    [Header("Crusher Movement")]
    [SerializeField] private float crushDistance = 2f;
    [SerializeField] private float crushSpeed = 10f;
    [SerializeField] private float resetSpeed = 2f;

    private Vector3 startPos;
    private Vector3 crushPos;

    private void Start()
    {
        startPos = transform.position;
        crushPos = startPos + Vector3.down * crushDistance;
        timer = defaultTimer;

        StartCoroutine(CrusherLoop());
    }

    private IEnumerator CrusherLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);

            // Move down quickly (simulate crush)
            yield return transform.DOMove(crushPos, 1f / crushSpeed).SetEase(Ease.InQuad).WaitForCompletion();

            // Pause at bottom if you want (optional)
            yield return new WaitForSeconds(0.1f);

            // Reset to start position
            yield return transform.DOMove(startPos, 1f / resetSpeed).SetEase(Ease.OutQuad).WaitForCompletion();
        }
    }
}