using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class LevelMenuManager : MonoBehaviour
{
    public List<GameObject> LevelButtons;
    private int _levelsCountInGroup = 21;
    [SerializeField] private GameObject WinGamePanel;
    [SerializeField] private GameObject DifficultyUI;
    [SerializeField] private GameObject LevelGroup1;
    [SerializeField] private GameObject LevelGroup2;
    [SerializeField] private GameObject ToPreviousGroupButton;
    [SerializeField] private GameManager _gameManager;
    private void Start()
    {
                int NumberToUnlockButtons = _gameManager.CheckMaxLevelIdAndSave();
                UnlockButtons(NumberToUnlockButtons);
                SelectUILevelGroup(NumberToUnlockButtons);
   
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.U) && Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.L))
        {
            UnlockButtons(LevelButtons.Count - 1);
            SelectUILevelGroup(LevelButtons.Count - 1);
        }
        if (Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.T) && Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.E))
        {
            UnlockButtons(19);
            SelectUILevelGroup(19);

        }
    }
    public void UnlockButtons(int id)
    {
        if(id > 1)
        {
            DifficultyUI.SetActive(true);   
        }
        for (int i = 0; i <= id; i++)
        {
            LevelButtons[i].GetComponent<Button>().enabled = true;
            LevelButtons[i].GetComponent<Image>().color = Color.green;

        }
        if (id + 2 > LevelButtons.Count && PlayerPrefs.HasKey("WinGame")==false)
        {
            PlayerPrefs.SetInt("WinGame", 1);
            WinGamePanel.SetActive(true);
            return;
        }
        for (int i = id+2; i < LevelButtons.Count; i++)
        {
            LevelButtons[i].GetComponent<Button>().enabled = false;
            LevelButtons[i].GetComponent<Image>().color = Color.black; 

        }
    }
    public void SelectUILevelGroup(int id)
    {
            if(id >= _levelsCountInGroup - 1)
        {
            LevelGroup1.SetActive(false);
            LevelGroup2.SetActive(true);
            ToPreviousGroupButton.SetActive(true);
            
        }
        else
        {
            LevelGroup1.SetActive(true);
            LevelGroup2.SetActive(false);
        }
    }
    public void LoadScene(int id)
    {
        SceneManager.LoadScene(_gameManager.LevelsList.LevelsNameList[id]);
    }
    public int FindID(GameObject button)
    {
        return LevelButtons.IndexOf(button);
    }
    [ContextMenu("Reset Win Prefs")]
public void ResetWinGamePanel()
    {
        PlayerPrefs.DeleteKey("WinGame");
    }
}
