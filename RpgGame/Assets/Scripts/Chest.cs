using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Chest : MonoBehaviour
{

    private Animator animator;
    public int goldInChest;
    private static bool chest_is_opened = false;

    //public GameObject Spell_Canvas;
     
    public bool commonChest = false;
    public bool legendaryChest = false;
    public bool crate = false;

    public GameObject vfx_particle;
    public GameObject spawn_point;
    public GameObject chest_Canvas;     //canvas text access
    public Text gold_amount_inside_text;
    public float text_animation_speed = 1.0f;
    public GameObject Camera;

    // can transfer int into Text ????
    private int display_money;

    public GameObject inventory_Canvas;
    public AudioClip chest_openning_SFX;
    public AudioClip key_twist_SFX;

    //public GameObject Crate_Container;
    //public GameObject Crate_Mesh;


    void Start()
    {
        if(crate == false)
        {
            animator = GetComponent<Animator>();          
        }
        chest_Canvas.SetActive(false);
        display_money = goldInChest;
    }

    private void Update()
    {
        if(chest_Canvas.activeSelf == true)
        {
            chest_Canvas.transform.Translate(Vector3.up * text_animation_speed * Time.deltaTime);
            
            gold_amount_inside_text.text = display_money.ToString();
            chest_Canvas.transform.LookAt(Camera.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(crate == false) 
        {
            if (other.CompareTag("Player"))
            {

                //For Common Chest
                if (Inventory.player_has_a_common_key == true && commonChest == true && chest_is_opened == false)
                {
                    animator.SetTrigger("openChest");
                    Inventory.gold = Inventory.gold + goldInChest;
                    goldInChest = 0;

                    //audio SFX for chest
                    inventory_Canvas.GetComponent<AudioSource>().volume = 0.4f;
                    inventory_Canvas.GetComponent<AudioSource>().clip = key_twist_SFX;     //too low
                    inventory_Canvas.GetComponent<AudioSource>().Play();            // too low

                    inventory_Canvas.GetComponent<AudioSource>().clip = chest_openning_SFX;
                    inventory_Canvas.GetComponent<AudioSource>().PlayDelayed(1 * Time.deltaTime);

                    chest_is_opened = true;
                    Inventory.amount_of_keySimp--;
                    if (Inventory.amount_of_keySimp == 0)
                    {
                        ItemPickUp.is_keySimp_exist = false;
                        // ItemPickUp.DestroyIcon();

                    }
                    Debug.Log("Gold = " + Inventory.gold);

                }
                else
                {
                    Debug.Log("You Don`t have a common Key");
                }


                //For Legendary Chest
                if (Inventory.player_has_a_gold_key == true && legendaryChest == true && chest_is_opened == false)
                {
                    animator.SetTrigger("openChest");
                    Inventory.gold = Inventory.gold + goldInChest;
                    goldInChest = 0;

                    //audio SFX for chest
                    inventory_Canvas.GetComponent<AudioSource>().clip = key_twist_SFX;          //to low
                    inventory_Canvas.GetComponent<AudioSource>().Play();            //to low

                    inventory_Canvas.GetComponent<AudioSource>().clip = chest_openning_SFX;
                    inventory_Canvas.GetComponent<AudioSource>().PlayDelayed(1 * Time.deltaTime);


                    chest_is_opened = true;
                    Inventory.amount_of_keyGold--;
                    if (Inventory.amount_of_keyGold == 0)
                    {
                        ItemPickUp.is_keyGold_exist = false;
                        // ItemPickUp.DestroyIcon(); 

                    }


                    Debug.Log("Gold = " + Inventory.gold);
                }
                else
                {
                    Debug.Log("You Don`t have a golden Key");
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (crate == false)
        {
            if (other.CompareTag("Player"))
            {
                if (Inventory.player_has_a_common_key == true && commonChest == true && chest_is_opened == true)
                {
                    animator.SetTrigger("closeChest");
                }
                else if (Inventory.player_has_a_gold_key == true && legendaryChest == true && chest_is_opened == true)
                {
                    animator.SetTrigger("closeChest");
                }


            }
        }
    }

    public void DestroyChest()
    {       
        Destroy(gameObject);
        chest_is_opened = false;
    }

    public void VFX_chest_text()
    {
        //Debug.Log("Spawn VFX");
        Instantiate(vfx_particle, spawn_point.transform.position, spawn_point.transform.rotation);
        chest_Canvas.SetActive(true);
    }


    public void VFX_crate_text()
    {
        //Debug.Log("Spawn VFX");
        Instantiate(vfx_particle, spawn_point.transform.position, spawn_point.transform.rotation);
        chest_Canvas.SetActive(true);

        if (crate == true)
        {
            Inventory.gold = Inventory.gold + goldInChest;
            goldInChest = 0;
            RandomAudio_Crate();
            chest_is_opened = true;
        }
    }
    public void RandomAudio_Crate()
    {
        int randomNumber = UnityEngine.Random.Range(1, 101);
        if (randomNumber > 0 && randomNumber < 50)
        {
            inventory_Canvas.GetComponent<AudioSource>().volume = 0.4f;
            inventory_Canvas.GetComponent<AudioSource>().clip = key_twist_SFX;    // Crack1_Wood - sfx
            StartCoroutine(Reset_Audio_Volume());


        }
        else if (randomNumber >= 50 && randomNumber <= 101)
        {
            inventory_Canvas.GetComponent<AudioSource>().volume = 0.4f;
            inventory_Canvas.GetComponent<AudioSource>().clip = chest_openning_SFX;   // Crack3_Wood - sfx
            StartCoroutine(Reset_Audio_Volume());
        }
        inventory_Canvas.GetComponent<AudioSource>().Play();
    }


    IEnumerator Reset_Audio_Volume()
    {
        yield return new WaitForSeconds(1);
        inventory_Canvas.GetComponent<AudioSource>().volume = 1.0f;
    }
    /*public void Destroy_CrateMesh()
    {
        Destroy(Crate_Mesh);
    }

    public void Destroy_CrateContainer()
    {
        Destroy(Crate_Container);
    }*/
}
