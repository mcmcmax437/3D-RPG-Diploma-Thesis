using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God_Mode : MonoBehaviour
{
    public GameObject Inventory_Canvas;
    private int selected_key = 1;
    public static bool GM_Invise = false;

    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Make_Immortal()
    {
        SaveScript.is_Immmortal_object = true;
    }
    public void Make_Mortal()
    {
        SaveScript.is_Immmortal_object = false;
    }

    public void Unlock_All_Weapons()
    {
        for(int i = 0; i < Inventory_Canvas.GetComponent<Inventory>().weapons.Length; i++)
        {
            Inventory_Canvas.GetComponent<Inventory>().weapons[i] = true;
        } 
    }

    public void Unlock_Armor1()
    {
        SaveScript.index_of_equiped_armor = 1;
        SaveScript.should_change_armor = true;
    }
    public void Unlock_Armor2()
    {
        SaveScript.index_of_equiped_armor = 2;
        SaveScript.should_change_armor = true;
    }

    public void Crit_0()
    {
        SaveScript.critical_hit_chance = 0;
    }
    public void Crit_0_2()
    {
        SaveScript.critical_hit_chance = 0.2f;
    }
    public void Crit_1()
    {
        SaveScript.critical_hit_chance = 1;
    }

    public void Class_Change_Mage()
    {
        SaveScript.class_Avarage = false; // 0
            SaveScript.class_Mage = true; // 1
        SaveScript.class_Seller = false; // 2
        SaveScript.class_Warrior = false; // 3

        SaveScript.uniqe_features_index = 1;
    }
    public void Class_Change_Seller()
    {
        SaveScript.class_Avarage = false; // 0
        SaveScript.class_Mage = false; // 1
            SaveScript.class_Seller = true; // 2
        SaveScript.class_Warrior = false; // 3

        SaveScript.uniqe_features_index = 2;
    }
    public void Class_Change_Warrior()
    {
        SaveScript.class_Avarage = false; // 0
        SaveScript.class_Mage = false; // 1
        SaveScript.class_Seller = false; // 2
            SaveScript.class_Warrior = true; // 3

        SaveScript.uniqe_features_index = 3;
        SaveScript.time_of_uniqe_feature_activasion = -Mathf.Infinity;
    }

    public void Mana_Reg_0_5()
    {
        SaveScript.mana_regeneration = 0.5f;
    }
    public void Mana_Reg_0_0_4()
    {
        SaveScript.mana_regeneration = 0.04f;
    }

    public void Make_Invisiable()
    {

        SaveScript.is_invisible = true;
    }
    public void Make_Visiable()
    {
        GM_Invise = true;
        SaveScript.is_invisible = false;
    }

    public void Add_SP()
    {
        SaveScript.points_to_upgrade++;
    }

    public void Spell_is_Unlock()
    {
        SaveScript.spell_was_unlocked = true;
    }
    public void Magic_is_Unlock()
    {
        SaveScript.magic_was_unlocked = true;
    }

    public void Get_Random_Spell()
    {
        StartCoroutine(Wait_For_Key());
        if(Wait_For_Key() != null)
        {
            for (int i = 0; i < Inventory_Canvas.GetComponent<Inventory>().UI_Slots_1_to_8.Length; i++)
            {
                if (selected_key == i+1)
                {
                    int selected_slot = UnityEngine.Random.Range(0, 6);
                  //  Debug.Log(selected_slot);
                    Inventory_Canvas.GetComponent<Inventory>().UI_Slots_1_to_8[i].sprite = Inventory_Canvas.GetComponent<Inventory>().Spell_icons[selected_slot];
                    Inventory_Canvas.GetComponent<Inventory>().spell_slots_assosiations[i] = selected_slot;
                }
            }
        }
        
    }

    public void Get_Random_Magic()
    {
        StartCoroutine(Wait_For_Key());
        if (Wait_For_Key() != null)
        {
            for (int i = 0; i < Inventory_Canvas.GetComponent<Inventory>().UI_Slots_1_to_8.Length; i++)
            {
                if (selected_key == i + 1)
                {
                    int selected_slot = UnityEngine.Random.Range(0, 7);
                   // Debug.Log(selected_slot);
                    Inventory_Canvas.GetComponent<Inventory>().UI_Slots_1_to_8[i].sprite = Inventory_Canvas.GetComponent<Inventory>().Magic_icons[selected_slot];
                    Inventory_Canvas.GetComponent<Inventory>().spell_slots_assosiations[i] = selected_slot += 6;
                }
            }
        }




    }
    IEnumerator Wait_For_Key()
    {
        bool keyPressed = false;
        while (!keyPressed)
        {
            for (int i = 1; i <= 8; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    selected_key = i;
                    //Debug.Log("Клавіша " + i + " була натиснута");
                    keyPressed = true;
                    break;
                }
            }
            yield return null;
        }
    }
}
