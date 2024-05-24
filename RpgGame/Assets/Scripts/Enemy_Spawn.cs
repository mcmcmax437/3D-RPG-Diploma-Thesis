using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public GameObject main_Camera;
    public GameObject[] enemies_for_spawn;
    public Transform[] places_for_spawn;

    public bool re_useable_trigger = false;

    private bool was_already_spawned = false;

    void Update()
    {
        if(SaveScript.amount_of_chasing_enemies <= 0)
        {
            if(was_already_spawned == true)
            {
                ChangeMusicTheme();
                if(re_useable_trigger == true)
                {
                    was_already_spawned = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(was_already_spawned == false)
            {
                was_already_spawned = true;
                for (int i = 0; i < enemies_for_spawn.Length; i++)
                {
                    SpawnEnemies(i);
                }
                ChangeMusicTheme();
            }
        }
    }

    private void SpawnEnemies(int i)
    {
        Instantiate(enemies_for_spawn[i], places_for_spawn[i].position, places_for_spawn[i].rotation);
        SaveScript.amount_of_chasing_enemies++;
    }
    private void ChangeMusicTheme()
    {
        if (SaveScript.amount_of_chasing_enemies > 0)
        {
            main_Camera.GetComponent<AudioSource>().volume = 1.0f;
            main_Camera.GetComponent<Music>().music_status = 3;
        }
        else
        {
            main_Camera.GetComponent<AudioSource>().volume = 0.6f;
            main_Camera.GetComponent<Music>().music_status = 1;
        }
        main_Camera.GetComponent<Music>().should_play = true;
    }
}
