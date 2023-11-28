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

    private bool isInventoryOpened = false;
    public static int amount_of_redMushrooms = 0;
    public static int amount_of_blueFlowers = 0;

    public Image[] empty_slots;
    public Sprite[] sprite_icons;
    public Sprite empty_icon_exm;

    public static int newIcon = 0;
    public static bool iconUpdated = false;
    private int max;

    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu.SetActive(false);
        bookOpen.SetActive(false);
        bookClose.SetActive(true);
        Time.timeScale = 1;

        max = empty_slots.Length;

        //Temp
        amount_of_redMushrooms = 0;
        amount_of_blueFlowers = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (iconUpdated == true)
        {
            for (int i = 0; i < max; i++)
            {
                if (empty_slots[i].sprite == empty_icon_exm)
                {
                    max = i;
                    empty_slots[i].sprite = sprite_icons[newIcon];
                }
            }
            StartCoroutine(Reset());
        }

    }

    public void OpenInventory()
    {

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
        Time.timeScale = 1;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0);
        iconUpdated = false;
        max = empty_slots.Length;
    }
}
