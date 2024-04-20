using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_Creation : MonoBehaviour
{

    public int[] sum_of_necessary_ingr;   

    [HideInInspector]
    public int necessary_value_for_creation_spells;  
    [HideInInspector]
    public int curr_value_of_ingr;  

    public Image[] emptySlots;
    public Sprite[] icons;
    public Sprite emptyIcon;

    [HideInInspector]
    public int itemID = 0;
    [HideInInspector]
    private int max;
    [HideInInspector]
    public int value_of_1_ingr; 
    private int maximum_second; 


    void Start()
    {
       // Debug.Log("sum_of_necessary_ingr[0] = " + sum_of_necessary_ingr[0]);


        necessary_value_for_creation_spells = sum_of_necessary_ingr[0];
        max = emptySlots.Length;
        maximum_second = emptySlots.Length;

        Create();   //because there its bugs with creation, only on second time
    }

    public void Create()
    {
        if (necessary_value_for_creation_spells == curr_value_of_ingr)
        {
            for(int i=0; i < max; i++)
            {
                if(emptySlots[i].sprite == emptyIcon)
                {
                    max = i;
                    emptySlots[i].sprite = icons[itemID];
                    emptySlots[i].transform.gameObject.GetComponent<ItemMessage>().objectType = itemID + 26;

                    curr_value_of_ingr = 0;
                    value_of_1_ingr = 0;
                      
                }
            }
            max = emptySlots.Length;
        } 
    }

    public void Cleare(int index)
    {
        for(int i =0; i < maximum_second; i++)
        {
             if(emptySlots[i].sprite = icons[index])
            {
                maximum_second = i;

                emptySlots[i].sprite = emptyIcon;
                emptySlots[i].transform.gameObject.GetComponent<ItemMessage>().objectType = 0;
            }
        }
        maximum_second = emptySlots.Length;
    }



    public void UpdateValues()
    {
        curr_value_of_ingr += value_of_1_ingr;
        necessary_value_for_creation_spells = sum_of_necessary_ingr[itemID];


        //Debug.Log("curr_value_of_ingr = " + curr_value_of_ingr);
       // Debug.Log("Expected = " + necessary_value_for_creation_spells);
    }
} 
