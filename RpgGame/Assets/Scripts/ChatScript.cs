using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Inventory_Canvas;
    public string Pub_task;

    public Text buttonText;
    public Color32 buttonText_off;
    public Color32 buttonText_on;

    public Text salesman_message;
    public GameObject[] shopUI;

    [HideInInspector]
    public int shop_number;


    public void OnPointerEnter(PointerEventData eventData)
    {
       buttonText.color = buttonText_on;
        PlayerMovement.canMove = false;                 //can`t move over text button
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = buttonText_off;
        PlayerMovement.canMove = true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        salesman_message.text = "What a pleasure surprise! Hello, " + SaveScript.player_name;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.canMove == true && PlayerMovement.isPlayerMoving == true)
        {
            if (shopUI != null)
            {
                salesman_message.text = "What a pleasure surprise! Hello, " + SaveScript.player_name;
                shopUI[shop_number].SetActive(false);
            }
        }
    }
    public void Message_N1()
    {
        salesman_message.text = Pub_task;
        if(Inventory_Canvas != null)
        {
            if(Pub_task != "Relax and do your job!")
            {
                Inventory_Canvas.GetComponent<Inventory>().Update_text_of_the_task(Pub_task);
            }   
        }
    }

    public void Message_N2()
    {
        salesman_message.text = "Buy everything what you want...";
        shopUI[shop_number].SetActive(true);
        if(shop_number < 3)
        {
            shopUI[shop_number].GetComponent<Buying>().UpdateFinance();
        }       
    }
}
