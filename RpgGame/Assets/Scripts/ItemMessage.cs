using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemMessage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioSource audio_Player;
    public GameObject textBox;
    public Text message;
    private bool displaying = true;
    private bool overIcon = false;
    private Vector3 screenPoint;

    //public GameObject theCanvas;
    public GameObject Spell_Canvas;

    public Sprite CursorBasic;
    public Sprite CursorHand;
    public Image CursorImage;

    public int objectType = 0; //0 = empty slots in inventory

    public bool left = true;

    public object CursorObject;

    public GameObject Inventory_Canvas;
    public bool spell = false;
    public bool magic = false;

     
    public void OnPointerEnter(PointerEventData eventData)
    {
        overIcon = true;
        if (displaying == true)
        {  
            CursorImage.sprite = CursorHand;
            textBox.SetActive(true);
            overIcon = true;

            if (left == true)
            {
                screenPoint.x = Input.mousePosition.x + Screen.width / 4.5f;
               // screenPoint.x = Input.mousePosition.x;
            }
            if (left == false)
            {
                screenPoint.x = Input.mousePosition.x - Screen.width / 4.5f;
            }
            screenPoint.y = Input.mousePosition.y;
            screenPoint.z = 1f;           //z axis displayes incorrect - need to be fixed
            Vector3 necessery_position = new Vector3(screenPoint.x, screenPoint.y, screenPoint.z+4.3f);
             //Debug.Log(screenPoint.z);
              textBox.transform.position = Camera.main.ScreenToWorldPoint(necessery_position);
          //  textBox.position.x = screenPoint.x;
          //  textBox.transform.position.y = screenPoint.y;
           // textBox.transform.position.z = screenPoint.z;

           
            MessageDisplay();
            // Debug.Log(screenPoint);

        }

    } 
    public void OnPointerExit(PointerEventData eventData)
    {
        CursorImage.sprite = CursorBasic;
        overIcon = false;
        textBox.SetActive(false);
    }

    void Start()
    {
        textBox.SetActive(false);
        audio_Player = Inventory_Canvas.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (overIcon == true)
        {
           // Debug.Log(overIcon + "= overIcon");

            if (Input.GetMouseButtonDown(0))
            {
                int randomNumber = UnityEngine.Random.Range(1, 101);           
                if (randomNumber % 2 == 0)
                {
                    audio_Player.clip = Inventory_Canvas.GetComponent<Inventory>().click1_SFX;
                }
                else
                {
                    audio_Player.clip = Inventory_Canvas.GetComponent<Inventory>().click2_SFX;
                }     
                audio_Player.Play();
               
                displaying = false;
                textBox.SetActive(false); 

                if(spell == true)
                {
                    if(objectType != 0)
                    {
                        //  Debug.Log(objectType + "objectType BEFORE");

                        Inventory_Canvas.GetComponent<Inventory>().selected_slot = objectType - 26;
                        Inventory_Canvas.GetComponent<Inventory>().set_key = true;
                        // Debug.Log(Inventory_Canvas.GetComponent<Inventory>().selected_slot + "selected_slot AFTER");
                    }
                }

                if (magic == true)
                {
                    if (objectType != 0)
                    {
                        //  Debug.Log(objectType + "objectType BEFORE");

                        Inventory_Canvas.GetComponent<Inventory>().selected_slot = objectType - 32;
                        Inventory_Canvas.GetComponent<Inventory>().set_key2 = true;
                        // Debug.Log(Inventory_Canvas.GetComponent<Inventory>().selected_slot + "selected_slot AFTER");
                    }
                }
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

            //Message for ITEMS
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

        if (objectType == 6)
        {
            message.text = Inventory.amount_of_roots.ToString() + " - amount of roots";
        }
        if (objectType == 7)
        {
            message.text = Inventory.amount_of_leaf.ToString() + " - amount of leaves";
        }
        if (objectType == 8)
        {
            message.text = Inventory.amount_of_keySimp.ToString() + " - amount of simple Keys";
        }
        if (objectType == 9)
        {
            message.text = Inventory.amount_of_keyGold.ToString() + " - amount of golden Keys";
        }
        if (objectType == 10)
        {
            message.text = Inventory.amount_of_monsterEye.ToString() + " - amount of monster eyes";
        }

        if (objectType == 11)
        { 
            message.text = Inventory.amount_of_bluePotion.ToString() + " - amount of blue potion";
        }
        if (objectType == 12)
        {
            message.text = Inventory.amount_of_greenPotion.ToString() + " - amount of green Potion";
        }
        if (objectType == 13)
        {
            message.text = Inventory.amount_of_lazurePotion.ToString() + " - amount of lazure Potion";
        }
        if (objectType == 14)
        {
            message.text = Inventory.amount_of_redPotion.ToString() + " - amount of red potion";
        }

        if (objectType == 15)
        {
            message.text = Inventory.amount_of_bread.ToString() + " - bread for health";
        }
        if (objectType == 16)
        {
            message.text = Inventory.amount_of_cheese.ToString() + " - cheese for health";
        }
        if (objectType == 17)
        {
            message.text = Inventory.amount_of_meat.ToString() + " - meat for health";
        }

        if (objectType == 18)
        {
            message.text = Inventory.amount_of_purpleMushroom.ToString() + " - amount of purple mushrooms";
        }
        if (objectType == 19)
        {
            message.text = Inventory.amount_of_orangeMushroom.ToString() + " - amount of orange mushrooms";
        }
        /*
         FUTURE EXTRA ITEMS 
        AND
        IT`s
        ID
         */
    




        //Message for SPELLS
        if (objectType == 26)
        {
            message.text = "Double strength until you have mana";
        }
        if (objectType == 27)
        {
            message.text = "Explosive fire attack";
        }
        if (objectType == 28)
        {
            message.text = "Receive less damage until you have mana";
        }
        if (objectType == 29)
        {
            message.text = "Powerfull healt recover";
        }
        if (objectType == 30)
        {
            message.text = "You become invisiable and move faster until you have mana";
        }
        if (objectType == 31)
        {
            message.text = "Strong wind attack";
        }

        //Message for MAGIC

        if (objectType == 32)
        {
            message.text = "Uknown MAGIC 1";
        }
        if (objectType == 33)
        {
            message.text = "Uknown MAGIC 2";
        }
        if (objectType == 34)
        {
            message.text = "Uknown MAGIC 3";
        }
        if (objectType == 35)
        {
            message.text = "Uknown MAGIC 4";
        }
        if (objectType == 36)
        {
            message.text = "Uknown MAGIC 5";
        }
        if (objectType == 37)
        {
            message.text = "Uknown MAGIC 6";
        }
        if (objectType == 38)
        {
            message.text = "Uknown MAGIC 7";
        }
        if (objectType == 39)
        {
            message.text = "Uknown MAGIC 8";
        }
        if (objectType == 40)
        {
            message.text = "Uknown MAGIC 9";
        }


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Spell_Canvas.GetComponent<Spell_Creation>().value_of_1_ingr = objectType;
        Spell_Canvas.GetComponent<Spell_Creation>().UpdateValues();
    }
}
