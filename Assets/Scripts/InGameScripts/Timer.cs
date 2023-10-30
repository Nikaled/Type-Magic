using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI TypeSpeedText;
    float seconds;
    int minutes;
    float miliseconds;
    bool TimerIsOn;
    private void OnEnable()
    {
        Controller.FirstBlockActivated += ActivateTimer;
    }
    private void OnDisable()
    {
        Controller.FirstBlockActivated -= ActivateTimer;
    }

    void Update()
    {
        if(TimerIsOn == true) 
        { 
        miliseconds += Time.deltaTime;
        seconds = (int)miliseconds;
        timerText.text = $" {minutes:00}  :  {seconds:00}";
        }
        if (seconds==60)
        {
            minutes++;
            seconds = 0;
            miliseconds = 0;
        }
        ShowTypeSpeed(Controller.instance.CurrentIndex);
    }
    private void ActivateTimer()
    {
        TimerIsOn = true;
    }
    private void ShowTypeSpeed(int charCount)
    {
        if (TimerIsOn)
        {
            float pastedTime =minutes * 60 + seconds;
         float TypeSpeed = (charCount / pastedTime) * 60;
            TypeSpeed =(float) Math.Round(TypeSpeed, 2);
        TypeSpeedText.text = $" {TypeSpeed} зн/м";

        }
    }

}
