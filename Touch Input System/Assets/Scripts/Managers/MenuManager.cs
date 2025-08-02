using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using System;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }

    [SerializeField]
    private List<Menu> allMenus;

    private List<Menu> _runTimeMenus = new List<Menu>();

    public static bool _settingsMenuSwitch = false;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            InitializeMenus();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void InitializeMenus()
    {
        foreach (Menu menu in allMenus)
        {
            if (menu != null)
            {
                 Menu newMenu = Instantiate(menu);
                _runTimeMenus.Add(newMenu);
            }
        }

        OpenMainMenu();


    }
    public void OpenMainMenu()
    {
        for (int i = 0; i < _runTimeMenus.Count; i++)
        {
            if (i == 0)
            {
                OpenMenu(_runTimeMenus[i]);
            }
            else
            {
                CloseMenu(_runTimeMenus[i]);
            }
        }
    }

    public void SetSortingOrder(Menu menu)
    {
        _runTimeMenus.ForEach(x => x.canvas.sortingOrder = 0);
        menu.canvas.sortingOrder = 20;
    }

    public Menu currentMenu;
    public Menu previousMenu;
    public void OpenMenu(Menu newmenu)
    {
        newmenu.MenuOpen();

        //Set and update previous and current Menu
        previousMenu = currentMenu;
        currentMenu = newmenu;
    }

    public void OpenPopupMenu(Menu newmenu)
    {
        newmenu.MenuOpen(); 
    }
    public void CloseMenu(Menu newmenu)
    {
        newmenu.MenuClose();
    }

    public class MenuTransitions 
    {
        List<MenuTransitions> menuTransitions;

        public void InitTransitions()
        {

        }
    
    }
}
