using TMPro;
using UnityEngine;

public class VolumeDisplay : MonoBehaviour
{
    TMP_Text musicVolume;
    enum VolumeType { Music, SFX };

    [SerializeField] VolumeType volumeType;
    void Start()
    {
        musicVolume = GetComponent<TMP_Text>();
        if (volumeType == VolumeType.Music)
        {
            AudioManager.Instance.onMusicVolumeChanged += UpdateVolume;
        }
        else
        {
            AudioManager.Instance.onSFXVolumeChanged += UpdateVolume;
        }
        UpdateVolume(AudioManager.Instance.GetVolume());
    }

    private void UpdateVolume(float volume)
    {
        musicVolume.SetText(volume.ToString("0") + "%");
    }

}
