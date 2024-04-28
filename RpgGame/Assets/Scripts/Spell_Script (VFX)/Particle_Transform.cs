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

    private GameObject vfx_target_save;
    public GameObject player;
    //
    public bool enemy_search = false ;
    public bool non_moving = false;
    public bool support_spell_follow_player = false;

    private void Start()
    {
        vfx_target_save = SaveScript.spell_target;
        player = GameObject.FindGameObjectWithTag("Player");
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
        }
        Destroy(vfx_object_container, duration_of_life);
    }
}
