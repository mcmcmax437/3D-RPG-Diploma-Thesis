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
            
            int dmg_check = 0;
            if(player.GetComponent<PlayerMovement>().critical_attack_is_active == true)
            {
                dmg_check = basic_weapon_damage * SaveScript.critical_dmg_multiply;
                other.transform.gameObject.GetComponent<EnemyMovement>().full_HP -= (basic_weapon_damage*SaveScript.critical_dmg_multiply);
                can_deal_dmg = false;
            }
            else
            {
                dmg_check = basic_weapon_damage;
                other.transform.gameObject.GetComponent<EnemyMovement>().full_HP -= basic_weapon_damage;
                can_deal_dmg = false;
            }     
            Debug.Log("Monster = " + other.name + " HP = " + other.transform.gameObject.GetComponent<EnemyMovement>().full_HP + " DMG = " + dmg_check);
            StartCoroutine(ResetDMG());
        }

      
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
