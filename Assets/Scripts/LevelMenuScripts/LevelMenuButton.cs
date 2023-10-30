using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuButton : MonoBehaviour
{
    public void LoadLevel()
    {
        LevelMenuManager levelMenuManager = FindObjectOfType<LevelMenuManager>();
        int buttonId = levelMenuManager.FindID(gameObject);
        levelMenuManager.LoadScene(buttonId);
    }
}
