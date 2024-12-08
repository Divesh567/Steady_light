using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Menus.Animations
{

   [CreateAssetMenu(fileName = "New_Fill_Transition", menuName = "Menu_Transition/Fill_Transition")]
    public class FillTransition : MenuTransitions
    {
        [Header("Menu Values")]

        [Tooltip("0 indicates left and 1 indicates right ")] [Range(0,1)]public int fillOrigin;
        public Image.FillMethod fillMethod;
        [Range(0,3)]
        public float fillOutDuration;
        public Ease easeType;

        [Tooltip("Id can be used to Play/Pause/Kill Animation")]
        public string AnimID = "FillInAnim";

        public override void PlayTransition(Menu menu)
        {
            DOTween.Kill(AnimID);

            RectTransform fromMenuRect = menu.MainPanel.PanelRect;
            Image fromMenuImage = menu.MainPanel.PanelImage;
            fromMenuImage.fillMethod = fillMethod;
            fromMenuImage.fillOrigin = fillOrigin;
            fromMenuImage.DOFillAmount(0, fillOutDuration).OnComplete(() =>
            {
                MenuManager.Instance.CloseMenu((menu));
                fromMenuImage.fillAmount = 1;
                base.OnTransitionComplete(menu);


            });

         
            base.PlayTransition(menu);
        }
    }
}