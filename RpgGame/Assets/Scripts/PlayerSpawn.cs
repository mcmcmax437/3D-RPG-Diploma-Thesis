using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    public GameObject[] Characters;
    public Transform spawn_point;

 void Start()
    {
        Vector3 spawnPosition = spawn_point.position;
        Quaternion spawnRotation = spawn_point.rotation;

        Instantiate(Characters[SaveScript.player_index_character], spawnPosition, spawnRotation);

        // Debug log to print the spawn position
        //Debug.Log("Spawned at: " + spawnPosition);
    }
 
}
