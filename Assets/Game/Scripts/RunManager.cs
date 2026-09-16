using UnityEngine;
using UnityEngine.InputSystem;


public class RunManager : MonoBehaviour
{
    public static RunManager instance;
    public static int RunPoints = 0;
    [SerializeField] JoyController join;
    [SerializeField] Transform leftLimit, rigthLimit;
    [SerializeField] Transform PlayerSpawn;
   
    void Start()
    {
        Player playerIns = Instantiate(Gamemanager.instance.CharactersList[(int)GlobalData.SelectedCharacter]);
        join.SetPlayer(playerIns);
        playerIns.transform.position = PlayerSpawn.position;
        RunPoints = 0;


        // Es un singletone raro por que se reemplaza siempre que puede
        if (instance != null) Destroy(instance);
        instance = this;
       

    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame) { UnityEngine.SceneManagement.SceneManager.LoadScene(1); }
    }

    public float GetlimitRange()
    {
        return rigthLimit.position.x - leftLimit.position.x;
    }



#endif
}
