using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] float syncronusTimeTreashold = 0.5f;

    Player playerInGoal;
    float timer;

    bool firstPlayerReachedGoal = false;
    bool completedSuccessfully = false;
    private void Update()
    {
        if (!firstPlayerReachedGoal) return;

        timer += Time.deltaTime;
        if (!completedSuccessfully && timer > syncronusTimeTreashold)
        {
            ResetLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player newPlayerInGoal = other.GetComponent<Player>();

        if (newPlayerInGoal == null) return;

        if(playerInGoal == null)
        {
            firstPlayerReachedGoal=true;
            playerInGoal = newPlayerInGoal;
            timer = 0;
        }
        else
        {
            if (newPlayerInGoal == playerInGoal) return;

            if(timer <= syncronusTimeTreashold)
            {
                CompleteLevel();
            }
            else
            {
                ResetLevel();
            }
        }
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CompleteLevel()
    {
        completedSuccessfully = true;
        GameManager.Gm.Collect(1000);
    }
}
