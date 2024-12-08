using UnityEngine;

public class LevelTransitionController : MonoBehaviour
{
    private GameObject _transitionobject;
    private GameObject _startButton;
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _transitionobject = transform.GetChild(0).gameObject;
        _startButton = transform.GetChild(1).gameObject;
    }
    public void PauseGameAndEnableStartButton()
    {
        _startButton.gameObject.SetActive(true);
        Time.timeScale = 0;
        _transitionobject.SetActive(false);
    }
    public void OnStartButtonPressed()
    {
        Time.timeScale = 1;
        _startButton.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
