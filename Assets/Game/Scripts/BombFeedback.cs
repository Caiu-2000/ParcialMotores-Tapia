using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BombFeedback : MonoBehaviour
{
    [SerializeField] Texture[] Icons = new Texture[2];
    protected RawImage image;
    private void Start()
    {
        image = GetComponent<RawImage>();
        image.texture = Icons[(int)GlobalData.SelectedCharacter];
        EventManager<GameEvent>.Subscribe<int>(GameEvent.BombTroued, ShowSprite);
    }

    public void ShowSprite(int x) => StartCoroutine(Show());
    private IEnumerator Show()
    {
        image.enabled = true;
        yield return new WaitForSeconds(0.25f);
        image.enabled = false;
    }
    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.BombTroued, ShowSprite);

    }
}
