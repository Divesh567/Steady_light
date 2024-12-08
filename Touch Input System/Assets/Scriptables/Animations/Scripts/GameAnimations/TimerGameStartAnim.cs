using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New_3,2,1_GOAnim", menuName = "Game_Transition/Game Start/3,2,1_GOAnim")]
public class TimerGameStartAnim : GameStartAnim
{
    public TextMeshProUGUI timerTextMesh;

    public float timeInterval;

    public float valueStartScaleMultiplier;
    public float valueEndScaleMultiplier;


    public AudioClip countDownClip;

    public override void StartAnim(UnityAction startAction, UnityAction endAction)
    {
        base.StartAnim(startAction, endAction);

        var timerSeq = DOTween.Sequence();

        timerSeq.AppendInterval(1.2f);

        var textmesh = Instantiate(timerTextMesh, GameMenu.Instance.transform);

        textmesh.DOFade(0,0);

        var audioController = textmesh.GetComponentInChildren<AudioControllerMono>();

        textmesh.text = "3";

        timerSeq.Append(textmesh.DOFade(1, 0));

        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * valueStartScaleMultiplier, 0));

        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * valueStartScaleMultiplier, 0).OnComplete(() =>  sceneInitializer.initStart.Invoke()));

        timerSeq.Append(textmesh.DOText("3", 0));

        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * valueEndScaleMultiplier, timeInterval).OnStart(() => 
        {
            audioController.PlayAudioClip(countDownClip);
        }));



        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * valueStartScaleMultiplier, 0));

        timerSeq.Append(textmesh.DOText("2", 0));

        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * valueEndScaleMultiplier, timeInterval).OnStart(() =>
        {
            audioController.PlayAudioClip(countDownClip);
        }));


        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * valueStartScaleMultiplier, 0));

        timerSeq.Append(textmesh.DOText("1", 0));

        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * valueEndScaleMultiplier, timeInterval).OnStart(() =>
        {
            audioController.PlayAudioClip(countDownClip);
        }));


        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * valueStartScaleMultiplier, 0));

        timerSeq.Append(textmesh.DOText("Go", 0));

        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * valueEndScaleMultiplier, timeInterval).OnStart(() =>
        {
            audioController.PlayAudioClip(countDownClip);
        }));

        timerSeq.Append(textmesh.transform.DOScale(Vector3.one * 0, timeInterval/2));

        timerSeq.Play().OnComplete(() => sceneInitializer.initEnd.Invoke());


    }


    #region InspectorTesting
    [Button("Play Audio Clip")]
    private void PlayClip()
    {
        // Use AudioSource.PlayClipAtPoint to play the audio clip in the scene
        if (countDownClip != null)
        {
            AudioSource.PlayClipAtPoint(countDownClip, Vector3.zero);
        }
        else
        {
            Debug.LogWarning("No audio clip assigned!");
        }
    }
    #endregion
}
