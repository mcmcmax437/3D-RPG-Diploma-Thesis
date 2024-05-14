using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Transform : MonoBehaviour
{
    //flame nova/twist
    public GameObject target_point;
    public GameObject vfx_object_container;
    public float speed = 5.0f;
    public float duration_of_life = 1.5f;
    public float spell_mana_cost = 0.06f;
    

    private GameObject vfx_target_save;
    public GameObject player;
    //
    public bool enemy_search = false ;
    public bool non_moving = false;
    public bool support_spell_follow_player = false;
    public bool shield_spell = false;
    public bool power_stats_up_spell = false;
    public bool heal_magic = false;
  

    public bool invisibility_spell_is_active = false;

    public GameObject object_triggered;
    public int damage = 30;



    private void Start()
    {

        vfx_target_save = SaveScript.spell_target;
        player = GameObject.FindGameObjectWithTag("Player") ;
        if(invisibility_spell_is_active == true)
        {
            SaveScript.is_invisible = true; 
        }

        if (shield_spell == true)
        {
            SaveScript.is_shielf_active = true;
        }
        if(power_stats_up_spell == true)
        {
            SaveScript.strength_increase = 100;
        }
        
    }
    // Update is called once per frame
    void Update()
    {

       

        if (target_point != null) //avarage target spel position - worl*
        {
            transform.position = Vector3.LerpUnclamped(transform.position/*current pos*/, target_point.transform.position/*target pos*/, speed * Time.deltaTime);   //fuction to move between object a and b with speed c (from curtrent position to target with speed multiplied by delta time
        }
        if(enemy_search == true) //enemy search spell attack
        {
            if (vfx_target_save != null)
            {
                transform.position = Vector3.LerpUnclamped(transform.position, vfx_target_save.transform.position, speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

            }
        } 
        if(non_moving == true) //click on enemy magic
        {
            if (vfx_target_save != null)
            {
                transform.position = vfx_target_save.transform.position;
            }
            else
            {
                Destroy(vfx_object_container);
            }
        }
        if(support_spell_follow_player == true)
        {
            transform.position = player.transform.position;
            duration_of_life = 100;
            if(SaveScript.mana <= 0.02)
            {
                Destroy(vfx_object_container);
            }
        }

        if (heal_magic == true)
        {
            SaveScript.health += SaveScript.health_regeneration_skill * Time.deltaTime;
        }

        SaveScript.mana -= spell_mana_cost * Time.deltaTime;

        Destroy(vfx_object_container, duration_of_life);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") && other.transform.gameObject != object_triggered)
        {
            other.transform.gameObject.GetComponent<EnemyMovement>().full_HP -= damage;
            object_triggered = other.transform.gameObject;
        }

    }
}
