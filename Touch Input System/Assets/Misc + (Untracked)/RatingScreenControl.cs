using UnityEngine;

public class RatingScreenControl : MonoBehaviour
{
    public void OnYesButtonPressed()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Immersiveorama.LightForce");
    }

    public void OnCrossButtonPressed()
    {
        Destroy(gameObject);
    }
}

