using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the Health of the Main Player (the Hero).
/// NOTE:   It needs to be filled with Good practices, it needs a refactor for the Public fields (to convert them to Serialized private....)
/// </summary>
public class Health : MonoBehaviour
{
	
	public enum DeathAction {LoadLevelWhenDead, DoNothingWhenDead /*, WaitForSomeSecondToRespawnThePlayerNeverDieJustFreezes*/};
	
	public float healthPoints = 1f;
	public float respawnHealthPoints = 1f;		//base health points
	
	public int numberOfLives = 1;					//lives and variables for respawning
	public bool isAlive = true;	

	public GameObject explosionPrefab;
	
	public DeathAction onLivesGone = DeathAction.DoNothingWhenDead;
	
	public string LevelToLoad = "";
	
	private Vector3 _respawnPosition;
	private Quaternion _respawnRotation;


	// Use this for initialization
	void Start () 
	{
		// store initial position as respawn location
		//
		_respawnPosition = transform.position;
		_respawnRotation = transform.rotation;
		
		if (LevelToLoad=="") // default to current scene 
		{
			// Original (2017/09/15): LevelToLoad = Application.loadedLevelName;
			//
			LevelToLoad = SceneManager.GetActiveScene().ToString();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (healthPoints <= 0)
		{
			// if the object is 'dead'
			numberOfLives--;					// decrement # of lives, update lives GUI
			
			if (explosionPrefab!=null)
			{
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}
			
			if (numberOfLives > 0)
			{ 
				// Original code: respawn
				// transform.position = _respawnPosition;	// reset the player to respawn position
				// transform.rotation = _respawnRotation;
				
				// reset the player to respawn position and rotation:
				//
				gameObject.transform.SetPositionAndRotation(_respawnPosition, _respawnRotation);
				healthPoints = respawnHealthPoints;	// give the player full health again
			}
			else
			{
				// here is where you do stuff once ALL lives are gone)
				isAlive = false;
				
				switch(onLivesGone)
				{
					case DeathAction.LoadLevelWhenDead:

						// Original (2017/09/15): Application.LoadLevel (LevelToLoad);
						//
						SceneManager.LoadScene( LevelToLoad );

						break;
					
					case DeathAction.DoNothingWhenDead:
						// do nothing, death must be handled in another way elsewhere
						break;
				}
				Destroy(gameObject);
			}
		}
	}
	
	public void ApplyDamage(float amount)
	{	
		healthPoints = healthPoints - amount;	
	}
	
	public void ApplyHeal(float amount)
	{
		healthPoints = healthPoints + amount;
	}

	public void ApplyBonusLife(int amount)
	{
		numberOfLives = numberOfLives + amount;
	}
	
	public void UpdateRespawn(Vector3 newRespawnPosition, Quaternion newRespawnRotation)
	{
		_respawnPosition = newRespawnPosition;
		_respawnRotation = newRespawnRotation;
	}
}
