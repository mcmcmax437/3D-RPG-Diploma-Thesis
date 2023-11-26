using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{

    public int number_of_pickedUp_items;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.newIcon = number_of_pickedUp_items;
            Inventory.iconUpdated = true;
            Destroy(gameObject);
        }
    }
}
