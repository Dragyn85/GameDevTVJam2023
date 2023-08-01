using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonLevelLoad : MonoBehaviour
{
	public void LoadLevel()
	{
		//Load the level from LevelToLoad
		var activeScene = SceneManager.GetActiveScene();
		
		if (SceneManager.sceneCountInBuildSettings <= activeScene.buildIndex+1)
		{
			SceneManager.LoadScene(0);
		}
		else
		{
            SceneManager.LoadScene(activeScene.buildIndex+1);
        }
	}
}
