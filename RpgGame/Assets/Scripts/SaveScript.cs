using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static int player_index_character = 0;
    public static string player_name = "player";


    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    /*void Update()
    {
       // Debug.Log("Name = " + player_name);
       // Debug.Log("Index = " + player_index_character);

    }*/
}
 