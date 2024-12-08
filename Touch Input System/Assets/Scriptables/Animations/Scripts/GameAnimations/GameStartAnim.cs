using UnityEngine;
using UnityEngine.Events;

public class GameStartAnim : ScriptableObject
{
    [HideInInspector]
    public SceneInitializer sceneInitializer = new SceneInitializer();



    public virtual void StartAnim(UnityAction startAction, UnityAction endAction)
    {
       // Time.timeScale = 0;

        sceneInitializer.initStart.AddListener(startAction);
        sceneInitializer.initEnd.AddListener(endAction);

    }

    public virtual void OnAnimComplete()
    {
      //  Time.timeScale = 1;

    }


}
