using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Menus.Animations
{
    public enum TransitionType
    {
        Open,
        Close
    }


    public class MenuTransitions : ScriptableObject
    {
        public TransitionType transitionType;

        private int selectedSortOrder = 20;
        private int unSelectedSortOrder = 0;
        public virtual void PlayTransition(Menu menu)
        {
            if (transitionType == TransitionType.Open)
            {
                MenuManager.Instance.OpenMenu(menu);
            }
        }

        public virtual void OnTransitionComplete(Menu menu)
        {
            menu.graphicRaycaster.enabled = true;
        }
    }
}