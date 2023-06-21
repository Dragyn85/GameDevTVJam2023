using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This is a class made for processing the Collection of any 'Pickup Items', such as: Coins, or Treasures. <br />
/// It can also be used for setting a 'Finish Goal' that, once your Main Player takes it: the Game is Over.
/// </summary>
public class Treasure : MonoBehaviour
{

	[Tooltip("Treasure value.")]
	[FormerlySerializedAs("value")]
	[SerializeField]
	private int _value = 1000;
	
	[Tooltip("Actor who will collide with this GameObject (i.e.: 'Player').")]
	[SerializeField]
	private string _collisionActorTag = $"Player";

	
	[Tooltip("Prefab VFX (or 'Particle System') to be spawned in-place, when the Collision with this Treasure happens.")]
	[FormerlySerializedAs("explosionPrefab")]
	[SerializeField]
	private GameObject _explosionPrefab;

	[Tooltip("Destroy this GameObject with the collision?")]
	[SerializeField]
	private bool _destroyThisGameObject = false;



	#region Unity Methods

	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag( _collisionActorTag ))
		{
			if (GameManager.Gm != null)
			{
				// tell the game manager to Collect
				GameManager.Gm.Collect (_value);
				
				// Make the 'Player' Disappear: Disable it...
				// ...because he won (we don't want him to collect this Treasure multiple times..):
				//
				other.gameObject.SetActive(false);
			}
			
			// Explode if specified
			//
			if (_explosionPrefab != null)
			{
				Instantiate (_explosionPrefab, transform.position, Quaternion.identity);
			}
			
			// Destroy after collection
			//
			if (_destroyThisGameObject)
			{
				Destroy (gameObject);
			}
		}
	}//End OnTriggerEnter

	#endregion Unity Methods
	
}
