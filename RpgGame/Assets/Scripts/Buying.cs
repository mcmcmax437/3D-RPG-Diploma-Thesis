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
                            UpdateShopAmount();
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
                        }
                    }
                }
            }
        }
    }

    void UpdateShopAmount()
    {
        inventory_items[0] = Inventory.amount_of_bread;
        inventory_items[1] = Inventory.amount_of_cheese;
        inventory_items[2] = Inventory.amount_of_meat;
    }

    public void UpdateFinance()
    {
        text_finance[0].text = Inventory.gold.ToString();
        text_finance[1].text = Inventory.diamond.ToString();
    }

    void SetShopAmount(int item)
    {
        if(item == 0)
        {
            Inventory.amount_of_bread++;
        }
        if (item == 1)
        {
            Inventory.amount_of_cheese++;
        }
        if (item == 2)
        {
            Inventory.amount_of_meat++;
        }
        amount_of_stuff_in_shop[item]--;
        text_amount_of_stuff_in_shop[item].text = amount_of_stuff_in_shop[item].ToString();
        UpdateFinance();
        max = amount_of_stuff_in_shop.Length;
    }

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
}
