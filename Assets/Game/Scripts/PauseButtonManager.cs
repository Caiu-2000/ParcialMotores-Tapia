using UnityEngine;

public class PauseButtonManager : MonoBehaviour
{
    [SerializeField] PauseManager pause;
    public void OnPressed(){

        pause.ChangeGameState(GameState.OnHold);
        //EventManager<GameEvent>.Publish<GameState>(GameEvent.GamePaused , GameState.OnHold);
    }
}
