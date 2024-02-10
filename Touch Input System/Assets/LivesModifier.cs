using UnityEngine;

public class LivesModifier : MonoBehaviour
{
    public void ChangeLives()
    {
        if (MyGameManager.Instance != null && GameMenu.Instance != null
                              && SoundManager.Instance != null)
        {
            MyGameManager.Instance.SetLifeOne();
            GameMenu.Instance.SetLifeOne(1, 2);
            SoundManager.Instance.PitchChangeEnudrance();
        }
    }
}
