using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
	internal List<GameObject> Enemies;
	public   float            stunRange = 3;


	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		Enemies = FindObjectsOfType<DroneAI>().ToList().ConvertAll(x => x.gameObject);
		
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StunCloseEnemy();
		}
	}

	private void StunCloseEnemy()
	{
		var orderedEnimies = Enemies.OrderBy(a => Vector3.Distance(a.gameObject.transform.position, transform.position)).ToList();

		GameObject closestEnemy = null;

		for (var i = 0; i < orderedEnimies.Count; i++)
		{
			closestEnemy = orderedEnimies[i];
			var dis = Vector3.Distance(closestEnemy.gameObject.transform.position, transform.position);
			
			if (closestEnemy && dis<stunRange)
			{
				closestEnemy.GetComponent<DroneAI>().StunDrone();
			}
			else
			{
			}
		}
	}
}