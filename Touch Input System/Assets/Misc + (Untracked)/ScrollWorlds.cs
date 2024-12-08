using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollWorlds : MonoBehaviour
{
    [SerializeField]
    private List<World> worlds;
    private int currentWorldIndex;

    [Header("Aniamtion Values")]
    [SerializeField]
    private float animSpeed;



    public CustomButton arrowButtonRight;
    public CustomButton arrowButtonLeft;


    private void Start()
    {
        arrowButtonRight.button.onClick.AddListener(() => ScrollWorld(1));
        arrowButtonLeft.button.onClick.AddListener(() => ScrollWorld(-1));

        currentWorldIndex = 0;
    }

    private void ScrollWorld(int scrollDir)
    {
        if (currentWorldIndex + scrollDir >= worlds.Count || currentWorldIndex + scrollDir < 0) return;


        if (scrollDir == -1)
        {
            worlds[currentWorldIndex].rectTransform.DOAnchorPosX(1920, animSpeed);

            currentWorldIndex += scrollDir;

            worlds[currentWorldIndex].rectTransform.DOAnchorPosX(0, animSpeed);
        }
        else
        {
            worlds[currentWorldIndex].rectTransform.DOAnchorPosX(-1920, animSpeed);

            currentWorldIndex += scrollDir;

            worlds[currentWorldIndex].rectTransform.DOAnchorPosX(0, animSpeed);

        }
       

       
    }


}
