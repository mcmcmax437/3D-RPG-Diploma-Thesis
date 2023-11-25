using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{

    public GameObject inventoryMenu;
    public GameObject bookOpen;
    public GameObject bookClose;

    private bool isInventoryOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu.SetActive(false);
        bookOpen.SetActive(false);
        bookClose.SetActive(true);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        

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
}
