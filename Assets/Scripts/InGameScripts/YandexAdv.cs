using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YandexAdv : MonoBehaviour
{
    private readonly string TimeForAdvPrefs = "TimeForAdvPrefs";
    int TimeForAdv;
    int SecondsBetweenAdv = 120; // количество секунд до показа реклама
    [SerializeField] GameManager gameManager;
    public void ShowYandexAdv()
    {
        TimeForAdv = PlayerPrefs.GetInt(TimeForAdvPrefs);
        float RealTime = Time.realtimeSinceStartup;
        if (TimeForAdv > RealTime)
        {
            SetPrefsAndShowAdv();
        }

        if (RealTime - TimeForAdv > SecondsBetweenAdv)
        {
            SetPrefsAndShowAdv();
        }
        void SetPrefsAndShowAdv()
        {
            TimeForAdv = (int)RealTime;
            PlayerPrefs.SetInt(TimeForAdvPrefs, TimeForAdv);
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
