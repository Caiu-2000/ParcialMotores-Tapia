using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class SoundFeedback : MonoBehaviour
{
    [SerializeField] SoundTypes Sound;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Call);
    }
    void Call()
    {
        print("butooo");
        SoundManager.instance.PlayPitched(Sound);
    }
}