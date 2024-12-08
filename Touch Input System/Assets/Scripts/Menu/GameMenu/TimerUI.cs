using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class TimerUI : ObjectiveUI
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Transform spirteParent;
    [SerializeField]
    private GameObject timerSprite;
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private AnimateUI animateUI;


    public override void InitUI()
    {
        animateUI.AnimateOut();
        animateUI.AnimateIn();
    }

    public override void ResetUI()
    {
        animateUI.AnimateOut();
    }

    private void LateUpdate()
    {
        timerText.text = TimeTrialControl._currentTime.ToString("F2");
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
            panelRect.DOAnchorPosY(0, 1f).OnComplete(() =>  timerRect.DOScale(Vector3.one * 1.2f, 0.5f).SetLoops(2, LoopType.Yoyo));
        }

        public void AnimateOut()
        {
            panelRect.anchoredPosition = new Vector2(0, -200f);
            canvasGroup.alpha = 0;
        }
    }
}
