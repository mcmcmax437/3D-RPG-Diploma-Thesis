using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    private Animator animator;
    public int goldInChest;
    private static bool chest_is_opened = false;

    public bool commonChest = false;
    public bool legendaryChest = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {

            //For Common Chest
            if (Inventory.player_has_a_common_key == true && commonChest == true && chest_is_opened == false)
            {
                animator.SetTrigger("openChest");
                Inventory.gold = Inventory.gold + goldInChest;
                goldInChest = 0;
                chest_is_opened = true;
                Inventory.amount_of_keySimp--;
                if (Inventory.amount_of_keySimp == 0)
                {
                    ItemPickUp.is_keySimp_exist = false;
                    // ItemPickUp.DestroyIcon();

                }
                Debug.Log("Gold = " + Inventory.gold);
            }
            else
            {
                Debug.Log("You Don`t have a common Key");
            }


            //For Legendary Chest
            if (Inventory.player_has_a_gold_key == true && legendaryChest == true && chest_is_opened == false)
            {
                animator.SetTrigger("openChest");
                Inventory.gold = Inventory.gold + goldInChest;
                goldInChest = 0;
                chest_is_opened = true;
                Inventory.amount_of_keyGold--;
                if(Inventory.amount_of_keyGold == 0)
                {
                    ItemPickUp.is_keyGold_exist = false;  
                   // ItemPickUp.DestroyIcon();
                  
                }
                Debug.Log("Gold = " + Inventory.gold);
            }
            else
            {
                Debug.Log("You Don`t have a golden Key");
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Inventory.player_has_a_common_key == true && commonChest == true && chest_is_opened == true)
            {
                animator.SetTrigger("closeChest");
            }else if (Inventory.player_has_a_gold_key == true && legendaryChest == true && chest_is_opened == true)
            {
                animator.SetTrigger("closeChest");
            }
               
            
        }
    }

    public void DestroyChest()
    {
        Destroy(gameObject);
        chest_is_opened = false;
    }
}
