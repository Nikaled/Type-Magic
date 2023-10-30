using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level Progress", menuName = "ScriptableObjects/Level Progress", order = 51)]
public class ProgressSO : ScriptableObject
{
    public readonly string  MaxlevelIdPrefs = "MaxlevelIdPrefs";
    public readonly string CurrentLevelNamePrefs = "CurrentLevelNamePrefs";
    public int OtladkaId;
    private int _maxLevelId;
    private string _currentLevelName;
    public int MaxLevelId
    {
        get
        {
            if(OtladkaId != 0)
            {
                SetLevelWithOtladkaId();
            }
            return PlayerPrefs.GetInt(MaxlevelIdPrefs);
        }
        set
        {
            _maxLevelId = value;
            PlayerPrefs.SetInt(MaxlevelIdPrefs, _maxLevelId);
        }
    }
    public string CurrentLevelName
    {
        get
        {
            return PlayerPrefs.GetString(CurrentLevelNamePrefs);
        }
        set
        {
            _currentLevelName = value;
            PlayerPrefs.SetString(CurrentLevelNamePrefs, _currentLevelName);
        }
    }
    public void SetLevelWithOtladkaId()
    {
        MaxLevelId = OtladkaId;
    }
}
