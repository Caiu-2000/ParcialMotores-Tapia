using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
[DefaultExecutionOrder(10)]
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


    [SerializeField]
    public  BulletManager EnemyBulletManager;
    [SerializeField]
    public  PlayerBulletManager PlayerBulletManager;

    public List<Enemy> enemies = new List<Enemy>();


    void Start()
    {
        Player playerIns = Instantiate(Gamemanager.instance.CharactersList[(int)GlobalData.SelectedCharacter]);
        join.SetPlayer(playerIns);
        playerIns.transform.position = PlayerSpawn.position;
        RunPoints = 0;
        RunTime = 0;


        if (instance != null) Destroy(instance);
        instance = this;
        GoBack = InputSystem.actions.FindAction("Cancel");
        LoadLevel();
    }




    public float GetlimitRange()
    {
        return rigthLimit.position.x - leftLimit.position.x;
    }
    void Update()
    {
        if (GoBack != null) 
            if (GoBack.WasPressedThisFrame()) pause.ChangeGameState(GameState.OnHold); 

       EnemyBulletManager.Update();
        //PlayerBulletManager.Update();


    }
    
    protected void LoadLevel()
    {
        EnemyBulletManager = new BulletManager(500, Player.Instance);
        PlayerBulletManager = new PlayerBulletManager(100, enemies);


        EventManager<CombatEvents>.Subscribe<Bullet>(CombatEvents.PlayerFired, PlayerBulletManager.AddToList);
        EventManager<CombatEvents>.Subscribe<Bullet>(CombatEvents.EnemyFired, EnemyBulletManager.AddToList);




    }

    private void OnDisable()
    {
        EventManager<CombatEvents>.Unsubscribe<Bullet>(CombatEvents.PlayerFired, PlayerBulletManager.AddToList);
        EventManager<CombatEvents>.Unsubscribe<Bullet>(CombatEvents.EnemyFired, EnemyBulletManager.AddToList);
    }


}
