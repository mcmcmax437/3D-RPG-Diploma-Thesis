using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Weapon : MonoBehaviour
{
    public int weapon_index;

    public AudioClip selection;
    public AudioSource Inventory_Canvas;
         
    public void Choose_Weapon()
    {
        SaveScript.weapon_index = weapon_index;
        SaveScript.should_change_weapon = true;
        SaveScript.is_character_equip_a_weapon = true;

        Inventory_Canvas.clip = selection;
        Inventory_Canvas.Play();
    }
}
