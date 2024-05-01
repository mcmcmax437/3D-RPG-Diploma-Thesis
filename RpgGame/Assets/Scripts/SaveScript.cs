using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static int player_index_character = 0;
    public static string player_name = "player";
    public static GameObject vfx_spawn_point;
    public static GameObject spell_target;

    public static float mana = 1.0f;
    public static float mana_regeneration = 0.04f; // make it 0.02 (but now it is 0.04 for test in game)
    public static bool is_invisible = false;

 

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame 
    void Update()
    {
       // Debug.Log("Name = " + player_name);
       // Debug.Log("Index = " + player_index_character);  

        if(mana < 1.0)
        {
            mana += mana_regeneration * Time.deltaTime;
        }
        if (mana <= 0)
        {
            mana = 0;          
        }
        if(mana < 0.03)
        {
            is_invisible = false;
        }
    }
}
 