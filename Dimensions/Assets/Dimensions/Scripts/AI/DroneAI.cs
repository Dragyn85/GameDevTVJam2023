using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class DroneAI : MonoBehaviour
{
    [SerializeField] internal float     moveSpeed   = 2;
    [SerializeField] internal float     rotateSpeed = 3;
    [SerializeField] internal LayerMask _layerMask;
    [SerializeField] internal float     STUN_TIME = 3;

    internal List<GameObject> players;
    internal ParticleSystem   _particleSystem;

    private Rigidbody rb;
    internal float stunTime = 0;


    void Start()
    {
        rb              = GetComponent<Rigidbody>();
        
        // Original code, David: players         = FindObjectsOfType<Player>().ToList().ConvertAll(x => x.gameObject);
        // Intent, by Alejandro: 28/05/2023:   It is not optimized, takes too much of our CPUs in Start time:
        // Get the players from the GameManager:
        //
        players = new List<GameObject>()
        {
            GameManager.Gm.PlayerToTheLeft,
            GameManager.Gm.PlayerToTheRight
        };

        _particleSystem = GetComponent<ParticleSystem>();
    }


    private string lastPlayersName;

    void Update()
    {
        UpdateDrone(Time.deltaTime);
        DebugStunActivator();
    }

    private void DebugStunActivator()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StunDrone();
        }
    }

    public void StunDrone()
    {
        stunTime = STUN_TIME;
        _particleSystem.Play();
    }


    private void UpdateDrone(float deltaTime)
    {
        if (stunTime > 0)
        {
            DoStun(deltaTime);
        }
        else
        {
            DoMovement();
        }
    }

    private void DoStun(float deltaTime)
    {
        stunTime -= deltaTime;
        if (stunTime < 0)
        {
            stunTime = 0;
            _particleSystem.Stop();
        }
    }

    private void DoMovement()
    {
        // Alejandro 2023/05/28:  I am fortifying (adding mover robustness...) to this Guard:
        //
        if ((players.Count <= 2) && (players[0] != null) && (players[1] != null))
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
