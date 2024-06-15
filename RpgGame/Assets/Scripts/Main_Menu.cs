using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public GameObject continue_;
    public GameObject load_;
    public GameObject save_;
    void Start()
    {
        TurnOn_Continue_If_Exists();
        Cursor.visible = true;
    }


    public void Start_New_Game()
    {
        SaveScript.health = 1.0f;
        SaveScript.player_gold = 500;
        SaveScript.player_diamond = 500;
        SaveScript.New_Game_Start = true;
        SceneManager.LoadScene(1);
    }

    public void Continue_Button()
    {
        load_.SetActive(true);
        save_.SetActive(true);
        SaveScript.take_data_to_load = true;
        StartCoroutine(LoadGame());
    }

    public void Exit()
    { 
        Application.Quit();
    }
    public void Settings()
    {
        
    }
    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
    public void TurnOn_Continue_If_Exists()
    {
        if (Application.persistentDataPath + "/preservation.data" != null)
        {
            continue_.SetActive(true);
        }
        else
        {
            continue_.SetActive(false);
        }
    }

}
