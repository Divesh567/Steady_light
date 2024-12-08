using System.Collections;
using UnityEngine;

public class FaceAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite _expressionLess;
    [SerializeField]
    private Sprite _expressionLessBlink;
    [SerializeField]
    private Sprite _angry;
    [SerializeField]
    private Sprite _confused;
    [SerializeField]
    private Sprite _suprised;
    [SerializeField]
    private Sprite _smile;
    [SerializeField]
    private Sprite _happy;
    [SerializeField]
    private Sprite _ashamed;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("PlayBlinkAnim");
    }

    public void MakeConfusedFace()
    {
        StopCoroutine("PlayBlinkAnim");
        _spriteRenderer.sprite = _confused;
    }

    public void MakeSmileFace()
    {
        StopCoroutine("PlayBlinkAnim");
        _spriteRenderer.sprite = _smile;
    }

    public void MakeSuprisedFace()
    {
        StopCoroutine("PlayBlinkAnim");
        _spriteRenderer.sprite = _suprised;
    }

    public void MakeHappyFace()
    {
        StopCoroutine("PlayBlinkAnim");
        _spriteRenderer.sprite = _happy;
    }

    public void MakeAngryFace()
    {
        StopCoroutine("PlayBlinkAnim");
        _spriteRenderer.sprite = _angry;
    }
    public void MakeExpressionlessFace()
    {
        StopCoroutine("PlayBlinkAnim");
        StartCoroutine("PlayBlinkAnim");
    }

    IEnumerator PlayBlinkAnim()
    {
        _spriteRenderer.sprite = _expressionLess;
        yield return new WaitForSeconds(0.75f);
        _spriteRenderer.sprite = _expressionLessBlink;
        yield return new WaitForSeconds(0.25f);
        StartCoroutine("PlayBlinkAnim");
    }
}
