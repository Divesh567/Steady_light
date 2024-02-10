using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    private void Start()
    {
        if (GameMenu.Instance != null && MenuManager.Instance != null)
        {
            MenuManager.Instance.CloseMenu(GameMenu.Instance);
        }
    }
    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void MainMenuButtonPressed()
    {
        if (GameMenu.Instance != null)
        {
            GameMenu.Instance.OnMainMenuButtonPressed();
            Destroy(gameObject);
        }
    }
}
