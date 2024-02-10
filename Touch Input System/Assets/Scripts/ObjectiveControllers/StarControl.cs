using UnityEngine;

public class StarControl : MonoBehaviour
{
    [SerializeField]
    GameObject _starCollectedFx;
    private void Awake()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PitchChangeStarObjective();
        }
    }
    private void Start()
    {
        if (MyGameManager.Instance != null && GameMenu.Instance != null)
        {
            MyGameManager.Instance.StarsObjectiveStatus(false);
            MyGameManager.Instance.ResetAllValues();
            GameMenu.Instance.EnableStarPanel();
        }

    }

    public void StarCollected()
    {
        if (MyGameManager.Instance != null)
        {
            Instantiate(_starCollectedFx, transform.position, transform.rotation);
            MyGameManager.Instance.StarCollected();
            Destroy(gameObject);
        }
    }
}
