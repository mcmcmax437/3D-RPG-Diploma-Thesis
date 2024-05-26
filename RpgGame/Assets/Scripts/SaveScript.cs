using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    public static bool class_Avarage = true; // 0
    public static bool class_Mage = false; // 1
    public static bool class_Seller = false; // 2
    public static bool class_Warrior = false; // 3

    public static int uniqe_features_index = 0;
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

    private int enemies_to_lvl_UP = 5;
    public static int killed_enemy = 0;
    public static int points_to_upgrade;

    public static int weapon_index = -1; 
    public static bool should_change_weapon = false; //true - to test, false at finish
    public static bool is_character_equip_a_weapon = false;

    public static int index_of_equiped_armor = 0;  //7 - bassic, 8 - light, 9 - heave  //1 - light 2 - heavy
    public static bool should_change_armor = false;
    public static float critical_hit_chance = 0.2f;
    public static int critical_dmg_multiply = 2;

    public static int player_gold;
    public static int player_diamond;
     
    public static int weapon_dmg_scaleUP;
    public static float armora_decrease = 0.0f;

    public static int amount_of_chasing_enemies;

    public static bool spell_was_unlocked = false;
    public static bool magic_was_unlocked = false;

    private GameObject Inventory_Canvas;
    public static bool take_data_to_load = false; //continueData
    public static bool should_be_saved = false; //saving
    private bool is_loading = false; //checkForLoad



    //for JSON SAVE

    public bool class_Avarage_SAVE; 
    public bool class_Mage_SAVE; 
    public bool class_Seller_SAVE; 
    public bool class_Warrior_SAVE; 

    public int uniqe_features_index_SAVE;
    public float time_of_uniqe_feature_activasion_SAVE;
    public float uniqe_features_index_CD_SAVE;


    public float player_lvl_character_SAVE;
    public int player_lvl_Display_SAVE;
    public int player_index_character_SAVE;
    public string player_name_SAVE;

    public float mana_SAVE;
    public float mana_regeneration_SAVE;
    public float stamina_SAVE;
    public float stamina_regeneration_SAVE;
    public float health_SAVE;
    public float health_regeneration_skill_SAVE;
    public float health_regeneration_passive_SAVE;

    public float time_of_last_damage_recive_SAVE;

    public bool is_Immmortal_object_SAVE;

    public bool is_invisible_SAVE;
    public bool is_shielf_active_SAVE;

    public float damage_reduce_by_Guardianship_SAVE;
    public int strength_increase_SAVE;

    public float strength_basic_SAVE;
    public float intelligence_basic_SAVE;
    public float stamina_basic_SAVE;

    public int enemies_to_lvl_UP_SAVE;
    public int killed_enemy_SAVE;
    public int points_to_upgrade_SAVE;

    public int weapon_index_SAVE;
    public bool should_change_weapon_SAVE;
    public bool is_character_equip_a_weapon_SAVE;

    public int index_of_equiped_armor_SAVE;  
    public bool should_change_armor_SAVE;
    public float critical_hit_chance_SAVE;
    public int critical_dmg_multiply_SAVE;

    public int player_gold_SAVE;
    public int player_diamond_SAVE;

    public int weapon_dmg_scaleUP_SAVE;
    public float armora_decrease_SAVE;

    public int amount_of_chasing_enemies_SAVE;

    public bool spell_was_unlocked_SAVE;
    public bool magic_was_unlocked_SAVE;

    public int amount_of_redMushrooms_SAVE;
    public int amount_of_blueFlowers_SAVE;
    public int amount_of_whiteFlowers_SAVE;
    public int amount_of_purpleFlowers_SAVE;
    public int amount_of_redFlowers_SAVE;
    public int amount_of_roots_SAVE;
    public int amount_of_leaf_SAVE;
    public int amount_of_keySimp_SAVE;
    public int amount_of_keyGold_SAVE;
    public int amount_of_monsterEye_SAVE;
    public int amount_of_bluePotion_SAVE;
    public int amount_of_greenPotion_SAVE;
    public int amount_of_lazurePotion_SAVE;
    public int amount_of_redPotion_SAVE;
    public int amount_of_bread_SAVE;
    public int amount_of_cheese_SAVE;
    public int amount_of_meat_SAVE;
    public int amount_of_purpleMushroom_SAVE;
    public int amount_of_orangeMushroom_SAVE;

    public bool[] weapons_SAVE = new bool[6];

    public int[] obj_index_SAVE = new int[20];



    void Start()
    {
        Debug.Log("Gold update");
        DontDestroyOnLoad(this);

        if(take_data_to_load == true)
        { 
            StreamReader_Data();

            player_index_character = player_index_character_SAVE;
            take_data_to_load = false;

            is_loading = true;
        }


    }

    void Update()
    {
        Mana();
        Stamina();

        float newTime = Time.time - time_of_last_damage_recive;
        if (health < 0.7  && newTime >= 10f)
        {
            Health_Regeneration();
        }
        //Debug.Log(weapon_index);
        if (killed_enemy == enemies_to_lvl_UP)
        {
            Character_Lvl_Up();
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
        if(amount_of_chasing_enemies < 0)
        {
            amount_of_chasing_enemies = 0;
        }


        if(should_be_saved == true)
        {
            should_be_saved = false;
            Write_DATA();
        }

        if(is_loading == true)
        {
            int scene_index = SceneManager.GetActiveScene().buildIndex; 
            if(scene_index == 2)
            {
                if (Inventory_Canvas == null)
                {
                    Inventory_Canvas = GameObject.Find("Inventory");
                }
                if (Inventory_Canvas != null)
                {
                    Read_Data();
                }
            }
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

    public void Mana()
    {
        if (mana < 1.0)
        {
            if (class_Mage == true)
            {

                mana += ((intelligence_basic / 10 + mana_regeneration) * 1.2f) * Time.deltaTime;
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
        if (mana < 0.03)
        {
            is_invisible = false;
            is_shielf_active = false;
            strength_increase = 0;
        }
    }

    public void Stamina()
    {
        if (stamina < 1.0)
        {
            stamina += (stamina_basic / 10 + stamina_regeneration) * Time.deltaTime;
            // Debug.Log(stamina); 
        }
        if (stamina < 0)
        {
            stamina = 0;

            // StartCoroutine(WaitBeforeStaminaRegeneration());
        }
    }
    public void Weapon_DMG_LVL_UP()
    {
        weapon_dmg_scaleUP = System.Convert.ToInt32(strength_basic * 100);
    }

    public void Character_Lvl_Up()
    {
        player_lvl_character += 0.05f;
        enemies_to_lvl_UP = killed_enemy + 8;
        points_to_upgrade++;
        player_lvl_Display++;
    }

    public void Read_Data()
    {
        class_Avarage = class_Avarage_SAVE;
        class_Mage = class_Mage_SAVE;
        class_Seller = class_Seller_SAVE;
        class_Warrior = class_Warrior_SAVE;

        uniqe_features_index = uniqe_features_index_SAVE;
        time_of_uniqe_feature_activasion = time_of_uniqe_feature_activasion_SAVE;
        uniqe_features_index_CD = uniqe_features_index_CD_SAVE;


        player_lvl_character = player_lvl_character_SAVE;
        player_lvl_Display = player_lvl_Display_SAVE;
        player_index_character = player_index_character_SAVE;
        player_name = player_name_SAVE;

        mana = mana_SAVE;
        mana_regeneration = mana_regeneration_SAVE;
        stamina = stamina_SAVE;
        stamina_regeneration = stamina_regeneration_SAVE;
        health = health_SAVE;
        health_regeneration_skill = health_regeneration_skill_SAVE;
        health_regeneration_passive = health_regeneration_passive_SAVE;

        time_of_last_damage_recive = time_of_last_damage_recive_SAVE;
        is_Immmortal_object = is_Immmortal_object_SAVE;

        is_invisible = is_invisible_SAVE;
        is_shielf_active = is_shielf_active_SAVE;

        damage_reduce_by_Guardianship = damage_reduce_by_Guardianship_SAVE;
        strength_increase = strength_increase_SAVE;

        strength_basic = strength_basic_SAVE;
        intelligence_basic = intelligence_basic_SAVE;
        stamina_basic = stamina_basic_SAVE;

        enemies_to_lvl_UP = enemies_to_lvl_UP_SAVE;
        killed_enemy = killed_enemy_SAVE;
        points_to_upgrade = points_to_upgrade_SAVE;

        weapon_index = weapon_index_SAVE;
        should_change_weapon = should_change_weapon_SAVE;
        is_character_equip_a_weapon = is_character_equip_a_weapon_SAVE;
        if (is_character_equip_a_weapon == true)
        {
            should_change_weapon = true;
        }
        index_of_equiped_armor = index_of_equiped_armor_SAVE;
        if (index_of_equiped_armor == 1 || index_of_equiped_armor == 2)
        {
            should_change_armor = true;
        }
        else
        {
            should_change_armor = should_change_armor_SAVE;
        }
        critical_hit_chance = critical_hit_chance_SAVE;
        critical_dmg_multiply = critical_dmg_multiply_SAVE;

        player_gold = player_gold_SAVE;
        player_diamond = player_diamond_SAVE;

        if (player_gold < 200)
        {
            player_gold = 256;
        }
        if (player_diamond < 10)
        {
            player_diamond = 12;
        }

        weapon_dmg_scaleUP = weapon_dmg_scaleUP_SAVE;
        armora_decrease = armora_decrease_SAVE;

        amount_of_chasing_enemies = amount_of_chasing_enemies_SAVE;

        spell_was_unlocked = spell_was_unlocked_SAVE;
        magic_was_unlocked = magic_was_unlocked_SAVE;

        Inventory.amount_of_redMushrooms = amount_of_redMushrooms_SAVE;
        Inventory.amount_of_blueFlowers = amount_of_blueFlowers_SAVE;
        Inventory.amount_of_whiteFlowers = amount_of_whiteFlowers_SAVE;
        Inventory.amount_of_purpleFlowers = amount_of_purpleFlowers_SAVE;
        Inventory.amount_of_redFlowers = amount_of_redFlowers_SAVE;
        Inventory.amount_of_roots = amount_of_roots_SAVE;
        Inventory.amount_of_leaf = amount_of_leaf_SAVE;
        Inventory.amount_of_keySimp = amount_of_keySimp_SAVE;
        Inventory.amount_of_keyGold = amount_of_keyGold_SAVE;
        Inventory.amount_of_monsterEye = amount_of_monsterEye_SAVE;
        Inventory.amount_of_bluePotion = amount_of_bluePotion_SAVE;
        Inventory.amount_of_greenPotion = amount_of_greenPotion_SAVE;
        Inventory.amount_of_lazurePotion = amount_of_lazurePotion_SAVE;
        Inventory.amount_of_redPotion = amount_of_redPotion_SAVE;
        Inventory.amount_of_bread = amount_of_bread_SAVE;
        Inventory.amount_of_cheese = amount_of_cheese_SAVE;
        Inventory.amount_of_meat = amount_of_meat_SAVE;
        Inventory.amount_of_purpleMushroom = amount_of_purpleMushroom_SAVE; 
        Inventory.amount_of_orangeMushroom = amount_of_orangeMushroom_SAVE;

        Inventory_Canvas.GetComponent<Inventory>().weapons = weapons_SAVE;


        for (int i = 0; i < 20; i++)
        {
            Inventory_Canvas.GetComponent<Inventory>().empty_slots[i].sprite = Inventory_Canvas.GetComponent<Inventory>().sprite_icons[obj_index_SAVE[i]];
            Inventory_Canvas.GetComponent<Inventory>().empty_slots[i].transform.gameObject.GetComponent<ItemMessage>().objectType = obj_index_SAVE[i];
        }
        is_loading = false;
    }

    public void Write_DATA()
    {
        if (Inventory_Canvas == null)
        {
            Inventory_Canvas = GameObject.Find("Inventory");
        }
        class_Avarage_SAVE = class_Avarage;
        class_Mage_SAVE = class_Mage;
        class_Seller_SAVE = class_Seller;
        class_Warrior_SAVE = class_Warrior;

        uniqe_features_index_SAVE = uniqe_features_index;
        time_of_uniqe_feature_activasion_SAVE = time_of_uniqe_feature_activasion;
        uniqe_features_index_CD_SAVE = uniqe_features_index_CD;


        player_lvl_character_SAVE = player_lvl_character;
        player_lvl_Display_SAVE = player_lvl_Display;
        player_index_character_SAVE = player_index_character;
        player_name_SAVE = player_name;

        mana_SAVE = mana;
        mana_regeneration_SAVE = mana_regeneration;
        stamina_SAVE = stamina;
        stamina_regeneration_SAVE = stamina_regeneration;
        health_SAVE = health;
        health_regeneration_skill_SAVE = health_regeneration_skill;
        health_regeneration_passive_SAVE = health_regeneration_passive;

        time_of_last_damage_recive_SAVE = time_of_last_damage_recive;
        is_Immmortal_object_SAVE = is_Immmortal_object;

        is_invisible_SAVE = is_invisible;
        is_shielf_active_SAVE = is_shielf_active;

        damage_reduce_by_Guardianship_SAVE = damage_reduce_by_Guardianship;
        strength_increase_SAVE = strength_increase;

        strength_basic_SAVE = strength_basic;
        intelligence_basic_SAVE = intelligence_basic;
        stamina_basic_SAVE = stamina_basic;

        enemies_to_lvl_UP_SAVE = enemies_to_lvl_UP;
        killed_enemy_SAVE = killed_enemy;
        points_to_upgrade_SAVE = points_to_upgrade;

        weapon_index_SAVE = weapon_index;
        should_change_weapon_SAVE = should_change_weapon;
        is_character_equip_a_weapon_SAVE = is_character_equip_a_weapon;

        index_of_equiped_armor_SAVE = index_of_equiped_armor;
        should_change_armor_SAVE = should_change_armor;
        critical_hit_chance_SAVE = critical_hit_chance;
        critical_dmg_multiply_SAVE = critical_dmg_multiply;

        player_gold_SAVE = player_gold;
        player_diamond_SAVE = player_diamond;

        weapon_dmg_scaleUP_SAVE = weapon_dmg_scaleUP;
        armora_decrease_SAVE = armora_decrease;

        amount_of_chasing_enemies_SAVE = amount_of_chasing_enemies;

        spell_was_unlocked_SAVE = spell_was_unlocked;
        magic_was_unlocked_SAVE = magic_was_unlocked;

        amount_of_redMushrooms_SAVE = Inventory.amount_of_redMushrooms;
        amount_of_blueFlowers_SAVE = Inventory.amount_of_blueFlowers;
        amount_of_whiteFlowers_SAVE = Inventory.amount_of_whiteFlowers;
        amount_of_purpleFlowers_SAVE = Inventory.amount_of_purpleFlowers;
        amount_of_redFlowers_SAVE = Inventory.amount_of_redFlowers;
        amount_of_roots_SAVE = Inventory.amount_of_roots;
        amount_of_leaf_SAVE = Inventory.amount_of_leaf;
        amount_of_keySimp_SAVE = Inventory.amount_of_keySimp;
        amount_of_keyGold_SAVE = Inventory.amount_of_keyGold;
        amount_of_monsterEye_SAVE = Inventory.amount_of_monsterEye;
        amount_of_bluePotion_SAVE = Inventory.amount_of_bluePotion;
        amount_of_greenPotion_SAVE = Inventory.amount_of_greenPotion;
        amount_of_lazurePotion_SAVE = Inventory.amount_of_lazurePotion;
        amount_of_redPotion_SAVE = Inventory.amount_of_redPotion;
        amount_of_bread_SAVE = Inventory.amount_of_bread;
        amount_of_cheese_SAVE = Inventory.amount_of_cheese;
        amount_of_meat_SAVE = Inventory.amount_of_meat;
        amount_of_purpleMushroom_SAVE = Inventory.amount_of_purpleMushroom;
        amount_of_orangeMushroom_SAVE = Inventory.amount_of_orangeMushroom;


        weapons_SAVE = Inventory_Canvas.GetComponent<Inventory>().weapons;

        for (int i = 0; i < 20; i++)
        {
            obj_index_SAVE[i] = Inventory_Canvas.GetComponent<Inventory>().empty_slots[i].transform.gameObject.GetComponent<ItemMessage>().objectType;
        }
        StreamWriter_Data();
    }

    void StreamWriter_Data()
    {
        string json_data = JsonUtility.ToJson(this);
        string path = Application.persistentDataPath + "/preservation.dat";
        StreamWriter stream = new StreamWriter(path);
        stream.WriteLine(json_data);
        stream.Close();
    }
    void StreamReader_Data()
    {
        string file_path = Application.persistentDataPath + "/preservation.dat";
        StreamReader read = new StreamReader(file_path);
        string data = read.ReadToEnd();
        read.Close();
        JsonUtility.FromJsonOverwrite(data, this);
    }
}
 