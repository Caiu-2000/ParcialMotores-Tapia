using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<GameState>(GameEvent.GamePaused, ChangeGameState);
        EventManager<GameEvent>.Subscribe<GameState>(GameEvent.GameResumed, ChangeGameState);
    }

    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<GameState>(GameEvent.GamePaused, ChangeGameState);
        EventManager<GameEvent>.Unsubscribe<GameState>(GameEvent.GameResumed, ChangeGameState);
    }


    public void ChangeGameState(GameState newState)
    {

        if(newState == GameState.OnHold)
        {
            this.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (newState == GameState.Running){
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void Onclicked()
    {
        ChangeGameState(GameState.Running);
    }
}
