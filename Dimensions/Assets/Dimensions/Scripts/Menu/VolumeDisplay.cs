using TMPro;
using UnityEngine;

public class VolumeDisplay : MonoBehaviour
{
    TMP_Text musicVolume;
    enum VolumeType { Music, SFX };
    AudioManager audioManager;

    [SerializeField] VolumeType volumeType;
    void Start()
    {
        audioManager = FindObjectOfType<GameServiceLocator>().GetService<AudioManager>();

        musicVolume = GetComponent<TMP_Text>();
        if (volumeType == VolumeType.Music)
        {
            audioManager.onMusicVolumeChanged += UpdateVolume;
        }
        else
        {
            audioManager.onSFXVolumeChanged += UpdateVolume;
        }
        UpdateVolume(audioManager.GetVolume());
    }

    private void UpdateVolume(float volume)
    {
        musicVolume.SetText(volume.ToString("0") + "%");
    }

}
