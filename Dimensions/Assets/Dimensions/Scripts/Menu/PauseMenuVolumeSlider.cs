using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuVolumeSlider : MonoBehaviour
{
    [SerializeField] private string _soundParameterName;
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _slider;
    [SerializeField] private float _multipier = 30f;


    private void Start()
    {
        var SL = GameServiceLocator.GetInstance();
        _slider.value = SL.GetService<AudioManager>().GetSoundNormalizedParameterValue(_soundParameterName);
        
        _slider.onValueChanged.AddListener(SliderChanged);
    }

    public void SliderChanged(float value)
    {
        GameServiceLocator.GetInstance().GetService<AudioManager>().SetVolume(_soundParameterName, value * 100);
    }
}
