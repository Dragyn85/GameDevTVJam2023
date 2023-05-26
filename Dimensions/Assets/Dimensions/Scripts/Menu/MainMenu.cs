using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int firstLevelIndex;
    int loadLevelIndex;

    public void NewGame()
    {
        SceneManager.LoadScene(firstLevelIndex);
    }

    public void LoadLevelIndex()
    {
        if (loadLevelIndex != 0)
        {
            SceneManager.LoadScene(loadLevelIndex);
        }
    }

    public void SetLevelToLoad(int index)
    {
        loadLevelIndex = index;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
