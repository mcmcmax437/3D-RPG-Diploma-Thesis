using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_Items : MonoBehaviour
{
    public GameObject Spell_Canvas; //theCanvas
    public int item_id;  //objID

    [HideInInspector]
    public Image item_image;
    [HideInInspector]
    public Color32 basic_transperancy_color = new Color32(255, 255, 255, 160);
    [HideInInspector]
    public Color32 selected_transperancy_color = new Color32(255, 255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        item_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Spell_Canvas.GetComponent<Spell_Creation>().value_of_1_ingr == item_id)
        {
            item_image.color = selected_transperancy_color;
        }
        if (Spell_Canvas.GetComponent<Spell_Creation>().value_of_1_ingr == 0)
        {
            item_image.color = basic_transperancy_color;
        }
    }
}
