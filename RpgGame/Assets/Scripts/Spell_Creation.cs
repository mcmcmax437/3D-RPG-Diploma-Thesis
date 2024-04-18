using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_Creation : MonoBehaviour
{

    public int[] sum_of_necessary_ingr;     //values

    [HideInInspector]
    public int necessary_value_for_creation_spells;  //expectedValue
    [HideInInspector]
    public int curr_value_of_ingr;  //value

    public Image[] emptySlots;
    public Sprite[] icons;
    public Sprite emptyIcon;

    [HideInInspector]
    public int itemID = 0;
    [HideInInspector]
    private int max;
    [HideInInspector]
    public int value_of_1_ingr; //thisValue


    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log("sum_of_necessary_ingr[0] = " + sum_of_necessary_ingr[0]);


        necessary_value_for_creation_spells = sum_of_necessary_ingr[0];
        max = emptySlots.Length;

        Create();   //because there ias bugs with creation, only on second time
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
                    curr_value_of_ingr = 0;
                    value_of_1_ingr = 0;
                      
                }
            }
            max = emptySlots.Length;
        } 
    }

    public void UpdateValues()
    {
        curr_value_of_ingr += value_of_1_ingr;
        necessary_value_for_creation_spells = sum_of_necessary_ingr[itemID];


        Debug.Log("curr_value_of_ingr = " + curr_value_of_ingr);
        Debug.Log("Expected = " + necessary_value_for_creation_spells);
    }
} 
