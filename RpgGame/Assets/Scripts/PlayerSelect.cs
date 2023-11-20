using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{

    public GameObject[] characters;
    private int player_index = 0;
    public Text player_name;


  

   /* void Start()
    {

         
      
    }*/


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

        SceneManager.LoadScene(1); //Load Terrain1
    }
}
