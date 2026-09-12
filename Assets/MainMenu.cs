using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Selection;

    public void PressedPlay()
    {
        GoSelection();
    }


    public void GoSelection()
    {
        Selection.SetActive(true);
        Menu.SetActive(false);
    }

    public void GoMenu()
    {
        Menu.SetActive(true);
        Selection.SetActive(false);
    }
}
