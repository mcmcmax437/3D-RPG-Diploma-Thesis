using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDchar : MonoBehaviour
{
    public GameObject[] D_Char_array;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < D_Char_array.Length; i++)
        {
            D_Char_array[i].SetActive(false);
        }
        D_Char_array[SaveScript.player_index_character].SetActive(true);
    }

}
  
