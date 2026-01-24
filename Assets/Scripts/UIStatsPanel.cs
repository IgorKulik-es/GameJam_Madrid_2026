using TMPro;
using UnityEngine;

public class UIStatsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;


    public void UpdateTimerText(int time)
    {
        var timer = SecondsToTime(time);
        timeText.text = timer;
    }

    private string SecondsToTime(int seconds)
    {
        return $"{seconds / 60}:{seconds % 60}";
    }
    
}