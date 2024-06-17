using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private AudioSource audio_Player;
    private bool enemy_can_attack = true;
    public float damage_enemy = 0.1f;
    private WaitForSeconds wait_before_attack = new WaitForSeconds(1);

    private float correct_dmg_reduce_by_Skill;
    private float correct_dmg_reduce_by_armor;
    private bool reset = true;


    void Start()
    {
        audio_Player = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
           // Debug.Log("Attack = true");
            float dmg_check;
            if (enemy_can_attack == true && SaveScript.is_Immmortal_object != true)
            {
                Deal_DMG_to_Character();
                SaveScript.time_of_last_damage_recive = Time.time;
                audio_Player.Play();
                StartCoroutine(DMG_Delay_Restart());
            }
            if(Enemy_Type.EnemyType.Golem != GetComponentInParent<Enemy_Type>().enemyType)
            {
                this.GetComponentInParent<EnemyMovement>().fov_angle = 360f;
                if (reset == true)
                {
                    reset = false;
                    StartCoroutine(Reset_Angle());
                }
            }
           
            
        } 
    }

    IEnumerator Reset_Angle()
    {
        yield return new WaitForSeconds(4f);
        //Debug.Log("Reset");
        this.GetComponentInParent<EnemyMovement>().fov_angle = 60f;
        reset = true;
    }
    IEnumerator DMG_Delay_Restart()
    {
        yield return wait_before_attack;
        enemy_can_attack = true;
    } 

    public void Deal_DMG_to_Character()
    {
        correct_dmg_reduce_by_armor = 1.0f - SaveScript.armora_decrease;
        enemy_can_attack = false;
        if(SaveScript.is_shielf_active == true)
        {
            SaveScript.agression_lvl = SaveScript.agression_lvl + 0.05f;
            correct_dmg_reduce_by_Skill = 1.0f - SaveScript.damage_reduce_by_Guardianship;
            SaveScript.health -= (damage_enemy * correct_dmg_reduce_by_armor * correct_dmg_reduce_by_Skill);
        }
        else
        {
            SaveScript.agression_lvl = SaveScript.agression_lvl + 0.1f;
            SaveScript.health -= damage_enemy * correct_dmg_reduce_by_armor;
        }
    }
}
