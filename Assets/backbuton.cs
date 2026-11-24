
using UnityEngine;
using UnityEngine.UI;
public class backbuton : MonoBehaviour
{
    [SerializeField] SoundTypes Sound;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Call);
    }
    void Call()
    {
        MainMenu.instance.GoSelection();
    }
}
