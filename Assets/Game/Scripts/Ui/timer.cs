using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        RunManager.RunTime+=  Time.deltaTime;
        UpdateTime(RunManager.RunTime);
    }
    public void UpdateTime(float time)
    {
    
            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            int milliseconds = Mathf.FloorToInt((time * 100) % 100); // Extracts 2-digit milliseconds

            // Format string as "00:00:00"
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

    }
}
