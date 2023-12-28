using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Buying : MonoBehaviour
{

    public GameObject shop;

    public int[] amount_of_stuff_in_shop;
    public int[] cost_of_stuff_in_shop;
    public int[] element_number;

    public int[] inventory_items;

    public Text[] text_amount_of_stuff_in_shop;
    public Text[] text_finance;

    private Text compare;

    public bool isPub;
    public bool isWizzardShop; 
    public bool isCraftsmenWorkshop;

    private int max = 0;
    private bool canClick;

    void Start()
    {
        shop.SetActive(false);
        max = text_amount_of_stuff_in_shop.Length;
        text_finance[0].text = Inventory.gold.ToString();
        text_finance[1].text = Inventory.diamond.ToString();

        for(int i=0; i < max; i++)
        {
            text_amount_of_stuff_in_shop[i].text = amount_of_stuff_in_shop[i].ToString();
        }
    }

     
    public void Close()
    {
        shop.SetActive(false);
        PlayerMovement.canMove = true;
    }

    public void BuyButton()
    {
        if (canClick == true)
        {
            for (int i = 0; i < max; i++)
            {
                if (text_amount_of_stuff_in_shop[i] == compare)
                {
                    max = i;
                    if (amount_of_stuff_in_shop[i] > 0)
                    {
                        
                        if (isPub == true)
                        {
                            RefreshShopAmount();
                        }else if(isWizzardShop == true)
                        {
                            RefreshWizardShopAmount();
                        }
                        else if(isCraftsmenWorkshop == true)
                        {
                            RefreshCraftsMenShopAmount();
                        }

                        if (Inventory.gold >= cost_of_stuff_in_shop[i])
                        {
                            if (inventory_items[i] == 0)
                            {
                                Inventory.newIcon = element_number[i];
                                Inventory.iconUpdated = true;
                            }
                            Inventory.gold -= cost_of_stuff_in_shop[i];
                            if (isPub == true)
                            {
                                SetShopAmount(i);
                            }
                            else if (isWizzardShop == true)
                            {
                                SetWizzardShopAmount(i);
                            }
                            else if (isCraftsmenWorkshop == true)
                            {
                                SetCraftsMenShopAmount(i);
                            }
                        }
                    }
                }
            }
        }
    }

    void RefreshShopAmount()
    {
        inventory_items[0] = Inventory.amount_of_bread;
        inventory_items[1] = Inventory.amount_of_cheese;
        inventory_items[2] = Inventory.amount_of_meat;
    }
    void RefreshWizardShopAmount()
    {
        inventory_items[0] = Inventory.amount_of_redPotion;
        inventory_items[1] = Inventory.amount_of_bluePotion;
        inventory_items[2] = Inventory.amount_of_lazurePotion;
        inventory_items[3] = Inventory.amount_of_greenPotion;

        inventory_items[4] = Inventory.amount_of_monsterEye;
        inventory_items[5] = Inventory.amount_of_roots;
        inventory_items[6] = Inventory.amount_of_leaf;
       
    }
    void RefreshCraftsMenShopAmount()
    {

    }

    public void UpdateFinance()
    {
        text_finance[0].text = Inventory.gold.ToString();
        text_finance[1].text = Inventory.diamond.ToString();
    }

    void SetShopAmount(int item)
    {
        
        switch (item)
        {
            case 0:
                Inventory.amount_of_bread++;
                break;
            case 1:
                Inventory.amount_of_cheese++;
                break;
            case 2:
                Inventory.amount_of_meat++;
                break;

            default:
                break;
        }

        amount_of_stuff_in_shop[item]--;
        text_amount_of_stuff_in_shop[item].text = amount_of_stuff_in_shop[item].ToString();
        UpdateFinance();
        max = amount_of_stuff_in_shop.Length;
    }
    void SetWizzardShopAmount(int item)
    {
        switch (item)
        {
            case 0:
                Inventory.amount_of_redPotion++;
                break;
            case 1:
                Inventory.amount_of_bluePotion++;
                break;
            case 2:
                Inventory.amount_of_lazurePotion++;
                break;
            case 3:
                Inventory.amount_of_greenPotion++;
                break;
            case 4:
                Inventory.amount_of_monsterEye++;
                break;
            case 5:
                Inventory.amount_of_roots++;
                break;
            case 6:
                Inventory.amount_of_leaf++;
                break;


            default:
                break;
        }
          
        amount_of_stuff_in_shop[item]--;
        text_amount_of_stuff_in_shop[item].text = amount_of_stuff_in_shop[item].ToString();
        UpdateFinance();
        max = amount_of_stuff_in_shop.Length;
    }
    void SetCraftsMenShopAmount(int item)
    {
        
    }


    void CheckAmount(int items_number_general)
    {
        if (amount_of_stuff_in_shop[items_number_general] > 0)
        {
            canClick = true;
        }
        else
        {
            canClick = false;
        }

    }
    void CheckAmount_for_WizzardShop(int items_number_general_v2)
    {
        if (amount_of_stuff_in_shop[items_number_general_v2] > 0)
        {
            canClick = true;
        }
        else
        {
            canClick = false;
        }

    }

    //for Shop basic
    public void bread()
    {
        compare = text_amount_of_stuff_in_shop[0];
        CheckAmount(0);
    }
    public void cheese()
    {
        compare = text_amount_of_stuff_in_shop[1];
        CheckAmount(1);
    }
    public void meat()
    {
        compare = text_amount_of_stuff_in_shop[2];
        CheckAmount(2);
    }

    //for Wizzard Shop
    public void red_Potion()
    {
        compare = text_amount_of_stuff_in_shop[0];
        CheckAmount_for_WizzardShop(0);
    }
    public void blue_Potion()
    {
        compare = text_amount_of_stuff_in_shop[1];
        CheckAmount_for_WizzardShop(1);
    }
    public void lazure_Potion()
    {
        compare = text_amount_of_stuff_in_shop[2];
        CheckAmount_for_WizzardShop(2);
    }

    public void green_Potion()
    {
        compare = text_amount_of_stuff_in_shop[3];
        CheckAmount_for_WizzardShop(3);
    }
    public void monster_Eye()
    {
        compare = text_amount_of_stuff_in_shop[4];
        CheckAmount_for_WizzardShop(4);
    }
    public void roots()
    {
        compare = text_amount_of_stuff_in_shop[5];
        CheckAmount_for_WizzardShop(5);
    }
    public void leaf()
    {
        compare = text_amount_of_stuff_in_shop[6];
        CheckAmount_for_WizzardShop(6);
    }
}
