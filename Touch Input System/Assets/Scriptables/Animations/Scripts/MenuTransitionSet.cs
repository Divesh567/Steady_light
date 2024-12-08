using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Menus.Animations
{
    [CreateAssetMenu(fileName = "New_Transition_Set", menuName = "Menu_Transition/Set/Transition_Set", order = 0)]
    public class MenuTransitionSet : ScriptableObject
    {
        private Menu _fromMenu;
        private Menu _toMenu;

        public MenuTransitions fromMenuTransition;
        public MenuTransitions toMenuTransition;
        public void InitTranistion(Menu fromMenu, Menu toMenu)
        {
            _fromMenu = fromMenu;
            _toMenu = toMenu;
        }

        public void PlayTransition()
        {
            _fromMenu.graphicRaycaster.enabled = false;
            _toMenu.graphicRaycaster.enabled = false;

            fromMenuTransition.PlayTransition(_fromMenu);
            toMenuTransition.PlayTransition(_toMenu);
        }

    }
}

