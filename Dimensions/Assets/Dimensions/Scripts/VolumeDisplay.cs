using TMPro;
using UnityEngine;

public class VolumeDisplay : MonoBehaviour
{
    TMP_Text musicVolume;
    void Start()
    {
        musicVolume = GetComponent<TMP_Text>();
        AudioManager.Instance.volumeChanged += UpdateVolume;
        UpdateVolume(AudioManager.Instance.GetVolume());
    }

    private void UpdateVolume(float volume)
    {
        musicVolume.SetText(volume.ToString("0")+"%");
    }

}
