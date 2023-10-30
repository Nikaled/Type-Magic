using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    public static void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public static void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
