using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonLevelLoad : MonoBehaviour
{
	
	public string LevelToLoad;
	
	public void LoadLevel()
	{
		//Load the level from LevelToLoad
		SceneManager.LoadScene(LevelToLoad);
	}
}
