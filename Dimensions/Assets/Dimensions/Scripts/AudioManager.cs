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

    public Action<float> volumeChanged;

    float volumePct;
    Dictionary<string, MixerParameter> audioMixerDict = new Dictionary<string, MixerParameter>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void incrVolume(string parameter)
    {
        if (!audioMixerDict.ContainsKey(parameter))
        {
            audioMixerDict.Add(parameter, new MixerParameter(audioMixer, parameter));
        }
        audioMixerDict[parameter].IncreaseVolume();
         audioMixer.GetFloat("MusicVolume",out volumePct);
        volumeChanged?.Invoke(volumePct);
    }
    public void decrVolume(string parameter)
    {
        if (!audioMixerDict.ContainsKey(parameter))
        {
            audioMixerDict.Add(parameter, new MixerParameter(audioMixer, parameter));
        }
        audioMixerDict[parameter].DecreaseVolume();
        audioMixer.GetFloat("MusicVolume", out volumePct);
        volumeChanged?.Invoke(volumePct);
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
        currentMixerValue += 0.1f;
        //currentMixerValue = Mathf.Clamp01(currentMixerValue);
        //float newMixerValue = GetMixerValue(currentMixerValue);

        audioMixer.SetFloat(parameter, currentMixerValue);
        ParameterChanged?.Invoke(currentMixerValue);
    }
    public void DecreaseVolume()
    {
        audioMixer.GetFloat(parameter, out float currentMixerValue);
        currentMixerValue -= 0.1f;
        //currentMixerValue = Mathf.Clamp01(currentMixerValue);
        //float newMixerValue = GetMixerValue(currentMixerValue);

        audioMixer.SetFloat(parameter, currentMixerValue);
        ParameterChanged?.Invoke(currentMixerValue);
    }
    private float GetMixerValue(float value)
    {
        return Mathf.Log10(value) * 30;
    }
}
