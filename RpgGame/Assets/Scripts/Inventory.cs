using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject inventoryMenu;
    public GameObject bookOpen;
    public GameObject bookClose;
    public GameObject spell_Book;

    private AudioSource audio_Player;
    public AudioClip openning_book_SFX;

    public AudioClip click1_SFX;
    public AudioClip click2_SFX;

    public AudioClip coin_buy_SFX;
    public AudioClip coin2_buy_SFX;
    public AudioClip coin3_buy_SFX;

    public AudioClip create_SFX;
    public AudioClip pick_UP_SFX;

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




    void Start()
    {
        inventoryMenu.SetActive(false);
        bookOpen.SetActive(false);
        bookClose.SetActive(true);
        spell_Book.SetActive(false);
        audio_Player = GetComponent<AudioSource>();
        Time.timeScale = 1;

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
        
       
        if(set_key == true)                                  
        {   
            for(int i = 0; i < UI_Slots_1_to_8.Length; i++)
            {
                if (Input.GetKeyDown(key_buttons[i]))
                {
                    set_key = false;
                    // Debug.Log(selected_slot);
                    UI_Slots_1_to_8[i].sprite = Spell_icons[selected_slot];
                    spell_slots_assosiations[i] = selected_slot;
                    Spell_Canvas.GetComponent<Spell_Creation>().Cleare(selected_slot);
                    
                }
            }
        }

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
    }

    public void DataOfItemsCheck()     //reda/write into   static data
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

}
