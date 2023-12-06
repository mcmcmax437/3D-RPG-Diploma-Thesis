using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemMessage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textBox;
    public Text message;
    private bool displaying = true;
    private bool overIcon = false;
    private Vector3 screenPoint;
    public GameObject theCanvas;
    public int objectType = 0; //0 = empty slots in inventory

    public bool left = true;

    public object CursorObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (displaying == true)
        {
            textBox.SetActive(true);
            //screenPoint.x = Input.mousePosition.x + 400;
            // screenPoint.y = Input.mousePosition.y;
            //screenPoint.z = 1f;
            // textBox.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            MessageDisplay();
            Debug.Log(screenPoint);
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
        overIcon = false;
        textBox.SetActive(false);
    }

    void Start()
    {
        textBox.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (overIcon == true)
        {
            if (Input.GetMouseButtonDown(0))
            {             
                displaying = false;
                textBox.SetActive(false);           
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            displaying = true;
        }
    }
    void MessageDisplay()
    {
        if (objectType == 0)
        {
            message.text = "Empty";

        }
        if (objectType == 1)
        {
            message.text = Inventory.amount_of_redMushrooms.ToString() + " - amount of red mushrooms";
        }
        if (objectType == 2)
        {
            message.text = Inventory.amount_of_blueFlowers.ToString() + " - amount of blue flowers";
        }
        if (objectType == 3)
        {
            message.text = Inventory.amount_of_whiteFlowers.ToString() + " - amount of white flowers";
        }
        if (objectType == 4)
        {
            message.text = Inventory.amount_of_purpleFlowers.ToString() + " - amount of purple flowers";
        }
        if (objectType == 5)
        {
            message.text = Inventory.amount_of_redFlowers.ToString() + " - amount of red flowers";
        }
        
    } 
}
