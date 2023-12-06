using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{

    public int number_of_pickedUp_items;

    public bool is_redMush = false;
    public bool is_blueFlower = false;
    public bool is_whiteFlower = false;
    public bool is_purpleFlower = false;
    public bool is_redFlower = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (is_redMush == true)
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
            }
            else
            {
                DisplayIcons();
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }

    void DisplayIcons()
    {
        Inventory.newIcon = number_of_pickedUp_items;
        Inventory.iconUpdated = true;
    }
}
