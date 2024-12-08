using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class StarUIPanel : ObjectiveUI
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Transform spirteParent;

    [SerializeField]
    private AnimateUI animateUI;

    [SerializeField]
    private StarUI starPrefab;
    [SerializeField]
    private Transform starParent;

    private List<StarUI> stars =  new List<StarUI>();

    private void OnEnable()
    {
        ObjectiveEventHandler.OnStarInitEvent -= InitStar;
        ObjectiveEventHandler.OnStarInitEvent += InitStar;

        ObjectiveEventHandler.OnStarCollectedEvent -= StarCollected;
        ObjectiveEventHandler.OnStarCollectedEvent += StarCollected;
    }



    public override void InitUI()
    {
        animateUI.AnimateOut();
        animateUI.AnimateIn();
    }

    public override void ResetUI()
    {
        stars.ForEach(x => Destroy(x.gameObject));
        stars.Clear();
        starsCollected = 0;
        animateUI.AnimateOut();
    }

    private void InitStar(Star star)
    {
       
       StarUI newStarImage =  Instantiate(starPrefab, starParent);

        stars.Add(newStarImage);
    }

    private int starsCollected = 0;

    private void StarCollected(Star star)
    {
        if (starsCollected >= stars.Count)
        {
            Debug.LogWarning("No more stars to collect!");
            return;
        }

        var starUI = stars[starsCollected];

        // Disable raycast to prevent further interaction
        starUI.starPositionForAnim = star.transform;
        starUI.starCollected = true;

        // Play collected animation

        starsCollected++; // Move to the next star
    }






    [Serializable]
    public class AnimateUI
    {
        public RectTransform panelRect;
        public CanvasGroup canvasGroup;

        public void AnimateIn()
        {
            panelRect.DOAnchorPosY(0, 1f);
            canvasGroup.alpha = 1;
        }

        public void AnimateOut()
        {
            panelRect.anchoredPosition = new Vector2(0, 200);
            canvasGroup.alpha = 0;
        }
    }
}
