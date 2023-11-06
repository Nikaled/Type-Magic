using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;

public class SaveData //класс-костыль для сохранения данных
{
    public string CurrentLevelName;
    public int index;
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ProgressSO progress;
    public LevelListSO LevelsList;
    [DllImport("__Internal")]
    public static extern void ShowAdv();

    void Start()
    {
            instance = this;
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SoundPanel.instance.LoadVolumeSettings();
        }

    }
    public void ShowYandexAdv()
    {
#if !UNITY_EDITOR
        ShowAdv();
#endif
    }
    public void SaveCurrentLevelName()
    {
        progress.CurrentLevelName = SceneManager.GetActiveScene().name;
    }
    public int? FindLevelId(string NameScene)
    {
        try
        {
            return LevelsList.LevelsNameList.IndexOf(LevelsList.LevelsNameList.Where
                (LevelAsObject => LevelAsObject == NameScene).First());
        }
        catch
        {
            return null;
        }
    }
    public void LoadNextLevelScene()
    {
        int CurrentId = (int)FindLevelId(SceneManager.GetActiveScene().name);
        if(CurrentId+1 < LevelsList.LevelsNameList.Count) 
        { 
        string NextSceneName = LevelsList.LevelsNameList[CurrentId + 1];
        SceneManager.LoadScene(NextSceneName);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    public int CheckMaxLevelIdAndSave()
    {
        string CurrentLevelName = progress.CurrentLevelName;
        if (FindLevelId(CurrentLevelName)!=null)
        {
            int currentId =(int)FindLevelId(CurrentLevelName);
            if (currentId > progress.MaxLevelId)
            {
                progress.MaxLevelId =  currentId;
            }
        return progress.MaxLevelId;
        }
        else
        {
            int NumberForUnlockButtons = -1;
            return NumberForUnlockButtons;
        }
    }

   
    public void SetPlayerInfo(string value) // вызывается из jslib
    {

        SaveData progr = JsonUtility.FromJson<SaveData>(value);
        progress.CurrentLevelName = progr.CurrentLevelName;
        progress.MaxLevelId = progr.index;
    }
    public void ResetProgress() // кнопка в главном меню
    {
        progress.CurrentLevelName = string.Empty;
        progress.MaxLevelId = 0;
    }
}
