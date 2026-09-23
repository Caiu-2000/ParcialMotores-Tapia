using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunManager : MonoBehaviour
{
    public static RunManager instance;
    public static int RunPoints = 0;
    public static float RunTime = 0;
    [SerializeField] JoyController join;
    [SerializeField] Transform leftLimit, rigthLimit;
    [SerializeField] Transform PlayerSpawn;

    InputAction GoBack;
    [SerializeField] PauseManager pause;

    void Start()
    {
        Player playerIns = Instantiate(Gamemanager.instance.CharactersList[(int)GlobalData.SelectedCharacter]);
        join.SetPlayer(playerIns);
        playerIns.transform.position = PlayerSpawn.position;
        RunPoints = 0;
        RunTime = 0;


        // Es un singletone raro por que se reemplaza siempre que puede
        if (instance != null) Destroy(instance);
        instance = this;
        GoBack = InputSystem.actions.FindAction("Cancel");

    }




    public float GetlimitRange()
    {
        return rigthLimit.position.x - leftLimit.position.x;
    }
    void Update()
    {
        if (GoBack.WasPressedThisFrame()) pause.ChangeGameState(GameState.OnHold); ;
    }
    


}
