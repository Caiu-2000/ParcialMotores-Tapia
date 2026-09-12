using UnityEngine;
using UnityEngine.InputSystem;


public class RunManager : MonoBehaviour
{

    [SerializeField] JoyController join;
   
    void Start()
    {
        Player instance = Instantiate(Gamemanager.instance.CharactersList[(int)GlobalData.SelectedCharacter]);
        join.SetPlayer(instance);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame) { UnityEngine.SceneManagement.SceneManager.LoadScene(1); }
    }

#endif
}
