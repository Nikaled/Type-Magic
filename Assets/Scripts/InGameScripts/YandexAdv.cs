using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YandexAdv : MonoBehaviour
{
    private readonly string TimeForAdvPrefs = "TimeForAdvPrefs";
     int TimeForAdv;
    int SecondsBetweenAdv = 120; // ���������� ������ �� ������ �������
    [SerializeField] GameManager gameManager;
    void Start() 
    { 
    }
    public void ShowYandexAdv()
    {
        SmartAdv();
    }
    private void SmartAdv() // ����� �������: �������� ������ �� 3 ������ � ������� ��������� �������, ���� ��, �� �������� ����������� �� ����� ������� ��� 3 ������
    {
       
        TimeForAdv = PlayerPrefs.GetInt(TimeForAdvPrefs);
        float RealTime = Time.realtimeSinceStartup;
        if (TimeForAdv > RealTime)
        {
            SetPrefsAndShowAdv();
            Debug.Log("TimeForAdv ������ 0");
            Debug.Log("-------------����� ������� ��-�� ������ ����� � ����---------------");
        }
        Debug.Log(RealTime);
        Debug.Log(TimeForAdv);
        if (RealTime - TimeForAdv > SecondsBetweenAdv)
        {
            SetPrefsAndShowAdv();
            Debug.Log("-------------����� �������---------------");
            Debug.Log("� ������ TimeForAdv ����� " + TimeForAdv);
            
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
            Debug.Log("UnPause Music ����� ������ � �������");
        }
        else
        {
            Debug.Log("UnPause Music �� ����� ������ � �������");
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
