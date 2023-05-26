using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUPERCharacter;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    [SerializeField] internal float rotSpeed;

    [SerializeField] internal LayerMask _layerMask;

    internal List<GameObject> players;

    private Rigidbody rb;


    void Start()
    {
        rb      = GetComponent<Rigidbody>();
        players = FindObjectsOfType<Player>().ToList().ConvertAll(x => x.gameObject);
    }


    private string lastPlayersName;

    void Update()
    {
        if (players.Count == 2)
        {
            var orderedPlayers = players.OrderBy(a => Vector3.Distance(a.gameObject.transform.position, transform.position)).ToList();

            GameObject closestPlayer = null;

            for (var i = 0; i < 2; i++)
            {
                closestPlayer = orderedPlayers[i];

                if (closestPlayer && PlayerVisible(closestPlayer))
                {
                    Debug.Log("Move Toward Player");
                    MoveTowardPlayer(closestPlayer);
                    break;
                }
                else
                {
                    Debug.Log("Stop Moving");
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    internal void MoveTowardPlayer(GameObject playerGO)
    {
        var playerDelta = playerGO.transform.position - transform.position;
        var dir         = playerDelta.normalized;
        dir.y = 0;

        rb.velocity = dir; //


        var angRot = Vector3.Cross(dir, transform.forward) * rotSpeed;
        rb.angularVelocity = angRot;

        var playersName = playerGO.gameObject.name;
        if (playersName != lastPlayersName)
        {
            Debug.Log($"Closest Player: {playersName}");
            lastPlayersName = playersName;
        }
    }



    internal bool PlayerVisible(GameObject player)
    {
        var dronPos   = transform.position;
        var playerPos = player.transform.position;
        var raycast   = Physics.Linecast(dronPos, playerPos, _layerMask);

        return !raycast;
    }
}
