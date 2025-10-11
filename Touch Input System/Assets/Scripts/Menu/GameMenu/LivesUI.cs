using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : ObjectiveUI
{

    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Transform spirteParent;


    [SerializeField]
    private Sprite lifeSprite;
    [SerializeField]
    private Sprite deathSprite;


    [SerializeField]
    private AnimateUI animateUI;

    private void OnEnable()
    {

        ObjectiveEventHandler.OnLifeLostEvent -= LifeLost;
        ObjectiveEventHandler.OnLifeLostEvent += LifeLost;
    }


    public override void InitUI()
    {
        animateUI.AnimateOut();
        animateUI.AnimateIn();
    }

    public override void ResetUI()
    {
        for(int i = 0; i < spirteParent.childCount; i++)
        {
            spirteParent.GetChild(i).gameObject.SetActive(true);
            spirteParent.GetChild(i).gameObject.GetComponent<Image>().sprite = lifeSprite;
        }

        currentLifes = 3;

        animateUI.AnimateOut();
    }


    private int currentLifes = 3;
    private void LifeLost()
    {
        Debug.Log("Life Lost");
        currentLifes--;
        if (currentLifes < 0) return;

        spirteParent.GetChild(currentLifes).gameObject.GetComponent<Image>().sprite = deathSprite;
    }


    [Serializable]
    public class AnimateUI
    {
        public RectTransform panelRect;
        public RectTransform timerRect;
        public CanvasGroup canvasGroup;

        public void AnimateIn()
        {

            panelRect.anchoredPosition = new Vector2(0, 200f);

            canvasGroup.alpha = 1;
            panelRect.DOAnchorPosY(0, 1f).OnComplete(() => timerRect.DOScale(Vector3.one * 1.2f, 0.5f).SetLoops(2, LoopType.Yoyo));


        }

        public void AnimateOut()
        {  
           canvasGroup.alpha = 0;
           panelRect.DOAnchorPosY(200, 0);
           
        }
    }

}
