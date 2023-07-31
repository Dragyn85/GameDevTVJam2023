using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeDisplay : MonoBehaviour
{
    TMP_Text musicVolume;

    enum VolumeType { Music, SFX };
    AudioManager audioManager;

    [SerializeField] VolumeType volumeType;
    string parameterName;
    private void Awake()
    {
        if (volumeType == VolumeType.Music)
        {
            parameterName = "MusicVolume";
        }
        if (volumeType == VolumeType.SFX)
        {
            parameterName = "SFXVolume";
        }
        
    }
    void Start()
    {
        audioManager = FindObjectOfType<GameServiceLocator>().GetService<AudioManager>();
        musicVolume = GetComponent<TMP_Text>();
        audioManager.onAnyVolumeChanged += UpdateVolume;
        float volume = audioManager.GetVolume(parameterName);
        MixerParameter mp = audioManager.GetMixerParameter(parameterName);
        UpdateVolume(volume, mp);
    }

    private void UpdateVolume(float volume, MixerParameter parameter)
    {
           if (parameter.Name != parameterName) { return; }

        musicVolume.SetText(volume.ToString("0") + "%");
    }
}
