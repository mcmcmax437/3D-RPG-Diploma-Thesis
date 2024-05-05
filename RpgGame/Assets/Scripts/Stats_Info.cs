using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats_Info : MonoBehaviour
{
    public Text name_lable;
    public Text finance_gold;
    public Text finance_diamond;
    public Text killed;

    public Image strength_bar;
    public Image intelligence_bar;
    public Image stamina_bar;

    // Start is called before the first frame update
    void Start()
    {
        name_lable.text = SaveScript.player_name;
  
    }

    // Update is called once per frame
    void Update()
    {
        finance_diamond.text = Inventory.diamond.ToString();
        finance_gold.text = Inventory.gold.ToString();
        killed.text = SaveScript.killed_enemy.ToString();
        strength_bar.fillAmount = SaveScript.strength_basic;
        intelligence_bar.fillAmount = SaveScript.intelligence_basic;
        stamina_bar.fillAmount = SaveScript.stamina_basic;
    }

    public void OnLoadUpdateOnce()
    {
        finance_diamond.text = Inventory.diamond.ToString();
        finance_gold.text = Inventory.gold.ToString();
        killed.text = SaveScript.killed_enemy.ToString();
        strength_bar.fillAmount = SaveScript.strength_basic;
        intelligence_bar.fillAmount = SaveScript.intelligence_basic;
        stamina_bar.fillAmount = SaveScript.stamina_basic;
    }
}
