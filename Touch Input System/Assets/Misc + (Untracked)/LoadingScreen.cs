using DG.Tweening;
using System.Collections;
using System.Linq;
using Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadingScreen : Menu<LoadingScreen>
{

    public DOTweenAnimation tweenAnimation;

    public override void MenuClose()
    {
        base.MenuClose();
        MainPanel.gameObject.SetActive(false);
    }
    public override void MenuOpen()
    {
        base.MenuOpen();
        StartCoroutine(AutoLoadCurrentLevel());
       // LevelLoader.Instance.LoadLevel(RuntimeGameData.levelSelected);
        MainPanel.gameObject.SetActive(true);
        tweenAnimation.DOPlayAllById(tweenAnimation.id);

        tweenAnimation.onComplete.AddListener(OnLoadingAnimComplete);


    }



    public void OnLoadingAnimComplete()
    {
        SceneTransitionManager.Instance.OnSceneTransitionAnimComplete.Invoke();
        tweenAnimation.onComplete.RemoveAllListeners();
       /* MenuManager.Instance.CloseMenu(this);
        MenuManager.Instance.OpenMenu(GameMenu.Instance);*/


    }

    public IEnumerator AutoLoadCurrentLevel()
    {
        yield return new WaitForSeconds(1f);

        var saveData = DataManager.Instance.saveDataSO.saveData.worldDatas;

        AssetReference currentLevel;

        currentLevel = LevelLoader.Instance.levelHolder.FindCurrentLevel(saveData);

        
     
        LevelLoader.Instance.LoadLevel(currentLevel);

        Debug.Log("Loading Level " + currentLevel.SubObjectName);
    }






}
