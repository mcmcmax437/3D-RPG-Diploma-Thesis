using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public GameObject current_enemy;
    private bool is_outliner_active = false;

    // Start is called before the first frame update
    void Start()
    {
        current_enemy.GetComponent<Outline>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(is_outliner_active == false)
        {
            is_outliner_active = true;
            if(SaveScript.spell_target == current_enemy)
            {
                current_enemy.GetComponent<Outline>().enabled = true;
            }
        }
        if (is_outliner_active == true)
        {           
            if (SaveScript.spell_target != current_enemy)
            {
                current_enemy.GetComponent<Outline>().enabled = false;
                is_outliner_active = false;
            }
        }
    }
}
