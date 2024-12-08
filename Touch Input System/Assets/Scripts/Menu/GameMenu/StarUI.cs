using Unity;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof (Image))]
public class StarUI :  MonoBehaviour
{

    [SerializeField]
    private DOTweenAnimation collectedAnim;

    public Transform starPositionForAnim;

    [SerializeField]
    private Image _starImage;
    [SerializeField]
    private RectTransform _starchild;

    private bool _isStarCollected;
    public bool starCollected
    {   
        get 
        {
            return _isStarCollected; 
        } 
        set 
        {
            _isStarCollected = value;
            OnStarCollectedAnim(); 
        } 
    }


    private void OnStarCollectedAnim()
    {
        Vector2 screenPos = CameraUtilities.GetScreenPos(starPositionForAnim, GetComponentInParent<Canvas>());

        _starchild.SetParent(TrasformUtilities.GetHighestParent(transform));

        _starchild.anchoredPosition = screenPos;

        _starchild.gameObject.SetActive(true);

        _starchild.SetParent(transform, true);

        _starchild.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutFlash);


        collectedAnim.DOPlay();

    }

}