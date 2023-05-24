using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUPERCharacter;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    internal List<GameObject> players;

    
    void Start()
    {
        players = FindObjectsOfType<SUPERCharacterAIO>().ToList().ConvertAll(x => x.gameObject);
    }


    void Update()
    {
        
    }
}
