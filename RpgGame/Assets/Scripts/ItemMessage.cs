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


    public bool left = true;

    public object CursorObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        textBox.SetActive(true);
        //screenPoint.x = Input.mousePosition.x + 400;
       // screenPoint.y = Input.mousePosition.y;
        //screenPoint.z = 1f;
       // textBox.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        MessageDisplay();
        Debug.Log(screenPoint);
        
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
        message.text = "Empty";
    } 
}
