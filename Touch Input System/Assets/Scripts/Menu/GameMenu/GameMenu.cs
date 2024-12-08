using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class GameMenu : Menu<GameMenu>
{

    public List<ObjectiveUI> objectiveUIs;
    public override void Start()
    {
        base.Start();
    }

    public override void MenuOpen()
    {
        MainPanel.gameObject.SetActive(true);
    }

    public override void MenuClose()
    {
        MainPanel.gameObject.SetActive(false);
        objectiveUIs.ForEach(x => x.ResetUI());
    }

    public void InitObjectiveUI(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour is LivesControl)
        {
            var ui = objectiveUIs.Find(x => x.GetType() == typeof(LivesUI));
            if (ui != null)
            {
                ui.InitUI();
            }
        }
 

        if (monoBehaviour is StarControl)
        {
            var ui = objectiveUIs.Find(x => x.GetType() == typeof(StarUIPanel));
            if (ui != null)
            {
                ui.InitUI();
            }
        }


        if (monoBehaviour is TimeTrialControl)
        {
            var ui = objectiveUIs.Find(x => x.GetType() == typeof(TimerUI));
            if (ui != null)
            {
                ui.InitUI();
            }
        }
    }
}
