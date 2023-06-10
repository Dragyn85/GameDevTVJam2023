using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuVolumeSlider : MonoBehaviour
{
    [SerializeField] private string _soundParameterName;
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _slider;
    [SerializeField] private float _multipier = 30f;


    private void Awake()
    {

        if (PlayerPrefs.HasKey(_soundParameterName))
        {
            var volume = PlayerPrefs.GetFloat(_soundParameterName);
            var newMexerValue = volume < 0.01 ? -80 : GetMixerValue(volume);
            _mixer.SetFloat(_soundParameterName, newMexerValue);
            _slider.value = volume;
        }
        else
        {
            float mixerValue;
            if (_mixer.GetFloat(_soundParameterName, out mixerValue))
            {
                mixerValue += 80;
                _slider.value = mixerValue / 100;
            }
        }
        _slider.onValueChanged.AddListener(SliderChanged);
    }

    public void SliderChanged(float value)
    {
        var newMexerValue = value < 0.01 ? -80 : GetMixerValue(value);
        _mixer.SetFloat(_soundParameterName, newMexerValue);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_soundParameterName, _slider.value);

    }

    private float GetMixerValue(float value)
    {
        return Mathf.Log10(value) * _multipier;
    }
}
