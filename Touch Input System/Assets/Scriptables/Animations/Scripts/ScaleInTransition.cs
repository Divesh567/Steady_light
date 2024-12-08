using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Menus.Animations
{
    [CreateAssetMenu(fileName = "New_ScaleIn_Transition", menuName = "Menu_Transition/ScaleIn_Transition")]
    class ScaleInTransition : MenuTransitions
    {
        [Header("Animation Values")]
        public Vector3 StartSize;
        public Vector3 EndSize;
        public Ease Ease;
        public float AnimDuration;

        [Tooltip("Id can be used to Play/Pause/Kill Animation")]
        public string AnimID = "ScaleInAnim";

        public override void PlayTransition(Menu menu)
        {
            DOTween.Kill(AnimID);

            RectTransform toMenuRect = menu.MainPanel.PanelRect;
            Image fromMenuImage = menu.MainPanel.PanelImage;
            toMenuRect.localScale = StartSize;

            toMenuRect.DOScale(EndSize, AnimDuration).SetId(AnimID).OnComplete(() => 
            {
                base.OnTransitionComplete(menu);
            });

            base.PlayTransition(menu);
        }
    
    }
}
