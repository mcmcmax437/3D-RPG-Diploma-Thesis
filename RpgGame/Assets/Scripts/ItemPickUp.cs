using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{

    public int number_of_pickedUp_items;

    public bool is_redMushroom = false;
    public bool is_blueFlower = false;
    public bool is_whiteFlower = false;
    public bool is_purpleFlower = false;
    public bool is_redFlower = false;

    public bool is_roots = false;
    public bool is_leaf = false;
    public bool is_keySimp = false;
    public bool is_keyGold = false;
    public bool is_monsterEye = false;

    public bool is_bluePotion = false;
    public bool is_greenPotion = false;
    public bool is_lazurePotion = false;
    public bool is_redPotion = false;

    public bool is_bread = false;
    public bool is_cheese = false;
    public bool is_meat = false;

    public bool is_purpleMushroom = false;
    public bool is_orangeMushroom = false;

    public static bool is_keySimp_exist = false;
    public static bool is_keyGold_exist = false;

    public GameObject Inventory_Canvas;
    public AudioSource audio_Player;


    private void Start()
    {
        Inventory_Canvas = GameObject.Find("Inventory");
        audio_Player = Inventory_Canvas.GetComponent<AudioSource>();

    }




    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player"))
        {
            audio_Player.clip = Inventory_Canvas.GetComponent<Inventory>().pick_UP_SFX;
            audio_Player.Play();
            if (is_redMushroom == true)
            {
                if (Inventory.amount_of_redMushrooms == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_redMushrooms++;
                Destroy(gameObject);
            }
            else if (is_blueFlower == true)
            {
                if (Inventory.amount_of_blueFlowers == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_blueFlowers++;
                Destroy(gameObject);
            }
            else if (is_whiteFlower == true)
            {
                if (Inventory.amount_of_whiteFlowers == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_whiteFlowers++;
                Destroy(gameObject);
            }
            else if (is_purpleFlower == true)
            {
                if (Inventory.amount_of_purpleFlowers == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_purpleFlowers++;
                Destroy(gameObject);
            }
            else if (is_redFlower == true)
            {
                if (Inventory.amount_of_redFlowers == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_redFlowers++;
                Destroy(gameObject);
            }

            else if (is_roots == true)
            {
                if (Inventory.amount_of_roots == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_roots++;
                Destroy(gameObject);
            }
            else if (is_leaf == true)
            {
                if (Inventory.amount_of_leaf == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_leaf++;
                Destroy(gameObject);
            }
            else if (is_keySimp == true)
            {
                if (Inventory.amount_of_keySimp == 0 && is_keySimp_exist == false)
                {
                    DisplayIcons();
                    is_keySimp_exist = true;
                }
                Inventory.amount_of_keySimp++;
                Inventory.player_has_a_common_key = true;
                Destroy(gameObject);
            }
            else if (is_keyGold == true)
            {
                if (Inventory.amount_of_keyGold == 0 && is_keyGold_exist == false)
                {
                    DisplayIcons();
                    is_keyGold_exist = true;
                }
                Inventory.amount_of_keyGold++;
                Inventory.player_has_a_gold_key = true;
                Destroy(gameObject);
            }
            else if (is_monsterEye == true)
            {
                if (Inventory.amount_of_monsterEye == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_monsterEye++;
                Destroy(gameObject);
            }

            else if (is_bluePotion == true)
            {
                if (Inventory.amount_of_bluePotion == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_bluePotion++;
                Destroy(gameObject);
            }
            else if (is_greenPotion == true)
            {
                if (Inventory.amount_of_greenPotion == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_greenPotion++;
                Destroy(gameObject);
            }
            else if (is_lazurePotion == true)
            {
                if (Inventory.amount_of_lazurePotion == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_lazurePotion++;
                Destroy(gameObject);
            }
            else if (is_redPotion == true)
            {
                if (Inventory.amount_of_redPotion == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_redPotion++;
                Destroy(gameObject);
            }

            else if (is_bread == true)
            {
                if (Inventory.amount_of_bread == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_bread++;
                Destroy(gameObject);
            }
            else if (is_cheese == true)
            {
                if (Inventory.amount_of_cheese == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_cheese++;
                Destroy(gameObject);
            }
            else if (is_meat == true)
            {
                if (Inventory.amount_of_meat == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_meat++;
                Destroy(gameObject);
            }

            else if (is_purpleMushroom == true)
            {
                if (Inventory.amount_of_purpleMushroom == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_purpleMushroom++;
                Destroy(gameObject);
            }
            else if (is_orangeMushroom == true)
            {
                if (Inventory.amount_of_orangeMushroom == 0)
                {
                    DisplayIcons();
                }
                Inventory.amount_of_orangeMushroom++;
                Destroy(gameObject);
            }

            else
            {
                DisplayIcons();
                Destroy(gameObject);
            }

           // Destroy(gameObject);
        }
    }

    void DisplayIcons()
    {
        Inventory.newIcon = number_of_pickedUp_items;
        Inventory.iconUpdated = true;
    }



    public static void DestroyIcon()
    {
        Inventory.newIcon = 0;
        Inventory.iconUpdated = true;
    }
}
