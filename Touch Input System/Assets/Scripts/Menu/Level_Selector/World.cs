using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class World : MonoBehaviour
{

    public RectTransform rectTransform; 

    private List<LevelButton> levelButtons = new List<LevelButton>();

    [SerializeField]
    private Transform buttonsParent;

    public WorldSO worldSO;


    private void Start()
    {
        levelButtons.AddRange(TrasformUtilities.GetComponentChildrenList<LevelButton>(buttonsParent.transform));

        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].level = worldSO.levels[i];
            levelButtons[i].LockButton();
        }

        if (DataManager.Instance.worldDatas.Exists(x => x.worldType == worldSO.worldType))
        {
            int levelsUnlocked = DataManager.Instance.worldDatas.Find(x => x.worldType == worldSO.worldType).levelsList.FindAll(l => l.unlocked).ToList().Count - 1;

            for (int i = 0; i <= levelsUnlocked; i++)
            {

                Debug.Log("levelsCompleted" + levelsUnlocked);
                levelButtons[i].UnlockButton();
            }
        }

        if(worldSO.worldType == WorldSO.WorldType.Basics)
                levelButtons[0].UnlockButton();
    }
}
