using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl_Up_Stats : MonoBehaviour
{
    public AudioClip selection;
    public AudioSource Inventory_Canvas;

   

    public void Lvl_UP_Strength()
    {
        if (SaveScript.points_to_upgrade > 0)
        {
            // SaveScript.strength_basic += SaveScript.player_lvl_character;
            SaveScript.strength_basic += 0.05f;
            SaveScript.points_to_upgrade--;
        }
    }

    public void Lvl_UP_Intelligence()
    {
        if (SaveScript.points_to_upgrade > 0)
        {
            // SaveScript.intelligence_basic += SaveScript.player_lvl_character;
            SaveScript.intelligence_basic += 0.05f;
            SaveScript.points_to_upgrade--;
        }
    }

    public void Lvl_UP_Stamina()
    {
        if (SaveScript.points_to_upgrade > 0)
        {
            //  SaveScript.stamina_basic += SaveScript.player_lvl_character;
            SaveScript.stamina_basic += 0.05f;
            SaveScript.points_to_upgrade--;
        }
    }
}
