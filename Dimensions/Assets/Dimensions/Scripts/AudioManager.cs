using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float volumeChangeSpeed;

    public Action<float> onMusicVolumeChanged;
    public Action<float> onSFXVolumeChanged;

    float volumePct;
    Dictionary<string, MixerParameter> audioMixerDict = new Dictionary<string, MixerParameter>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void incrMusicVolume()
    {
        string parameter = "MusicVolume";
        if (!audioMixerDict.ContainsKey(parameter))
        {
            audioMixerDict.Add(parameter, new MixerParameter(audioMixer, parameter));
        }
        audioMixer.GetFloat(parameter, out volumePct);
        if (volumePct + 80 < 100)
        {
            audioMixerDict[parameter].IncreaseVolume();
            audioMixer.GetFloat(parameter, out volumePct);
            volumePct += 80;
            onMusicVolumeChanged?.Invoke(volumePct);
        }
        else { 
            audioMixer.SetFloat(parameter, 20);
        }
    }

    public void incrSFXVolume()
    {
        string parameter = "SFXVolume";
        if (!audioMixerDict.ContainsKey(parameter))
        {
            audioMixerDict.Add(parameter, new MixerParameter(audioMixer, parameter));
        }
        audioMixer.GetFloat(parameter, out volumePct);
        if (volumePct + 80 < 100)
        {
            audioMixerDict[parameter].IncreaseVolume();
            audioMixer.GetFloat(parameter, out volumePct);
            volumePct += 80;
            onSFXVolumeChanged?.Invoke(volumePct);
        }
        else
        {
            audioMixer.SetFloat(parameter, 20);
        }
    }

    public void decrMusicVolume()
    {
        string parameter = "MusicVolume";
        if (!audioMixerDict.ContainsKey(parameter))
        {
            audioMixerDict.Add(parameter, new MixerParameter(audioMixer, parameter));
        }
        audioMixer.GetFloat(parameter, out volumePct);
        if (volumePct + 80 > 0)
        {

            audioMixerDict[parameter].DecreaseVolume();
            audioMixer.GetFloat(parameter, out volumePct);
            volumePct += 80;
            onMusicVolumeChanged?.Invoke(volumePct);
        }
        else
        {
            audioMixer.SetFloat(parameter, -80);
        }
    }

    public void decrSFXVolume()
    {
        string parameter = "SFXVolume";
        if (!audioMixerDict.ContainsKey(parameter))
        {
            audioMixerDict.Add(parameter, new MixerParameter(audioMixer, parameter));
        }
        audioMixer.GetFloat(parameter, out volumePct);
        if (volumePct + 80 > 0)
        {

            audioMixerDict[parameter].DecreaseVolume();
            audioMixer.GetFloat(parameter, out volumePct);
            volumePct += 80;
            onSFXVolumeChanged?.Invoke(volumePct);
        }
        else
        {
            audioMixer.SetFloat(parameter, -80);
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
