using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spells_Book : MonoBehaviour
{

    public Image spell_Icon;
    public Text spell_Name;
    public Text spell_Description;

    public Sprite[] spell_UISprites;
    public string[] title;
    public string[] descriptions;
    public GameObject[] iconSets;

    private int curr_Icon = 0;
    public GameObject Spell_Canvas;
    

    // Start is called before the first frame update
    void Start()
    {
        spell_Icon.sprite = spell_UISprites[0];
        spell_Name.text = title[0];
        spell_Description.text = descriptions[0];
        iconSets[0].SetActive(true);

    }

    public void Next()
    {
        if (curr_Icon < spell_UISprites.Length - 1)
        {
            curr_Icon++;
            spell_Icon.sprite = spell_UISprites[curr_Icon];
            spell_Name.text = title[curr_Icon];
            spell_Description.text = descriptions[curr_Icon];
            Close();
            iconSets[curr_Icon].SetActive(true);
            Spell_Canvas.GetComponent<Spell_Creation>().itemID++;
            Spell_Canvas.GetComponent<Spell_Creation>().curr_value_of_ingr = 0;
            Spell_Canvas.GetComponent<Spell_Creation>().value_of_1_ingr = 0;
        }
    }
     
    void Close()
    {
        for (int i = 0; i < iconSets.Length; i++)
        {
            iconSets[i].SetActive(false);
        }
    }

    public void Previous()
    {
        if (curr_Icon > 0)
        {
            curr_Icon--;
            spell_Icon.sprite = spell_UISprites[curr_Icon];
            spell_Name.text = title[curr_Icon];
            spell_Description.text = descriptions[curr_Icon];
            Close();
            iconSets[curr_Icon].SetActive(true);
            Spell_Canvas.GetComponent<Spell_Creation>().itemID--;
            Spell_Canvas.GetComponent<Spell_Creation>().curr_value_of_ingr = 0;
            Spell_Canvas.GetComponent<Spell_Creation>().value_of_1_ingr = 0;

        }
    }
}
