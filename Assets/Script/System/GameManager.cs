using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // find the spawn holder in the game
        Blackboard.spawnHolder = GameObject.FindGameObjectWithTag("CacheHolder").transform;
    }

}
