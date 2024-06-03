using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingTrigger : MonoBehaviour
{
    public GameObject Inventory_Canvas;
    private GameObject compas;
    private GameObject map;
    public GameObject chatbox_trigger;
    public int shop_number = 0;
    public GameObject task;
    public string aim;
    private bool task_was_already_given = false;

    private void Start()
    {
        Inventory_Canvas = GameObject.Find("Inventory");
        map = GameObject.FindGameObjectWithTag("map");
        compas = GameObject.FindGameObjectWithTag("compas");
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chatbox_trigger.SetActive(true);
            TurnOff_MiniMap();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chatbox_trigger.SetActive(false);
            chatbox_trigger.GetComponentInChildren<ChatScript>().shop_number = shop_number;
            Check_If_Task_was_Given();
            if (task_was_already_given == false)
            {
                task.GetComponent<ChatScript>().Pub_task = aim;
                
            }else if(task_was_already_given == true)
            {
                task.GetComponent<ChatScript>().Pub_task = "Relax and do your job!";
            }
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurnOn_MiniMap();
            chatbox_trigger.SetActive(false);
        }
    }

    public void TurnOff_MiniMap()
    {
        map.SetActive(false);
        compas.SetActive(false);
    }

    public void TurnOn_MiniMap()
    {
        map.SetActive(true);
        compas.SetActive(true);
    }

    public void Check_If_Task_was_Given()
    {
        for (int i = 1; i < Inventory_Canvas.GetComponent<Inventory>().tasks_text.Length; i++)
        {
            if (aim == Inventory_Canvas.GetComponent<Inventory>().tasks_text[i].text)
            {
                task_was_already_given = true;
            }
        }
    }
}
