using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI livesCount;

    // When a new game level is first loaded, it sets the InGameUI life counter to the current
    // player's lives. I learned this method in the Unity Documentation: SceneManager.sceneLoaded.
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        float time = GameStateManager.GetTimer;
        InitializeTime(time);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Convert time player spent in a round and display it.
    private void InitializeTime(float timer)
    {
        int timeInt = (int)timer;
        int seconds = timeInt % 60;
        int mins = (timeInt / 60) % 60;
        int hrs = (timeInt / 3600) % 24;

        string timeString = "Time:\n";
        string semiColon = ":";

        timeString += ConvertToTimeFormat(hrs);
        timeString += semiColon;
        timeString += ConvertToTimeFormat(mins);
        timeString += semiColon;
        timeString += ConvertToTimeFormat(seconds);

        livesCount.text = timeString;
    }

    // If num is less than 10, add 0 to string before num.
    private string ConvertToTimeFormat(int num)
    {
        string timeFormat = "";
        if(num < 10)
        {
            timeFormat += "0";
            timeFormat += num.ToString();
        }
        else
        {
            timeFormat += num.ToString();
        }
        return timeFormat;
    }
}
