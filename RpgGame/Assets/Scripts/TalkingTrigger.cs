using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingTrigger : MonoBehaviour
{
    public GameObject chatbox_trigger;
    public int shop_number = 0;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chatbox_trigger.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chatbox_trigger.SetActive(false);
            chatbox_trigger.GetComponentInChildren<ChatScript>().shop_number = shop_number;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chatbox_trigger.SetActive(false);
        }
    }
}
