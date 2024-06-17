using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Attack : MonoBehaviour
{
    private GameObject mesh_to_Destroy;
    public int basic_weapon_damage;

    private GameObject player;

    private bool can_deal_dmg = true;
    private WaitForSeconds dmg_Pause = new WaitForSeconds(0.1f);
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

        //if we are attacking crate
        if (other.CompareTag("Crate"))
        {
            other.transform.gameObject.GetComponentInParent<Chest>().VFX_crate_text();
            mesh_to_Destroy = other.transform.parent.gameObject;
          
            Destroy(other.transform.gameObject);
            StartCoroutine(Wait_before_Destroy());
        }

        if (other.CompareTag("enemy") && can_deal_dmg == true )
        {
            SaveScript.agression_lvl = SaveScript.agression_lvl + 0.2f;

            Enemy_Type enemy_type = other.GetComponent<Enemy_Type>();
            int dmg_check = 0;
            if(player.GetComponent<PlayerMovement>().critical_attack_is_active == true)
            {
                dmg_check = (basic_weapon_damage + SaveScript.weapon_dmg_scaleUP + SaveScript.strength_increase) * SaveScript.critical_dmg_multiply;
                if (enemy_type.enemyType == Enemy_Type.EnemyType.Golem)
                {
                    if (Random.Range(0f, 1f) >= other.GetComponent<Golem_Movement>().dmg_block_probability) //15 per cent to block dmg
                    {
                        other.transform.gameObject.GetComponent<Golem_Movement>().full_HP -= ((basic_weapon_damage + SaveScript.weapon_dmg_scaleUP + SaveScript.strength_increase) * SaveScript.critical_dmg_multiply);
                    }
                    else
                    {
                        other.GetComponent<Golem_Movement>().audio_Player.clip = other.GetComponent<Golem_Movement>().block_SFX;
                        other.GetComponent<Golem_Movement>().audio_Player.Play();
                    }
                }
                else
                {
                    other.transform.gameObject.GetComponent<EnemyMovement>().full_HP -= ((basic_weapon_damage + SaveScript.weapon_dmg_scaleUP + SaveScript.strength_increase) * SaveScript.critical_dmg_multiply);

                }
                can_deal_dmg = false;
            }
            else
            {
                dmg_check = (basic_weapon_damage + SaveScript.weapon_dmg_scaleUP + SaveScript.strength_increase);

                if(enemy_type.enemyType == Enemy_Type.EnemyType.Golem)
                {
                    if (Random.Range(0f, 1f) >= other.GetComponent<Golem_Movement>().dmg_block_probability) //15 per cent to block dmg
                    {
                        other.transform.gameObject.GetComponent<Golem_Movement>().full_HP -= (basic_weapon_damage + SaveScript.weapon_dmg_scaleUP + SaveScript.strength_increase);
                    }
                    else
                    {
                        other.GetComponent<Golem_Movement>().audio_Player.clip = other.GetComponent<Golem_Movement>().block_SFX;
                        other.GetComponent<Golem_Movement>().audio_Player.Play();
                    }
                }
                else
                {
                    other.transform.gameObject.GetComponent<EnemyMovement>().full_HP -= (basic_weapon_damage + SaveScript.weapon_dmg_scaleUP + SaveScript.strength_increase);
                }
               
                
                can_deal_dmg = false;
            }

            if(enemy_type.enemyType != Enemy_Type.EnemyType.Golem)
            {
                other.GetComponent<EnemyMovement>().fov_angle = 360f;
            }
            
          

        }
    



    /* Debug.Log(basic_weapon_damage + " " + SaveScript.weapon_dmg_scaleUP + " " + SaveScript.strength_increase);
     if (enemy_type.enemyType == Enemy_Type.EnemyType.Golem)
     {
         Debug.Log("Monster = " + other.name + " HP = " + other.transform.gameObject.GetComponent<Golem_Movement>().full_HP + " DMG = " + dmg_check);
     }
     else
     {
         Debug.Log("Monster = " + other.name + " HP = " + other.transform.gameObject.GetComponent<EnemyMovement>().full_HP + " DMG = " + dmg_check);

     }*/
    StartCoroutine(ResetDMG());
        

      
    }


    IEnumerator Wait_before_Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(mesh_to_Destroy);
    }

    IEnumerator ResetDMG()
    {
        yield return dmg_Pause;
        can_deal_dmg = true;
    }

}
