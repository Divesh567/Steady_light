using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Menus.Animations
{
    [CreateAssetMenu(fileName = "New_SlideIn_Transition", menuName = "Menu_Transition/SlideIn_Transition")]
    public class SlideInTransition : MenuTransitions
    {
        [Header("Animation Values")]
        public Vector3 StartPosition;
        public Vector3 EndPosition;
        public Ease Ease;
        public float AnimDuration;

        [Tooltip("Id can be used to Play/Pause/Kill Animation")]
        public string AnimID = "SlideInAnim";

        public override void PlayTransition(Menu menu)
        {
            DOTween.Kill(AnimID);

            RectTransform MenuRect = menu.MainPanel.PanelRect;
            Image fromMenuImage = menu.MainPanel.PanelImage;
            MenuRect.anchoredPosition = StartPosition;

            MenuRect.DOAnchorPos(EndPosition, AnimDuration).OnComplete(() =>
            {
                base.OnTransitionComplete(menu);
            });
            Debug.Log("Slide In  transition");


            base.PlayTransition(menu);
        }

    }
}