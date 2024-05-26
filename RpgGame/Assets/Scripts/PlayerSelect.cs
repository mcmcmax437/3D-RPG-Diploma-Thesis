using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{

    public GameObject[] characters;
    private int player_index = 0;
    public Text player_name;

    private int class_index;

    public Text features_text;
    public GameObject Class_Buttons;
    public GameObject[] class_is_selected;

  

    void Start()
    {
        select_Avarage();

        for (int i =0; i < class_is_selected.Length; i++)
        {
            class_is_selected[i].SetActive(false);

            if (i == class_index)
            {
                class_is_selected[i].SetActive(true);
            }
        }
        Class_Buttons.SetActive(true);

    }

    private void Update()
    {
        for (int i = 0; i < class_is_selected.Length; i++)
        {
            class_is_selected[i].SetActive(false);

            if (i == class_index)
            {
                class_is_selected[i].SetActive(true);
            }
        }




        if(class_index == 0)
        {
            features_text.text = "None";
        }
        else if (class_index == 1)
        {
            features_text.text = "More Mana Regeneration and +20% spell/magic damage";
        }
        else if (class_index == 2)
        {
            features_text.text = "Price in shop is -20% lower";
        }
        else if (class_index == 3)
        {
            features_text.text = "You can survive lethal damage and regain 50% HP (500 sec CD)";
        }

    }


    public void Next()
    {
        if (player_index < characters.Length-1)
        {
            characters[player_index].SetActive(false);
            player_index++;
            characters[player_index].SetActive(true);
        }    
    }

    public void Before()
    {
        if (player_index <= characters.Length - 1 && player_index > 0)
        {
            
            characters[player_index].SetActive(false);
            player_index--;
            characters[player_index].SetActive(true);
        }
    }

    public void Accept()
    {
        SaveScript.player_index_character = player_index;
        SaveScript.player_name = player_name.text;

        Debug.Log(player_index +" INDEX");

        SceneManager.LoadScene(2); //Load Terrain1
    }

    public void select_Avarage()
    {
            SaveScript.class_Avarage = true;
        SaveScript.class_Mage = false;
        SaveScript.class_Seller = false;
        SaveScript.class_Warrior = false;

        SaveScript.uniqe_features_index = 0;
        class_index = 0;

    }
    public void select_Mage()
    {
        SaveScript.class_Avarage = false;
            SaveScript.class_Mage = true;
        SaveScript.class_Seller = false;
        SaveScript.class_Warrior = false;

        SaveScript.uniqe_features_index = 1;
        class_index = 1;
    }
    public void select_Seller()
    {
        SaveScript.class_Avarage = false;
        SaveScript.class_Mage = false;
            SaveScript.class_Seller = true;
        SaveScript.class_Warrior = false;

        SaveScript.uniqe_features_index = 2;
        class_index = 2;
    }
    public void select_Warrior()
    {
        SaveScript.class_Avarage = false;
        SaveScript.class_Mage = false;
        SaveScript.class_Seller = false;
            SaveScript.class_Warrior = true;

        SaveScript.uniqe_features_index = 3;
        class_index = 3;
    }
}
