using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private bool enemy_can_attack = true;
    public float damage_enemy = 0.1f;
    private WaitForSeconds wait_before_attack = new WaitForSeconds(1);

    private float correct_dmg_reduce_by_Skill;
    private float correct_dmg_reduce_by_armor;

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            float dmg_check;
            if (enemy_can_attack == true && SaveScript.is_Immmortal_object != true)
            {
                correct_dmg_reduce_by_armor = 1.0f - SaveScript.armora_decrease;

                if (SaveScript.is_shielf_active == true)
                {
                    enemy_can_attack = false;

                    correct_dmg_reduce_by_Skill = 1.0f - SaveScript.damage_reduce_by_Guardianship;
                    
                    SaveScript.health -= (damage_enemy * correct_dmg_reduce_by_armor * correct_dmg_reduce_by_Skill);

                  //  dmg_check = (damage_enemy * correct_dmg_reduce_by_armor * correct_dmg_reduce_by_Skill);
                }
                else
                {         
                    enemy_can_attack = false;
                    SaveScript.health -= damage_enemy * correct_dmg_reduce_by_armor;

                   // dmg_check = damage_enemy * correct_dmg_reduce_by_armor;
                }
                SaveScript.time_of_last_damage_recive = Time.time;
               // Debug.Log(dmg_check);
                StartCoroutine(DMG_Delay_Restart());
            }
        } 
    }

    IEnumerator DMG_Delay_Restart()
    {
        yield return wait_before_attack;
        enemy_can_attack = true;
    } 
}
