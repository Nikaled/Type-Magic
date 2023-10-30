using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Creator creator;
    [SerializeField] TextMeshProUGUI LevelText;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Controller.IsGameActive)
            {
                GamePause();
            }
            else
            {
                GameResume();
            }
        }
    }
    public void GamePause()
    {
        Controller.StopGame();
        PauseMenu.SetActive(true);
    }
    public void GameResume()
    {
        Controller.ResumeGame();
        PauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        Controller.OnCompletedAction += LevelComplete;
        KillingPlatform.PlatformKills += LevelFailed;
    }
    private void OnDisable()
    {
        Controller.OnCompletedAction -= LevelComplete;
        KillingPlatform.PlatformKills -= LevelFailed;

    }
    public  void SetWindowWithLevelText()
    {
        LevelText.text = creator.LevelText;
    }
    private void LevelComplete()
    {
        WinPanel.SetActive(true);
        GameManager.instance.progress.CurrentLevelName = SceneManager.GetActiveScene().name;
    }
    private void LevelFailed()
    {
        Controller.StopGame();
        GameOverPanel.SetActive(true);
    }
}
