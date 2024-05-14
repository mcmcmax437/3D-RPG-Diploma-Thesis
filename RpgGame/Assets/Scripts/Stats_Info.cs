using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats_Info : MonoBehaviour
{
    public Text name_lable;
    public Text finance_gold;
    public Text finance_diamond;
    public Text killed;

    public Image strength_bar;
    public Image intelligence_bar;
    public Image stamina_bar;

    public GameObject[] bought_weapons_inventory;
    public GameObject Inventory_Canvas;
    public bool should_be_updated_weapons = true;
    public GameObject[] armor_in_shop;

    public Text character_LVL;
    public GameObject plus_button;
    public Text scale_points_amt;


    // Start is called before the first frame update
    void Start()
    {
        character_LVL.text = SaveScript.player_lvl_Display.ToString();
        name_lable.text = SaveScript.player_name;
        scale_points_amt.text = SaveScript.points_to_upgrade.ToString();
        
        if (SaveScript.player_index_character == 1 || SaveScript.player_index_character == 2 || SaveScript.player_index_character == 0)
        {
            armor_in_shop[0].SetActive(true);           
        }
        else
        {
            armor_in_shop[1].SetActive(true);            
        }


    }

    // Update is called once per frame
    void Update()
    {
        finance_diamond.text = Inventory.diamond.ToString();
        finance_gold.text = Inventory.gold.ToString();
        killed.text = SaveScript.killed_enemy.ToString();
        strength_bar.fillAmount = SaveScript.strength_basic;
        intelligence_bar.fillAmount = SaveScript.intelligence_basic;
        stamina_bar.fillAmount = SaveScript.stamina_basic;

        scale_points_amt.text = SaveScript.points_to_upgrade.ToString();
        character_LVL.text = SaveScript.player_lvl_Display.ToString();

        if (should_be_updated_weapons == true)
        {
            for (int i =0; i < bought_weapons_inventory.Length; i++)
            {
                if(Inventory_Canvas.GetComponent<Inventory>().weapons[i] == true)
                {
                    bought_weapons_inventory[i].SetActive(true);
                }
            }
        }
        if (this.isActiveAndEnabled)
        {
            should_be_updated_weapons = false;
        }

        
        

    }

    public void OnLoadUpdateOnce()
    {
        finance_diamond.text = Inventory.diamond.ToString();
        finance_gold.text = Inventory.gold.ToString();
        killed.text = SaveScript.killed_enemy.ToString();
        strength_bar.fillAmount = SaveScript.strength_basic;
        intelligence_bar.fillAmount = SaveScript.intelligence_basic;
        stamina_bar.fillAmount = SaveScript.stamina_basic;
    }
}
