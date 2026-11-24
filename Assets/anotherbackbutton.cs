
using UnityEngine;
using UnityEngine.UI;

public class anotherbackbutton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Call);
    }
    void Call()
    {
        MainMenu.instance.GoMenu();
    }
}
