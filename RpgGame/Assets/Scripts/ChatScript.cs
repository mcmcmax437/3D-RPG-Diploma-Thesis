using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


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
                shopUI[shop_number].SetActive(false);
            }
        }
    }
    public void Message1()
    {
        salesman_message.text = "There is nothing special going on here";
    }

    public void Message2()
    {
        salesman_message.text = "Buy everything what you want...";
        shopUI[shop_number].SetActive(true);
        shopUI[shop_number].GetComponent<Buying>().UpdateFinance();

    }
}
