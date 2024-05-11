using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Attack : MonoBehaviour
{
    private GameObject mesh_to_Destroy;
    public int basic_weapon_damage;
    // Start is called before the first frame update
    void Start()
    {
        
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

        
    }

    IEnumerator Wait_before_Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(mesh_to_Destroy);
    }

}
