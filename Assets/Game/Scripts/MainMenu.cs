using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static bool ComicReaded = false;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Selection;
    [SerializeField] GameObject Comic;

    private void Start()
    {
        GoMenu();
    }
    public void PressedPlay()
    {
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
    }
    public void GoSelection()
    {
        HideAll();
        Selection.SetActive(true);
        
    }

    public void GoMenu()
    {
        HideAll();
        Menu.SetActive(true);
        
    }
    public void HideAll()
    {
        Selection.SetActive(false);
        Menu.SetActive(false);
        Comic.SetActive(false);
    }
}
