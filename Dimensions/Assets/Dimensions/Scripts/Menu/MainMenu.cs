using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] int firstLevelIndex;
    [SerializeField] MainMenuItem musicUpButton;
    [SerializeField] MainMenuItem musicDownBytton;
    [SerializeField] MainMenuItem sfxUpButton;
    [SerializeField] MainMenuItem sfxDownBytton;
    int loadLevelIndex;
    private void Start()
    {
        audioManager = FindObjectOfType<GameServiceLocator>().GetService<AudioManager>();
        musicDownBytton.onStay.AddListener(audioManager.decrMusicVolume);
        musicUpButton.onStay.AddListener(audioManager.incrMusicVolume);
        sfxDownBytton.onStay.AddListener(audioManager.decrSFXVolume);
        sfxUpButton.onStay.AddListener(audioManager.incrSFXVolume);
    }

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
