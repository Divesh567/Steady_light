using UnityEngine;

public class EnduranceObjectiveControl : MonoBehaviour
{
    [SerializeField]
    private bool _isSingleLifeLevel;
    private void Start()
    {
        if (MyGameManager.Instance != null && GameMenu.Instance != null
                        && AnotherChanceScript.Instance != null)
        {
            AnotherChanceScript.Instance._timetrail = false;
            MyGameManager.Instance._levelType = "eo";
            MyGameManager.Instance.ResetAllValues();
            GameMenu.Instance.EnableLivesPanel();
            if (_isSingleLifeLevel)
            {
                AnotherChanceScript.Instance._singleLife = true;
                MyGameManager.Instance.SetLifeOne();
                GameMenu.Instance.SetLifeOne(1, 2);
                SoundManager.Instance.PitchChangeEnudrance();
            }
            else
            {
                AnotherChanceScript.Instance._singleLife = false;
                GameMenu.Instance.SetLifeAll();
            }
        }
    }
}
