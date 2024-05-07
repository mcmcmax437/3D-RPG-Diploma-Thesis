using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Player_ArmorDisplay : MonoBehaviour
{

    public GameObject[] armor_parts_Torso;
    public GameObject[] armor_parts_Legs;

   public void DisplayArmor()
    {
        for (int i = 0; i < armor_parts_Torso.Length; i++)
        {
            armor_parts_Torso[i].SetActive(false);
            armor_parts_Legs[i].SetActive(false);
        }
        armor_parts_Torso[SaveScript.index_of_equiped_armor].SetActive(true);
        armor_parts_Legs[SaveScript.index_of_equiped_armor].SetActive(true);

    }

}
