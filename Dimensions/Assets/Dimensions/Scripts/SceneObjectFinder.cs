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
            EditorUtility.DisplayDialog("Results", "No audio sources without mixer group attached found", "ok");
            return;
        }

        GameObject gameObject = new GameObject("AudioSource Finder");
        var audioFinder = gameObject.AddComponent<SceneObjectFinder>();

        audioFinder.InitalizeSearch(audioSources);
    }

    void InitalizeSearch(AudioSource[] audioSources)
    {
        StartCoroutine(CheckAllAudioSources(audioSources));
    }

    IEnumerator CheckAllAudioSources(AudioSource[] audioSources)
    {
        foreach (var audioSource in audioSources)
        {
            bool continueToNext = false;
            while (!continueToNext)
            {

                if (audioSource.outputAudioMixerGroup == null)
                {
                    Debug.Log(audioSource.gameObject);
                    Selection.activeObject = audioSource;
                    bool findNext = EditorUtility.DisplayDialog("Continue search", "Would you like to continue searching AudioSources without mixer group", "yes");

                    if (findNext)
                    {
                        continueToNext = true;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    continueToNext = true;
                }
                yield return null;

            }
        }
        DestroyImmediate(gameObject);
    }
}
#endif