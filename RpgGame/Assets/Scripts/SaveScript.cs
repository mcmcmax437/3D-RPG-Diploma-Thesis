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
    public static float stamina = 1.0f;
    public static float stamina_regeneration = 0.05f;
    public static bool is_invisible = false;

    public static float strength_basic = 0.1f;
    public static float intelligence_basic = 0.1f;
    public static float stamina_basic = 0.1f;

    public static int killed_enemy = 0;

    public static int weapon_index = -1;
    public static bool should_change_weapon = false; //true - to test, false at finish
    public static bool is_character_equip_a_weapon = false;

    public static int index_of_equiped_armor = 0;  //7 - bassic, 8 - light, 9 - heave
    public static bool should_change_armor = false;
    public static float critical_hit_chance = 1.0f;
    public static int critical_dmg_multiply = 2;

    public static int player_gold = 1000;
    public static int player_diamond = 50;

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


        if (stamina < 1.0)
        {
            stamina += stamina_regeneration * Time.deltaTime;
           // Debug.Log(stamina); 
        }
        if (stamina < 0)
        {
            stamina = 0;
           // StartCoroutine(WaitBeforeRegeneration());
        }

        //Debug.Log(weapon_index);

    }

    IEnumerator WaitBeforeRegeneration()
    {
        yield return new WaitForSeconds(3);
        stamina += (stamina_regeneration*2) * Time.deltaTime;
    }
}
 