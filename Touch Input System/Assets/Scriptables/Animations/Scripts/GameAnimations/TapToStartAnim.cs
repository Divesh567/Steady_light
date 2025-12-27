using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Tap to Start Anim", menuName = "Game_Transition/Game Start/Tap to Start Anim")]
public class TapToStartAnim : GameStartAnim
{


    public CustomButton startButtonPrefab;
    public TextMeshProUGUI tapToStartText;
    public Transform tapToStartTransform;

    public float valueStartScaleMultiplier;
    public float valueEndScaleMultiplier;
    


    public override void StartAnim(UnityAction startAction, UnityAction endAction)
    {

        var startButton = Instantiate(startButtonPrefab, GameMenu.Instance.transform);

        base.StartAnim(startAction, endAction);
        sceneInitializer.initStart.Invoke();

        startButton.button.interactable = true;
      

        startButton.button.onClick.AddListener(() => 
        {
            AnalyticsEvent analyticsEvent = new AnalyticsEvent(EventName.LevelStart)
                                             .AddParam(ParamName.Level_Name, LevelLoader.Instance.GetCurrentSceneName());

            FirebaseAnalyticsController.LogEvent(analyticsEvent);

            sceneInitializer.initEnd.Invoke();
            Destroy(startButton.gameObject);

        });

        tapToStartText = startButton.textMesh;
        tapToStartTransform = startButton.transform;

        tapToStartText.DOText("Tap to Start", 1f);
        tapToStartTransform.DOScale(valueEndScaleMultiplier, 1f).SetLoops(-1, LoopType.Yoyo);
    }

}