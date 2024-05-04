using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock_Book_Magic : MonoBehaviour
{
    public GameObject UI_Magic;
    public GameObject UI_Spells;

    private bool is_magic_Unlock = false;
    private bool is_spells_Unlock = false;

    public bool is_pintogram_Magic = false;
    public bool is_book_Spels = false;

    // Start is called before the first frame update
    void Start()
    {
        if(is_pintogram_Magic == true)
        {
            UI_Magic.SetActive(false);
        }
        if(is_book_Spels == true)
        {
            UI_Spells.SetActive(false);
        }
       
      

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (is_pintogram_Magic == true) 
            {
                if(is_magic_Unlock == false)
                {
                    UI_Magic.SetActive(true);
                    is_magic_Unlock = true;
                    Destroy(gameObject);
                }
            }
        }

        if (other.CompareTag("Player"))
        {
            if (is_book_Spels == true)
            {
                if (is_spells_Unlock == false)
                {
                    UI_Spells.SetActive(true);
                    is_spells_Unlock = true;
                    Destroy(gameObject);

                }
            }
        }
    }
}