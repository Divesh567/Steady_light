using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Events;

public class SceneTransitionManager : MonoBehaviour
{
    public UnityEvent  OnSceneTransitionStarted;
    public UnityEvent OnSceneTransitionCompleted;

    public static SceneTransitionManager Instance { get; private set; }



    private bool transitionStarted;
    private int transitionIndex;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    private void OnEnable()
    {
        OnSceneTransitionStarted.AddListener(OnSceneLoadStarted);
        OnSceneTransitionCompleted.AddListener(OnSceneLoaded);
    }

    private void OnDisable()
    {
        OnSceneTransitionStarted.RemoveListener(OnSceneLoadStarted);
        OnSceneTransitionCompleted.RemoveListener(OnSceneLoaded);
    }


    public List<SceneTransitionBaseSO> sceneTransitions;
    public Canvas canvas;


    private GameObject panel;

    private int PickRandomAnimation()
    {

        transitionIndex = UnityEngine.Random.Range(0, sceneTransitions.Count);
        return transitionIndex;

    }

    private void OnSceneLoaded()
    {
        if(transitionStarted == false) { return; }

        transitionStarted = false;
        sceneTransitions[transitionIndex].OnSceneLoaded();
    }

    private void OnSceneLoadStarted()
    {
        PickRandomAnimation();

        panel = Instantiate(sceneTransitions[transitionIndex].trasnitionObject, this.transform);
        sceneTransitions[transitionIndex].PlayAnimationPart1(panel);
        transitionStarted = true;

    }

    public GameObject TestAnimation()
    {
        return Instantiate(sceneTransitions[2].trasnitionObject, this.transform);
    }

}
