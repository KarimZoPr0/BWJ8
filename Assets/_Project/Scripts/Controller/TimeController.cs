using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    public TMP_Text timeCounter;
    public int timeLimit;
    
    

    private TimeSpan timePlaying;
    private bool timeGoing;
    private float elapsedTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeCounter.text = "Time: 00:00:00";
        timeGoing = false;
        
        BeginTimer();
    }

    public void BeginTimer()
    {
        timeGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timeGoing && timePlaying.Seconds != timeLimit)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = $"Time: {timePlaying:mm':'ss'.'ff}";
            timeCounter.text = timePlayingStr;

            yield return null;
        }

        Debug.Log("Stopped");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
