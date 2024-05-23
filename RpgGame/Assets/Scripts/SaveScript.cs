using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static bool class_Avarage = false; // 0
    public static bool class_Mage = false; // 1
    public static bool class_Seller = true; // 2
    public static bool class_Warrior = false; // 3

    public static int uniqe_features_index = 2;
    public static float time_of_uniqe_feature_activasion = -Mathf.Infinity;
    public static float uniqe_features_index_CD = 500f; // 500 sec couldown


    public static float player_lvl_character = 0.05f;
    public static int player_lvl_Display  = 1;
    public static int player_index_character = 0;
    public static string player_name = "player";
    public static GameObject vfx_spawn_point;
    public static GameObject spell_target;

    public static float mana = 1.0f;
    public static float mana_regeneration = 0.04f; // make it 0.02 (but now it is 0.04 for test in game)
    public static float stamina = 1.0f;
    public static float stamina_regeneration = 0.05f;
    public static float health = 1.0f;
    public static float health_regeneration_skill = 0.04f;
    public static float health_regeneration_passive = 0.01f;

    public static float time_of_last_damage_recive;

    public static bool is_Immmortal_object = false;

    public static bool is_invisible = false;
    public static bool is_shielf_active = false;

    public static float damage_reduce_by_Guardianship = 0.5f;
    public static int strength_increase = 0;

    public static float strength_basic = 0.05f;
    public static float intelligence_basic = 0.05f;
    public static float stamina_basic = 0.05f;

    private int enemies_to_lvl_UP = 1;
    public static int killed_enemy = 0;
    public static int points_to_upgrade;

    public static int weapon_index = -1; 
    public static bool should_change_weapon = false; //true - to test, false at finish
    public static bool is_character_equip_a_weapon = false;

    public static int index_of_equiped_armor = 0;  //7 - bassic, 8 - light, 9 - heave  //1 - light 2 - heavy
    public static bool should_change_armor = false;
    public static float critical_hit_chance = 0.2f;
    public static int critical_dmg_multiply = 2;

    public static int player_gold = 10000;
    public static int player_diamond = 50;
     
    public static int weapon_dmg_scaleUP;

    public static float armora_decrease = 0.0f;
    

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
            if(class_Mage == true)
            {
                
                mana += ((intelligence_basic / 10 + mana_regeneration)*1.2f) * Time.deltaTime;
            }
            else
            {
                mana += (intelligence_basic / 10 + mana_regeneration) * Time.deltaTime;
            }
          
        }
        if (mana <= 0)
        {
            mana = 0;          
        }
        if(mana < 0.03)
        {
            is_invisible = false;
            is_shielf_active = false;
            strength_increase = 0;
        }


        if (stamina < 1.0)
        {
            stamina += (stamina_basic/10 + stamina_regeneration) * Time.deltaTime; 
           // Debug.Log(stamina); 
        }
        if (stamina < 0)
        {
            stamina = 0;

            // StartCoroutine(WaitBeforeStaminaRegeneration());
        }


        float newTime = Time.time - time_of_last_damage_recive;
        //Debug.Log(Time.time + " - " + time_of_last_damage_recive + " = " + newTime);
        if (health < 0.7  && newTime >= 10f)
        {
            Health_Regeneration();
        }

       


        //Debug.Log(weapon_index);

        if (killed_enemy == enemies_to_lvl_UP)
        {
            player_lvl_character += 0.05f;
            enemies_to_lvl_UP = killed_enemy + 2;
            points_to_upgrade++;
            player_lvl_Display++;
            //Stats_Update_LVL_UP();
            Weapon_DMG_LVL_UP();     
        }

        if(index_of_equiped_armor == 1)
        {
            armora_decrease = 0.2f;
        }
        if (index_of_equiped_armor == 2)
        {
            armora_decrease = 0.4f;
        }
    }

    IEnumerator WaitBeforeStaminaRegeneration()
    {
        yield return new WaitForSeconds(3);
        stamina += (stamina_regeneration*2) * Time.deltaTime;
    }

    public void Health_Regeneration()
    {
        
        health += health_regeneration_passive * Time.deltaTime;
   
    }

   /* public void Stats_Update_LVL_UP()
    {
        strength_basic = player_lvl_character;
        intelligence_basic = player_lvl_character;
        stamina_basic = player_lvl_character;
    }*/
    

    public void Weapon_DMG_LVL_UP()
    {
        weapon_dmg_scaleUP = System.Convert.ToInt32(strength_basic * 100);
    }
}
 