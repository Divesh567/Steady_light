using UnityEngine;

public class PortalControl : MonoBehaviour
{
    private GameObject _portalClose;
    private GameObject _portalOpen;

    private void Start()
    {
        MyGameManager.Instance.GetExitPortal(GetComponent<PortalControl>());
        _portalOpen = transform.GetChild(0).gameObject;
        _portalClose = transform.GetChild(1).gameObject;

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
    }

}
