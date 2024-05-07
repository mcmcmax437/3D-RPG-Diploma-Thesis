using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Buying_Weapons : MonoBehaviour
{
    public Text finance_text_gold;
    public Text finance_text_diamond;

    public int weapon_index;
    public int armor__index;
    public int price;
    public GameObject Inventory_Canvas;
    public AudioSource audio_Player;

    // Start is called before the first frame update
    void Start()
    {
        finance_text_diamond.text = Inventory.diamond.ToString();
        finance_text_gold.text = Inventory.gold.ToString(); 
        audio_Player = Inventory_Canvas.GetComponent<AudioSource>();
    }

    public void BuyButton_Weapon()
    {
        if(Inventory.gold >= price)
        {
            Inventory.gold -= price;
            Inventory_Canvas.GetComponent<Inventory>().weapons[weapon_index] = true;

            //RANDOM SFX COIN
            RandomAudio();
            //
            finance_text_diamond.text = Inventory.diamond.ToString();
            finance_text_gold.text = Inventory.gold.ToString();

        }
    }

    public void BuyButton_Armor()
    {
        if (Inventory.gold >= price)
        {
            Inventory.gold -= price;
            SaveScript.index_of_equiped_armor = armor__index;
            SaveScript.should_change_armor = true;
            RandomAudio();
            finance_text_diamond.text = Inventory.diamond.ToString();
            finance_text_gold.text = Inventory.gold.ToString();
        } 
    }

    public void RandomAudio()
    {
        int randomNumber = UnityEngine.Random.Range(1, 101);
        if (randomNumber > 0 && randomNumber < 33)
        {
            audio_Player.clip = Inventory_Canvas.GetComponent<Inventory>().coin_buy_SFX;
        }
        else if (randomNumber >= 33 && randomNumber < 66)
        {
            audio_Player.clip = Inventory_Canvas.GetComponent<Inventory>().coin2_buy_SFX;
        }
        else if (randomNumber >= 66 && randomNumber < 101)
        {
            audio_Player.clip = Inventory_Canvas.GetComponent<Inventory>().coin3_buy_SFX;
        }
        audio_Player.Play();
    }
}
