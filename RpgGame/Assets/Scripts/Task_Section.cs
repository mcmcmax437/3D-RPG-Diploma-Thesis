using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Task_Section : MonoBehaviour
{

    public GameObject Inventory_Canvas;
    public Text[] tasks;
    public bool can_be_Updated = false;



    void Update()
    {
        Assign_New_Task();
    }


    public void Assign_New_Task()
    {
        if (can_be_Updated == true)
        {
            can_be_Updated = false;
            for (int i = 0; i < Inventory_Canvas.GetComponent<Inventory>().tasks_text.Length; i++)
            {
                if (Inventory_Canvas.GetComponent<Inventory>().tasks_text[i].text != "Empty")
                {
                    tasks[i].color = new Color(255, 255, 2355, 255);
                    tasks[i].text = Inventory_Canvas.GetComponent<Inventory>().tasks_text[i].text;
                }
            }
        }
    }
}
