using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to apply damage to the Robots (the Main Character).
/// It must be attached to an Enemy (A.I.), with a link in a Field (Attribute of this Class) that points to the Robot (i.e.: Main Player).
/// </summary>
public class Damage : MonoBehaviour
{
	
	public float damageAmount = 10.0f;
	
	public bool damageOnTrigger = true;
	public bool damageOnCollision = false;
	public bool continuousDamage = false;
	public float continuousTimeBetweenHits = 0;

	public bool destroySelfOnImpact = false;	// variables dealing with exploding on impact (area of effect)
	public float delayBeforeDestroy = 0.0f;
	public GameObject explosionPrefab;

	private float _savedTime = 0;

	/// <summary>
	/// Used for things like bullets, which are triggers.  
	/// </summary>
	/// <param name="collision"></param>
	void OnTriggerEnter(Collider collision)
	{
		if (damageOnTrigger)
		{
			
			if (/*this.CompareTag("PlayerBullet") &&*/ collision.gameObject.CompareTag("Player"))	// if the player got hit with it's own bullets, ignore it
				return;
		
			if (collision.gameObject.GetComponent<Health> () != null)
			{	
				// if the hit object has the Health script on it, deal damage
				//
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
		
				if (destroySelfOnImpact)
				{
					Destroy (gameObject, delayBeforeDestroy);	  // destroy the object whenever it hits something
				}
			
				if (explosionPrefab != null)
				{
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	/// <summary>
	/// This is used for things that explode on impact and are NOT triggers
	/// </summary>
	/// <param name="collision"></param>
	void OnCollisionEnter(Collision collision) 						
	{	
		if (damageOnCollision)
		{
			if (/*this.CompareTag("PlayerBullet") &&*/ collision.gameObject.CompareTag("Player"))	// if the player got hit with it's own bullets, ignore it
				return;
		
			if (collision.gameObject.GetComponent<Health> () != null)
			{
				// if the hit object has the Health script on it, deal damage
				//
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
			
				if (destroySelfOnImpact)
				{
					Destroy (gameObject, delayBeforeDestroy);	  // destroy the object whenever it hits something
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	#region Damage to the Main Player (when an Enemy Attacks the Player)

	/// <summary>
	/// this is used for damage over time things, and over the Player.
	/// </summary>
	/// <param name="collision"></param>
	void OnCollisionStay(Collision collision) 
	{
		if (continuousDamage)
		{
			if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Health> () != null)
			{
				// is only triggered if whatever it hits is the player
				//
				if (Time.time - _savedTime >= continuousTimeBetweenHits)
				{
					_savedTime = Time.time;
					collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
					
				}// End if (Time.time
				
			}// End if (collision.gameObject.CompareTag("Player")
			
		}//End if (continuousDamage)
		
	}//End OnCollisionStay

	#endregion Damage to the Main Player (when an Enemy Attacks the Player)

	
}