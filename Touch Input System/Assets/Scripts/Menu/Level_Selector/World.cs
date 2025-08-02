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

        if(levelButtons.Count > worldSO.levels.Count)
        {
           int buttonsToDisable = levelButtons.Count - worldSO.levels.Count;

            for (int x = 0; x < buttonsToDisable; x++)
            {
                levelButtons[levelButtons.Count - x - 1].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < worldSO.levels.Count; i++)
        {
            levelButtons[i].level = worldSO.levels[i];
            levelButtons[i].LockButton();
        }

        if (DataManager.Instance.worldDatas.Exists(x => x.worldType == worldSO.worldType))
        {
            int levelsUnlocked = DataManager.Instance.worldDatas.Find(x => x.worldType == worldSO.worldType).levelsList.FindAll(l => l.unlocked).ToList().Count - 1;

            for (int i = 0; i <= levelsUnlocked; i++)
            {
                levelButtons[i].UnlockButton();
            }
        }

        if(worldSO.worldType == WorldSO.WorldType.Basics)
                levelButtons[0].UnlockButton();
    }
}
