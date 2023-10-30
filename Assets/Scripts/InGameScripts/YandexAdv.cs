using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YandexAdv : MonoBehaviour
{
    private readonly string TimeForAdvPrefs = "TimeForAdvPrefs";
     int TimeForAdv;
    int SecondsBetweenAdv = 120; // количество секунд до показа реклама
    [SerializeField] GameManager gameManager;
    void Start() 
    { 
    }
    public void ShowYandexAdv()
    {
        SmartAdv();
    }
    private void SmartAdv() // умная реклама: проверям прошло ли 3 минуты с запуска последней рекламы, если да, то начинаем отслеживать от этого момента еще 3 минуты
    {
       
        TimeForAdv = PlayerPrefs.GetInt(TimeForAdvPrefs);
        float RealTime = Time.realtimeSinceStartup;
        if (TimeForAdv > RealTime)
        {
            SetPrefsAndShowAdv();
            Debug.Log("TimeForAdv теперь 0");
            Debug.Log("-------------Время рекламы из-за нового входа в игру---------------");
        }
        Debug.Log(RealTime);
        Debug.Log(TimeForAdv);
        if (RealTime - TimeForAdv > SecondsBetweenAdv)
        {
            SetPrefsAndShowAdv();
            Debug.Log("-------------Время рекламы---------------");
            Debug.Log("а теперь TimeForAdv равен " + TimeForAdv);
            
        }
        void SetPrefsAndShowAdv()
        {
            TimeForAdv = (int)RealTime;
            PlayerPrefs.SetInt(TimeForAdvPrefs, TimeForAdv);
            //FindObjectOfType<BackgroundMusic>()?.PauseMusic();
            PauseMusic();
            gameManager.ShowYandexAdv();
        }
    }
    [ContextMenu("UnPause Music")]
    public void UnPauseMusic()
    {
        if (FindObjectOfType<BackgroundMusic>() != null) 
        {
        FindObjectOfType<BackgroundMusic>().UnPauseMusic();
            Debug.Log("UnPause Music нашел объект с музыкой");
        }
        else
        {
            Debug.Log("UnPause Music не нашел объект с музыкой");
        }
    }
    [ContextMenu("Pause Music")]
    public void PauseMusic()
    {
        FindObjectOfType<BackgroundMusic>()?.PauseMusic();
    }

    [ContextMenu("SetTimeToZero")]
    public void SetTimeToZero()
    {
        PlayerPrefs.SetInt(TimeForAdvPrefs, 0);

    }

}
