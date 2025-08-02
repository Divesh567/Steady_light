using UnityEngine;

public class PortalControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _portalClose;
    [SerializeField]
    private GameObject _portalOpen;


    private void OnEnable()
    {
        ObjectiveEventHandler.OnStarObjectiveCompleted += OpenPortal;
        ObjectiveEventHandler.OnTimerObjectiveComplete += OpenPortal;

        ObjectiveEventHandler.OnTimerObjectiveFailed += ClosePortal;
        ObjectiveEventHandler.OnLifeObjectiveFailed += ClosePortal;
    }

    private void OnDisable()
    {
        ObjectiveEventHandler.OnStarObjectiveCompleted -= OpenPortal;
        ObjectiveEventHandler.OnTimerObjectiveComplete -= OpenPortal;


        ObjectiveEventHandler.OnTimerObjectiveFailed -= ClosePortal;
        ObjectiveEventHandler.OnLifeObjectiveFailed -= ClosePortal;
    }

    public void OpenPortal()
    {
        _portalClose.SetActive(false);
        _portalOpen.SetActive(true);
    }



    public void ClosePortal()
    {
        _portalClose.SetActive(true);
        _portalOpen.SetActive(false);

        MenuManager.Instance.OpenMenu(LoseScreen.Instance);
        MyGameManager.Instance.LevelLost();

    }

}
