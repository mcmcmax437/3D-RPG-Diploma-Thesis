using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock_Book_Magic : MonoBehaviour
{
    public GameObject UI_Magic;
    public GameObject UI_Spells;

    public bool is_magic_Unlock = false;
    public bool is_spells_Unlock = false;

    public bool is_pintogram_Magic = false;
    public bool is_book_Spels = false;

    public GameObject pintogram;
    public GameObject spell_book;
 


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
      
        if(SaveScript.spell_was_unlocked == true && is_book_Spels == true)
        {
            is_spells_Unlock = true;
            spell_book.SetActive(false);
        }
        if (SaveScript.magic_was_unlocked == true && is_pintogram_Magic == true)
        {
            is_magic_Unlock = true;
            pintogram.SetActive(false);
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
                    SaveScript.magic_was_unlocked = true;
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
                    SaveScript.spell_was_unlocked = true;

                }
            }
        }
    }
}