using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingTrigger : MonoBehaviour
{
    private GameObject compas;
    private GameObject map;
    public GameObject chatbox_trigger;
    public int shop_number = 0;
    public GameObject task;
    public string aim;
    private bool task_was_already_given = false;

    private void Start()
    {
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
            if(task_was_already_given == false)
            {
                task.GetComponent<ChatScript>().Pub_task = aim;
                StartCoroutine(Reset_Permition());         
            }else if(task_was_already_given == true)
            {
                task.GetComponent<ChatScript>().Pub_task = "Relax and do your job!";
            }
            
        }
    }
    IEnumerator Reset_Permition()
    {
        yield return new WaitForSeconds(5); //5 sec to take a quest
        task_was_already_given = true;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurnOn_MiniMap();
            chatbox_trigger.SetActive(false);
        }
    }

    void TurnOff_MiniMap()
    {
        map.SetActive(false);
        compas.SetActive(false);
    }

    void TurnOn_MiniMap()
    {
        map.SetActive(true);
        compas.SetActive(true);
    }
}
