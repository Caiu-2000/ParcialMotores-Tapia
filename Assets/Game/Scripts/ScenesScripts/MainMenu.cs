using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static bool ComicReaded = false;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Selection;
    [SerializeField] GameObject Comic;
    [SerializeField] GameObject Credits;
    [SerializeField] StoreManager Store; 
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
    public void HideAll()
    {
        Selection.SetActive(false);
        Menu.SetActive(false);
        Comic.SetActive(false);
        Credits.SetActive(false);
        Store.gameObject.SetActive(false);
    }
}
