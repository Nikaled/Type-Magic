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
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    public static extern void ShowAdv();

    void Start()
    {
            instance = this;
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            //            #if !UNITY_EDITOR
            //            LoadExtern();
            //#endif
            SoundPanel.instance.LoadVolumeSettings();

        }

    }
    public void LoadButton()
    {
        LoadExtern();

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
        SaveProgress();
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
            Debug.Log("GameManager.FindLevelId вернул null");
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
        if (FindLevelId(CurrentLevelName)!=null) // до этого было if CurrentLevelName != String.Empty
        {
            int currentId =(int)FindLevelId(CurrentLevelName);
            Debug.Log(currentId);
            if (currentId > progress.MaxLevelId)
            {
                progress.MaxLevelId =  currentId;
                SaveProgress();
                Debug.Log(progress.MaxLevelId + " - мах левел айди");
            }
        return progress.MaxLevelId;
        }
        else
        {
            int NumberForUnlockButtons = -1;
            return NumberForUnlockButtons;
        }
    }

    public void SaveProgress() // весь метод закоменчен пока не разберусь с инициализацией пользователя на Яндекс
    {
//        //#if !UNITY_EDITOR && UNITY_WEBGL____
//        SaveData save = new();
//        save.CurrentLevelName = progress.CurrentLevelName;
//        save.index = progress.MaxLevelId;
//        string jsonString = JsonUtility.ToJson(save);
//        SaveExtern(jsonString);
////#endif
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
        SaveProgress();
    }
}
