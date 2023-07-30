using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float volumeChangeSpeed;

    public Action<float> onMusicVolumeChanged;
    public Action<float> onSFXVolumeChanged;

    string musicVolumeParameter = "MusicVolume";
    string sfxVolumeParameter   = "SFXVolume";

    float volumePct;

    Dictionary<string, MixerParameter> audioMixerDict = new Dictionary<string, MixerParameter>();

    private void Start()
    {
        if (!audioMixerDict.ContainsKey(musicVolumeParameter))
        {
            audioMixerDict.Add(musicVolumeParameter, new MixerParameter(audioMixer, musicVolumeParameter));
        }

        if (!audioMixerDict.ContainsKey(sfxVolumeParameter))
        {
            audioMixerDict.Add(sfxVolumeParameter, new MixerParameter(audioMixer, sfxVolumeParameter));
        }

        audioMixerDict[musicVolumeParameter].SetVolume(80);
        onSFXVolumeChanged?.Invoke(80);
        audioMixerDict[sfxVolumeParameter].SetVolume(80);
        onMusicVolumeChanged?.Invoke(80);
    }

    public void incrMusicVolume()
    {
        if (!audioMixerDict.ContainsKey(musicVolumeParameter))
        {
            audioMixerDict.Add(musicVolumeParameter, new MixerParameter(audioMixer, musicVolumeParameter));
        }
        audioMixer.GetFloat(musicVolumeParameter, out volumePct);
        if (volumePct + 80 < 100)
        {
            audioMixerDict[musicVolumeParameter].IncreaseVolume();
            audioMixer.GetFloat(musicVolumeParameter, out volumePct);
            volumePct += 80;
            onMusicVolumeChanged?.Invoke(volumePct);
            Debug.Log($"VP: {volumePct}");
        }
        else { 
            audioMixer.SetFloat(musicVolumeParameter, 20);
        }
    }

    public void incrSFXVolume()
    {
        if (!audioMixerDict.ContainsKey(sfxVolumeParameter))
        {
            audioMixerDict.Add(sfxVolumeParameter, new MixerParameter(audioMixer, sfxVolumeParameter));
        }
        audioMixer.GetFloat(sfxVolumeParameter, out volumePct);
        if (volumePct + 80 < 100)
        {
            audioMixerDict[sfxVolumeParameter].IncreaseVolume();
            audioMixer.GetFloat(sfxVolumeParameter, out volumePct);
            volumePct += 80;
            onSFXVolumeChanged?.Invoke(volumePct);
        }
        else
        {
            audioMixer.SetFloat(sfxVolumeParameter, 20);
        }
    }

    public void decrMusicVolume()
    {
        if (!audioMixerDict.ContainsKey(musicVolumeParameter))
        {
            audioMixerDict.Add(musicVolumeParameter, new MixerParameter(audioMixer, musicVolumeParameter));
        }
        audioMixer.GetFloat(musicVolumeParameter, out volumePct);
        if (volumePct + 80 > 0)
        {

            audioMixerDict[musicVolumeParameter].DecreaseVolume();
            audioMixer.GetFloat(musicVolumeParameter, out volumePct);
            volumePct += 80;
            Debug.Log($"VP: {volumePct}");
            onMusicVolumeChanged?.Invoke(volumePct);
        }
        else
        {
            audioMixer.SetFloat(musicVolumeParameter, -80);
        }
    }

    public void decrSFXVolume()
    {
        if (!audioMixerDict.ContainsKey(sfxVolumeParameter))
        {
            audioMixerDict.Add(sfxVolumeParameter, new MixerParameter(audioMixer, sfxVolumeParameter));
        }
        audioMixer.GetFloat(sfxVolumeParameter, out volumePct);
        if (volumePct + 80 > 0)
        {

            audioMixerDict[sfxVolumeParameter].DecreaseVolume();
            audioMixer.GetFloat(sfxVolumeParameter, out volumePct);
            volumePct += 80;
            onSFXVolumeChanged?.Invoke(volumePct);
        }
        else
        {
            audioMixer.SetFloat(sfxVolumeParameter, -80);
        }
    }

    internal float GetVolume()
    {
        audioMixer.GetFloat("MusicVolume", out volumePct);
        volumePct += 80;
        return volumePct;
    }

}
public class MixerParameter
{
    AudioMixer audioMixer;
    string parameter;


    public event Action<float> ParameterChanged;
    public MixerParameter(AudioMixer mixer, string parameter)
    {
        audioMixer = mixer;
        this.parameter = parameter;
    }

    public void SetVolume(float perc)
    {
        audioMixer.GetFloat(parameter, out float currentMixerValue);
        currentMixerValue = perc - 80;

        audioMixer.SetFloat(parameter, currentMixerValue);
        ParameterChanged?.Invoke(currentMixerValue);
    }

    public void IncreaseVolume()
    {
        audioMixer.GetFloat(parameter, out float currentMixerValue);
        currentMixerValue += 0.2f;
        //currentMixerValue = Mathf.Clamp01(currentMixerValue);
        //float newMixerValue = GetMixerValue(currentMixerValue);

        audioMixer.SetFloat(parameter, currentMixerValue);
        ParameterChanged?.Invoke(currentMixerValue);
    }
    
    public void DecreaseVolume()
    {
        audioMixer.GetFloat(parameter, out float currentMixerValue);
        currentMixerValue -= 0.2f;
        //currentMixerValue = Mathf.Clamp01(currentMixerValue);
        //float newMixerValue = GetMixerValue(currentMixerValue);

        audioMixer.SetFloat(parameter, currentMixerValue);
        ParameterChanged?.Invoke(currentMixerValue);
    }

}
