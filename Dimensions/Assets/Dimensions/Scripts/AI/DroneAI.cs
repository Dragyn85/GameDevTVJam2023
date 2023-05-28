using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class DroneAI : MonoBehaviour
{
    [FormerlySerializedAs("movepeed")] [SerializeField]
    internal float moveSpeed = 2;
    [FormerlySerializedAs("rotSpeed")] [SerializeField]
    internal float rotateSpeed = 3;

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
                    MoveTowardPlayer(closestPlayer);
                    break;
                }
                else
                {
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

        rb.velocity = dir * moveSpeed;


        var angRot = Vector3.Cross(dir, transform.forward) * rotateSpeed;
        rb.angularVelocity = angRot;

        var playersName = playerGO.gameObject.name;
        if (playersName != lastPlayersName)
        {
            lastPlayersName = playersName;
        }
    }



    internal bool PlayerVisible(GameObject player)
    {
        var dronPos   = transform.position;
        var playerPos = player.transform.position;
        playerPos.y += 1;
        var raycast   = Physics.Linecast(dronPos, playerPos, _layerMask);

        return !raycast;
    }
}
