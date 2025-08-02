using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LevelButton : CustomButton
{

    [SerializeField]
    private Sprite lockedSprite;
    [SerializeField]
    private Sprite unlockedSprite;

    private WorldSO.LevelClass _level;

    public WorldSO.LevelClass level
    {
        get { return _level; }
        set
        {
            _level = value;
            InitLevelDisplay();
        }
    }

    void InitLevelDisplay()
    {
        button.onClick.AddListener(() => StartLevel());
    }

    private void StartLevel()
    {
      
        LevelSelectionEvents.OnLevelSelectedEventCaller();
    }

   

    public void LockButton()
    {
        image.sprite = lockedSprite;
        image.gameObject.SetActive(true);
        button.interactable = false;
        textMesh.gameObject.SetActive(false);
    }


    public void UnlockButton()
    {
        image.gameObject.SetActive(false);
        textMesh.gameObject.SetActive(true);
        button.interactable = true;
    }


}
