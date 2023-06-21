#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneObjectFinder : MonoBehaviour
{
    [MenuItem("Audio Tools/Find non mixergroup audio source")]
    static void FindAudioSourceWithoutMixerGroup()
    {
        var audioSources = FindObjectsOfType<AudioSource>();

        if (audioSources.Length == 0)
        {
            return;
        }
        foreach (var audioSource in audioSources)
        {
            bool continueToNext = false;


            if (audioSource.outputAudioMixerGroup == null)
            {
                Debug.Log(audioSource.gameObject);
                Selection.activeObject = audioSource;
                bool findNext = EditorUtility.DisplayDialog("Results", "Audio sources without mixer group attached found", "ok");
                break;


            }
        }

    }


}
#endif