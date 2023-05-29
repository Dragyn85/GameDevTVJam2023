using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour
{

	public int value = 1000;
	public GameObject explosionPrefab;

	[Tooltip("Destroy this GameObject with the collision?")]
	[SerializeField]
	public bool _destroyThisGameObject = false;


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (GameManager.Gm != null)
			{
				// tell the game manager to Collect
				GameManager.Gm.Collect (value);
				
				// Make the Player Disappear, because he won (we don't want him to collect this Treasure multiple times..):
				//
				other.gameObject.SetActive(false);
			}
			
			// explode if specified
			if (explosionPrefab != null)
			{
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}
			
			// destroy after collection
			if (_destroyThisGameObject)
			{
				Destroy (gameObject);
			}
		}
	}
}
