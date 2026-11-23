using System;
using UnityEditor;
using UnityEngine;

public class MainMenu : ExitReader
{
    public static bool ComicReaded = false;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Selection;
    [SerializeField] GameObject Comic;
    [SerializeField] GameObject Credits;
    [SerializeField] StoreManager Store;

    [SerializeField] SkinSelection skinSelection;
    [SerializeField] GameObject config;
    [SerializeField] GameObject ExitGame;

    public static MainMenu instance ;
    protected override void Start()
    {
        instance = this;
        Time.timeScale = 1.0f;
        base.Start();
        GoMenu();
    }
    public void PressedPlay()
    {
        if (GlobalData.Stamina < 20) return;
        if (!ComicReaded)
        {
            ComicReaded = true;
            GoComic();
        }
        else
        {
            GoSelection();
        }
    }

    public void GoComic()
    {
        HideAll();
        Comic.SetActive(true);
        SoundManager.instance.PlayRandom(SoundTypes.menu);
    }
    public void GoSelection()
    {
        HideAll();
        SoundManager.instance.PlayRandom(SoundTypes.menu);
        Selection.SetActive(true);
        
    }
    public void GoCredits()
    {
        HideAll();
        Credits.SetActive(true);
    }
    public void GoMenu()
    {
        HideAll();
        Menu.SetActive(true);
        

    }
    public void GoStore()
    {
        HideAll();
        Store.gameObject.SetActive(true);
        Store.LoadStore();
    }

    public void GoSkinSelection()
    {
        HideAll();
        skinSelection.gameObject.SetActive(true);
        skinSelection.LoadSkins();
    }

    public void ConfirmExit()
    {
        //HideAll();
        ExitGame.SetActive(true);
    }

    public void HideAll()
    {
        Selection.SetActive(false);
        Menu.SetActive(false);
        Comic.SetActive(false);
        Credits.SetActive(false);
        config.SetActive(false);
        ExitGame.SetActive(false);
        Store.gameObject.SetActive(false);
        skinSelection.gameObject.SetActive(false);
        skinSelection.UnloadScreen();

    }

    protected override void GoBackPressed()
    {
        ConfirmExit();
    }

    public void GoSettings()
    {
        HideAll();
        config.SetActive(true);
    }
}
