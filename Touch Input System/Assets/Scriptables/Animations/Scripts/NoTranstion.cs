using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Menus.Animations
{
    [CreateAssetMenu(fileName = "No_Transition", menuName = "Menu_Transition/No_Transition")]
    class NoTranstion : MenuTransitions
    {

        [TextArea]
        public string NoTrasnition; // To  be replaced by some Custom Property
    }
}
