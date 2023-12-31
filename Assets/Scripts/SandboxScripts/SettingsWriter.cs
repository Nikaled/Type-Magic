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
    int MaxSymbolsInText = 2000;
    private void Start()
    {
        if (PanelInputField != null)
        {
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
        }
    }
    public void SaveTypeSpeed()
    {
        string TypeSpeedString = ClientTypeSpeed.text;
        int TypeSpeed = 0;
        int.TryParse(TypeSpeedString, out TypeSpeed);
        if (TypeSpeed >= 0)
        TypeSpeedSettings.TypeSpeed = TypeSpeed;
    }
    public void CutTextToMaxAvailable()
    {
        if (ClientText.text.Length > MaxSymbolsInText)
            ClientText.text = ClientText.text[..MaxSymbolsInText];
    }
    public void Zamena()
    {
        ClientText.text = ClientText.text.Replace("S", " ");
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
        ClientText.text = NewText;
        CutTextToMaxAvailable();
    }
}
