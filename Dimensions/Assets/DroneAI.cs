using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUPERCharacter;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    internal List<GameObject> players;

    private Rigidbody rb;

    
    void Start()
    {
        rb      = GetComponent<Rigidbody>();
        players = FindObjectsOfType<SUPERCharacterAIO>().ToList().ConvertAll(x => x.gameObject);
    }


    private string lastPlayersName;
    
    void Update()
    {
        var orderedPlayers = players.OrderBy(a => Vector3.Distance(a.gameObject.transform.position, transform.position)).ToList();
        var closestPlayer  = orderedPlayers[0];
        var playerDelta    = closestPlayer.transform.position - transform.position;
        var dir            = playerDelta.normalized;

        rb.velocity = dir;

        var angRot = Vector3.Cross(dir, transform.forward);
        rb.angularVelocity = angRot;

        var playersName = closestPlayer.gameObject.name;
        if (playersName != lastPlayersName)
        {
            Debug.Log($"Closest Player: {playersName}");
            lastPlayersName = playersName;
        }
    }
}
