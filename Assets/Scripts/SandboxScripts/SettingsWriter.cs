using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class SettingsWriter : MonoBehaviour
{
    [SerializeField] public TMP_InputField ClientText;
    [SerializeField] TMP_InputField ClientTypeSpeed;
    [SerializeField] TextFragments ClientTextFragment;
    [SerializeField] TypeSpeedSO TypeSpeedSettings;
    public  readonly string SpeedPlayerPrefs = "SpeedPlayerPrefs";
    public  readonly string ChallengeModePrefs = "ChallengeModePrefs";
    private bool IsChallengeMode;
    [SerializeField] GameObject PanelInputField;
    [SerializeField] Toggle ChallengeModeToggle;
    private void Start()
    {
        if (PanelInputField != null)
        {

            //bool isChallenge = Convert.ToBoolean(PlayerPrefs.GetInt(ChallengeModePrefs));
            PanelInputField.SetActive(TypeSpeedSettings.IsChallengeMode);
            ChallengeModeToggle.isOn = TypeSpeedSettings.IsChallengeMode;
            ClientTypeSpeed.text = TypeSpeedSettings.TypeSpeed.ToString();
        }
        if (ClientText)
        {
            ClientText.text = ClientTextFragment.FragmentOfText1;
        }
    }
    public void SaveSettings()
    {
        SaveText();
        SaveTypeSpeed();
    }
    public void SetGameMode()
    {
        IsChallengeMode = !IsChallengeMode;
        TypeSpeedSettings.IsChallengeMode = IsChallengeMode;
        int IsChallengeModeAsInt = Convert.ToInt32(IsChallengeMode);
        Debug.Log("Current Saved ChallengeModePrefs:" + IsChallengeModeAsInt);
    }
    public void SaveText()
    {
       
        if (ClientText.text == String.Empty)
        {
            return;
        }
        if (ClientText.text.Contains("\n"))
        {
            ClientText.text = ClientText.text.Replace("\n", " ");
        }
        if (ClientTextFragment != null) 
        {
        ClientTextFragment.FragmentOfText1 = ClientText.text;
        Debug.Log("сохраненный текст:" + ClientTextFragment.FragmentOfText1);
        }
    }
    public void SaveTypeSpeed()
    {
        string TypeSpeedString = ClientTypeSpeed.text;
        int TypeSpeed = 0;
        int.TryParse(TypeSpeedString, out TypeSpeed);
        if (TypeSpeed >= 0)
        TypeSpeedSettings.TypeSpeed = TypeSpeed;
        Debug.Log("скорость платформы:" + TypeSpeed);
    }
    public void OtladochnayaKnopka()
    {
        if (ClientText.text.Length > 2000)
            ClientText.text = ClientText.text[..2000];
        //  ClientTextFragment.FragmentOfText1 = ClientText.text[..2000];
    }
    public void Otladka()
    {
       
        //if (ClientText.text.Contains("\n"))
        //{
        //    ClientText.text = ClientText.text.Replace("\n", " ");
        //}
    }
    public void Zamena()
    {
        ClientText.text = ClientText.text.Replace("S", " ");
        Debug.Log("текст после отладки:" + ClientText.text);
        Debug.Log(ClientText.text[^1].GetTypeCode());
        string NewText = String.Empty;
        int razrivIndex = 0;
        for (int i = 0; i < ClientText.text.Length; i++)
        {
            if (char.IsControl(ClientText.text[i]))
            {
                NewText += ClientText.text[razrivIndex..i] + " ";
                razrivIndex = i+1;
            }
        }
        NewText += ClientText.text.Substring(razrivIndex);
        Debug.Log(NewText);
        ClientText.text = NewText;
        OtladochnayaKnopka();
    }
}
