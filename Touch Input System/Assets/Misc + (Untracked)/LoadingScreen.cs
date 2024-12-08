using DG.Tweening;
using Unity;
using UnityEngine;

public class LoadingScreen : Menu<LoadingScreen>
{

    public DOTweenAnimation tweenAnimation;

    public override void MenuClose()
    {
        MainPanel.gameObject.SetActive(false);
    }
    public override void MenuOpen()
    {
        LevelLoader.Instance.LoadLevel(RuntimeGameData.levelSelected);
        MainPanel.gameObject.SetActive(true);
        tweenAnimation.DOPlayAllById(tweenAnimation.id);
      
    }

    public void OnLoadingAnimComplete()
    {
        MenuManager.Instance.CloseMenu(this);
        MenuManager.Instance.OpenMenu(GameMenu.Instance);
    }





    
}
