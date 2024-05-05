using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject D_Characters_container;
    public GameObject inventoryMenu;
    public GameObject bookOpen;
    public GameObject bookClose;
    public GameObject spell_Book;

    //public GameObject Deeds_Page_Canvas;
    public GameObject Stats_Page_Canvas;
    public GameObject Inventory_Page_Canvas;
    //public GameObject Map_Page_Canvas;


    private AudioSource audio_Player;
    public AudioClip openning_book_SFX;

    public AudioClip click1_SFX;
    public AudioClip click2_SFX;

    public AudioClip coin_buy_SFX;
    public AudioClip coin2_buy_SFX;
    public AudioClip coin3_buy_SFX;

    public AudioClip create_SFX;
    public AudioClip pick_UP_SFX;

    //for character cast spell animation 
    private GameObject player_mesh;
    private Animator player_animation;
    private float weight_of_animation_layer = 1.0f;
    private bool change_weight = false;
    private AnimatorStateInfo player_information;
    

    private bool isInventoryOpened = false;

    //amount of items in inventory
    public static int amount_of_redMushrooms = 0;

    public static int amount_of_blueFlowers = 0;
    public static int amount_of_whiteFlowers = 0;
    public static int amount_of_purpleFlowers = 0;
    public static int amount_of_redFlowers = 0;

    public static int amount_of_roots = 0;
    public static int amount_of_leaf = 0;
    public static int amount_of_keySimp = 0;
    public static int amount_of_keyGold = 0;
    public static int amount_of_monsterEye = 0;
     
    public static int amount_of_bluePotion = 0;
    public static int amount_of_greenPotion = 0;
    public static int amount_of_lazurePotion = 0;
    public static int amount_of_redPotion = 0;

    public static int amount_of_bread = 0;
    public static int amount_of_cheese = 0;
    public static int amount_of_meat = 0;

    public static int amount_of_purpleMushroom = 0;
    public static int amount_of_orangeMushroom = 0;
   // public static int amount_of_redMushrooms = 0;

    public static bool player_has_a_common_key = false;
    public static bool player_has_a_gold_key = false;

    public static int gold = 50000;
    public static int diamond = 0;
    //


    public GameObject Spell_Canvas;
    public GameObject Stats_Canvas;

    public Image[] empty_slots;
    public Sprite[] sprite_icons;
    public Sprite empty_icon_exm;

    public GameObject chatBox;
    public GameObject shop; 

    public static int newIcon = 0;
    public static bool iconUpdated = false;
    private int max;

    //object in inventory
    [HideInInspector]
    public string entry_text;      
    public string[] array_of_items;   
    [HideInInspector]
    public int curr_item_id = 0;    
    [HideInInspector]
    public int curr_amount_of_item = 0;     
    private int maximum_second;   
    private int maximum_third;    


    public Image[] UI_Slots_1_to_8;
    public Sprite[] Spell_icons;
    public Sprite[] Magic_icons;
    public KeyCode[] key_buttons;  //maybe will be able to change in the future //keys
    public bool set_key = false;
    public bool set_key2 = false; 
    [HideInInspector]
    public int selected_slot = 0;
    public int[] spell_slots_assosiations;  

    public GameObject[] spells_vfx_particles;       //for Spell Test    
    public AudioClip[] Spell_SFX;           // should be in the same order as spell_vfx_particles


    public Image mana_bar; // to talk to fill amount from mana

    void Start()
    {
        D_Characters_container.SetActive(false);
        Stats_Canvas.GetComponent<Stats_Info>().OnLoadUpdateOnce();

        inventoryMenu.SetActive(false);
        bookOpen.SetActive(false);
        bookClose.SetActive(true);
        spell_Book.SetActive(false);
        audio_Player = GetComponent<AudioSource>();
        Time.timeScale = 1;

        player_mesh = GameObject.FindGameObjectWithTag("Player");
        player_animation = player_mesh.GetComponent<Animator>();

        max = empty_slots.Length;
        maximum_second = array_of_items.Length;
        maximum_third = empty_slots.Length;

        //Temp

        amount_of_blueFlowers = 0;
        amount_of_whiteFlowers = 0;
        amount_of_purpleFlowers = 0;
        amount_of_redFlowers = 0;

        amount_of_roots = 0;
        amount_of_leaf = 0;
        amount_of_keySimp = 0;
        amount_of_keyGold = 0;
        amount_of_monsterEye = 0;

        amount_of_bluePotion = 0;
        amount_of_greenPotion = 0;
        amount_of_lazurePotion = 0;
        amount_of_redPotion = 0;

        amount_of_bread = 0;
        amount_of_cheese = 0;
        amount_of_meat = 0;

        amount_of_purpleMushroom = 0;
        amount_of_orangeMushroom = 0;
        amount_of_redMushrooms = 0;
        //



    }

    void Update()
    {
        player_information = player_animation.GetCurrentAnimatorStateInfo(1); //listen to Animator


        //Debug.Log("iconUpdated = " + iconUpdated);
        if (iconUpdated == true)
        {
            for (int i = 0; i < max; i++)
            {
                if (empty_slots[i].sprite == empty_icon_exm)
                {
                    max = i;
                    empty_slots[i].sprite = sprite_icons[newIcon];
                    empty_slots[i].transform.gameObject.GetComponent<ItemMessage>().objectType = newIcon;
                }
            }
            StartCoroutine(Reset());
        }
        
       //Set spell icons into main bar
        if(set_key == true)                                  
        {   
            for(int i = 0; i < UI_Slots_1_to_8.Length; i++)
            {
                if (Input.GetKeyDown(key_buttons[i]))
                {
                    set_key = false;
                    Debug.Log(selected_slot);
                    UI_Slots_1_to_8[i].sprite = Spell_icons[selected_slot];
                    spell_slots_assosiations[i] = selected_slot;
                    Spell_Canvas.GetComponent<Spell_Creation>().Cleare(selected_slot);
                    
                }
            }
        }

        //Set magic icons into main bar
        if (set_key2 == true)
        {
            for (int i = 0; i < UI_Slots_1_to_8.Length; i++)
            {
                if (Input.GetKeyDown(key_buttons[i]))
                {
                    set_key2 = false;
                    Debug.Log(selected_slot);
                    UI_Slots_1_to_8[i].sprite = Magic_icons[selected_slot];
                    spell_slots_assosiations[i] = selected_slot += 6;                   

                }
            }
        }

        if (Input.anyKey && Time.timeScale == 1)  //listen to keys  (not in Inventory)
        {
            for (int i = 0; i < UI_Slots_1_to_8.Length; i++)
            {
                if (Input.GetKeyDown(key_buttons[i]))
                {
                    if (UI_Slots_1_to_8[i].sprite != empty_icon_exm)
                    {
                        if (SaveScript.mana > 0.01f)
                        {
                            Instantiate(spells_vfx_particles[spell_slots_assosiations[i]], SaveScript.vfx_spawn_point.transform.position, SaveScript.vfx_spawn_point.transform.rotation); //go throug the buttons 1-8, look what number of spells stores and cast this spell
                            audio_Player.clip = Spell_SFX[spell_slots_assosiations[i]];
                            audio_Player.Play();
                            player_animation.SetTrigger("spellAttack"); 
                            player_animation.SetLayerWeight(1, 1);
                            weight_of_animation_layer = 1;
                        }
                        if(spell_slots_assosiations[i] < 6 && SaveScript.mana > 0.1)
                        {
                            UI_Slots_1_to_8[i].sprite = empty_icon_exm;
                        }
                    }
                }
            }
        }

        mana_bar.fillAmount = SaveScript.mana;

        //for player animation magic attack
        if (player_information.IsTag("spell_attack_tag"))
        {
            change_weight = true;
        }
        if(change_weight == true)
        {
            weight_of_animation_layer -= 0.6f * Time.deltaTime;
            player_animation.SetLayerWeight(1, weight_of_animation_layer);
            if(weight_of_animation_layer <= 0)
            {
                change_weight = false;
            }
        }
        //
        
    }

    public void DataOfItemsCheck()     //read/write into   static data
    {
        for (int i = 0; i < maximum_second; i++)
        {
            if (i == curr_item_id)
            {
                maximum_second = i;
                entry_text = array_of_items[i];
                curr_amount_of_item = System.Convert.ToInt32(typeof(Inventory).GetField(entry_text).GetValue(null));
                curr_amount_of_item--;
                typeof(Inventory).GetField(entry_text).SetValue(null, curr_amount_of_item);
                if (curr_amount_of_item == 0)
                {
                    DestroyIcon(i);
                }
            }           
        }
        maximum_second = array_of_items.Length;
    }


    public void DestroyIcon(int IconType) 
    {
        for (int i = 0; i < maximum_third; i++) 
        {
            if (empty_slots[i].sprite == sprite_icons[IconType])
            {
                maximum_third = i;

                empty_slots[i].sprite = sprite_icons[0];
                empty_slots[i].transform.gameObject.GetComponent<ItemMessage>().objectType = 0;
            }
        }
        maximum_third = empty_slots.Length;
    }

    public void OpenInventory()
    {
        SaveScript.spell_target = null;
        Open_Section_Inventory();
        shop.SetActive(false);               
        chatBox.SetActive(false);
        audio_Player.clip = openning_book_SFX;
        audio_Player.Play();

        inventoryMenu.SetActive(true);
        bookOpen.SetActive(true);
        bookClose.SetActive(false);
        Time.timeScale = 0;


    }

    public void CloseInventory()
    {
        
        inventoryMenu.SetActive(false);
        bookOpen.SetActive(false);
        bookClose.SetActive(true);
        D_Characters_container.SetActive(false);
        audio_Player.clip = openning_book_SFX;
        audio_Player.Play();
        Time.timeScale = 1;
    }

    public void OpenSpellBook()
    {
   
        spell_Book.SetActive(true);
    }

    public void CloseSpellBook()
    {
        Spell_Canvas.GetComponent<Spell_Creation>().curr_value_of_ingr = 0;
        Spell_Canvas.GetComponent<Spell_Creation>().value_of_1_ingr = 0; 
        spell_Book.SetActive(false);
        
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0);
        iconUpdated = false;
        max = empty_slots.Length;
    }

    public void Open_Section_Inventory()
    {
        Stats_Page_Canvas.SetActive(false);
        D_Characters_container.SetActive(false);
        Inventory_Page_Canvas.SetActive(true);
    }

    public void Open_Section_Stats()
    {
        Inventory_Page_Canvas.SetActive(false);
        Stats_Page_Canvas.SetActive(true);
        D_Characters_container.SetActive(true);      
    }

    public void Open_Section_Deeds()
    {
      
    }

    public void Open_Section_Map()
    {
        
    }

}
