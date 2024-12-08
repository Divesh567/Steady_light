using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Level", menuName = "Level/New Level")]
public class LevelSO : ScriptableObject
{
    public enum LevelType {
        Stars,
        TimeTrail,
        OneLife
    }


    [Header("Level Details")]
    [SerializeField]
    private string _levelId;
    [SerializeField]
    private Object sceneObject; 
    [SerializeField]
    private LevelType _levelType;

    [Space(10)]
    [Header("UI Sprites")]
    private Sprite _levelLockedSprite;
    private CustomButton _levelUnlockedSprite;

    public void InitLevelDisplay(CustomButton button)
    {
       
    }


}
