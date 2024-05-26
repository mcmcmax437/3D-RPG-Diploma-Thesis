using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private GameObject main_character;
    void Start()
    {
        StartCoroutine(Search_For_Main_Character());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(main_character == null)
        {
            Search_For_Main_Character();
        }
        if (main_character != null)
        {
            Vector3 mini_map_camera_position = main_character.transform.position;
            mini_map_camera_position.y = transform.position.y;
            transform.position = mini_map_camera_position;
        }
    }

    IEnumerator Search_For_Main_Character()
    {
        yield return new WaitForSeconds(1);
        main_character = GameObject.FindGameObjectWithTag("Player");
    }
}
 