using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    // Для движения персонажа {1}
    private UnityEngine.AI.NavMeshAgent nav;
    private Animator anim; 
    private Ray ray;
    private RaycastHit hit;

    private float x;
    private float z;
    private float velocitySpeed;
    public static int ray_numbers = 6;

    //For Camera
    CinemachineTransposer cinemachineTransposer;
    //public CinemachineVirtualCamera playerCamera;  //free 
    CinemachineOrbitalTransposer cinemachine_orbital_Transposer;
 
    private Vector3 mouse_pos;
    private Vector3 current_pos;
    private string axis_named = "Mouse X";

    private bool isPlayerSelectScene;
    public static bool canMove = true;
    public static bool isPlayerMoving = false;

    public GameObject camera_1_static;
    public GameObject camera_2_free;
    private bool is_camera1_active = true;

    private float previous_health = 1.0f;
    public GameObject get_hit_VFX_Place;
    private WaitForSeconds life_time_hit_effect = new WaitForSeconds(0.1f);


    //for roof box colider
    public LayerMask boxLayer;

    public GameObject vfx_spawm_point;
    private WaitForSeconds nearEnemy = new WaitForSeconds(0.4f);

    public GameObject[] player_mesh_parts;
    public GameObject[] weapons_props;
    public GameObject[] armor_parts_Torso;
    public GameObject[] armor_parts_Legs;
    public string[] attacks_tags;
    public AudioClip[] weapon_SFX;
    public AudioSource audio_Player;

    private AnimatorStateInfo player_information;

    private GameObject trail_mesh;
    private WaitForSeconds traill_time = new WaitForSeconds(0.1f);
    public bool critical_attack_is_active = false;

    public float[] stamina_cost_for_weapon;
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();


        camera_1_static.SetActive(false);
        camera_2_free.SetActive(true);
        SaveScript.vfx_spawn_point = vfx_spawm_point;
        //cinemachineTransposer = playerCamera.GetCinemachineComponent<CinemachineTransposer>();
        //current_pos = cinemachineTransposer.m_FollowOffset;
        cinemachineTransposer = camera_1_static.gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        cinemachine_orbital_Transposer = camera_2_free.gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();

        for (int i = 0; i < weapons_props.Length; i++)
        {
            weapons_props[i].SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "PlayerSelect")
        {
            isPlayerSelectScene = true;
      
        }

        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            Display_Correct_ArmorInShop();
        }
        Check_Class_Info();
        get_hit_VFX_Place.SetActive(false);

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Display_Correct_ArmorInShop();
        }
        //Debug.Log("can mpve " + canMove);
        player_information = anim.GetCurrentAnimatorStateInfo(0); //listen to Animator

        //change correct weapon
        if (SaveScript.should_change_weapon == true)
        {
            SaveScript.should_change_weapon = false;
            for (int i = 0; i < weapons_props.Length; i++)
            {
                weapons_props[i].SetActive(false);
            }
            weapons_props[SaveScript.weapon_index].SetActive(true);
            StartCoroutine(WaitForTrail());
        }


        if (isPlayerSelectScene == false)
        {
            x = nav.velocity.x;
            z = nav.velocity.z;
            velocitySpeed = new Vector2(x, z).magnitude; 

            Ray[] rays = new Ray[ray_numbers];

            if (Input.GetMouseButtonDown(0) && player_information.IsTag("nonAttack") && !anim.IsInTransition(0))
            {
                if (canMove == true)
                {
                    for (int i = 0; i < ray_numbers; i++)
                    {
                        rays[i] = Camera.main.ScreenPointToRay(Input.mousePosition);
                    }

                    Vector3 averageHitPoint = Vector3.zero;

                    foreach (Ray ray in rays)
                    {
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, 300, boxLayer))
                        {
                            if (hit.transform.gameObject.CompareTag("enemy"))
                            {
                                nav.isStopped = false;
                                SaveScript.spell_target = hit.transform.gameObject;
                                averageHitPoint += hit.point;
                                transform.LookAt(SaveScript.spell_target.transform);
                                StartCoroutine(MoveTo()); //wait 3 sec and than isStopped == true

                            }
                            else
                            {
                                SaveScript.spell_target = null;
                                averageHitPoint += hit.point;
                                nav.isStopped = false;
                            }                          
                        }
                    }
                    averageHitPoint /= rays.Length;
                    nav.destination = averageHitPoint;
                }
            }


            if (Input.GetMouseButton(1))
            {
                cinemachine_orbital_Transposer.m_XAxis.m_InputAxisName = axis_named;   //we put "Mouse X" into field of orbital camera to be able to rotate it
            }

            if (Input.GetMouseButtonUp(1))
            {
                cinemachine_orbital_Transposer.m_XAxis.m_InputAxisName = null;
                cinemachine_orbital_Transposer.m_XAxis.m_InputAxisValue = 0;
            }


            // Check if the character is moving (forward or backward)
            anim.SetBool("sprinting", velocitySpeed > 0.1f);
            if(velocitySpeed != 0)
            {
                if(SaveScript.is_character_equip_a_weapon == false)
                {
                    anim.SetBool("sprinting", true);
                    anim.SetBool("equip_a_weapon", false);
                }
                if (SaveScript.is_character_equip_a_weapon == true)
                {
                    anim.SetBool("sprinting", true);
                    anim.SetBool("equip_a_weapon", true);
                }
                isPlayerMoving = true;
            } 
            if (velocitySpeed == 0)
            {
                anim.SetBool("sprinting", false);
                isPlayerMoving = false;
            }
             

            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("sprinting", false);
                nav.destination = transform.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(is_camera1_active == true)
            {
                camera_1_static.SetActive(false);
                camera_2_free.SetActive(true);

                is_camera1_active = false;
            }
            else if (is_camera1_active == false)
            {
                camera_1_static.SetActive(true);
                camera_2_free.SetActive(false);

                is_camera1_active = true;
            }
        }

        //make player invisible 
        if (player_mesh_parts[0].activeSelf == true)
        {
            if(SaveScript.is_invisible == true)
            {
                SaveScript.agression_lvl = SaveScript.agression_lvl - 0.15f;
                for (int i = 0; i < player_mesh_parts.Length; i++)
                {
                    player_mesh_parts[i].SetActive(false);
                }
            }
        }
        //make player visible  
        if (SaveScript.mana <= 0.05)
        {
            if (SaveScript.is_invisible == false)
            {
                for (int i = 0; i < player_mesh_parts.Length; i++)
                {
                    player_mesh_parts[i].SetActive(true);
                   
                }
                SaveScript.should_change_armor = true;
            }
        }


        if(SaveScript.should_change_armor == true)
        {
            for(int i = 0; i < armor_parts_Torso.Length; i++)
            {
                armor_parts_Torso[i].SetActive(false);
                armor_parts_Legs[i].SetActive(false);
            }
            armor_parts_Torso[SaveScript.index_of_equiped_armor].SetActive(true);
            armor_parts_Legs[SaveScript.index_of_equiped_armor].SetActive(true);
            SaveScript.should_change_armor = false;
        }



        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (SaveScript.is_character_equip_a_weapon == true && SaveScript.stamina > 0.2)
            {
                Basic_or_Critical_Attack();
            }
        }

        if(SaveScript.health <= 0.0f)
        {   
                if (SaveScript.uniqe_features_index == 3 && Time.time - SaveScript.time_of_uniqe_feature_activasion > SaveScript.uniqe_features_index_CD)
            {
                SaveScript.time_of_uniqe_feature_activasion = Time.time;
                SaveScript.health = 0.5f;
            }
            else
            {
                SceneManager.LoadScene(0);   // 0 - Player Select  1 - Terrain1 (More can check in File -> Build Settings)
                SaveScript.health = 1.0f;
            }
   
        }

        if(previous_health > SaveScript.health)
        {
            CharacterGetHit();
        }
       

       


    }

    public void Basic_or_Critical_Attack()
    {         
            float randomNumber = Random.value;            
            if (randomNumber <= SaveScript.critical_hit_chance)
            {

            critical_attack_is_active = true;
            anim.SetTrigger(attacks_tags[6]);
            audio_Player.volume = 0.4f;
            audio_Player.clip = weapon_SFX[6];
            audio_Player.Play();
            SaveScript.stamina -= stamina_cost_for_weapon[6];

        }
        else
            {
            critical_attack_is_active = false;
            anim.SetTrigger(attacks_tags[SaveScript.weapon_index]);
            audio_Player.volume = 0.3f;
            audio_Player.clip = weapon_SFX[SaveScript.weapon_index];
            //audio_Player.Play();
            SaveScript.stamina -= stamina_cost_for_weapon[SaveScript.weapon_index];
        }    
    }

    IEnumerator TurnOff_Hit_VFX()
    {
        yield return life_time_hit_effect;
        get_hit_VFX_Place.SetActive(false);
    }

    public void CharacterGetHit()
    {
        get_hit_VFX_Place.SetActive(true);
        previous_health = SaveScript.health;
        StartCoroutine(TurnOff_Hit_VFX());
    }
    public void Weapon_SFX_Play()
    { 
        
        audio_Player.Play();
    }

    public void TurnOn_Trail()
    {
        trail_mesh.GetComponent<Renderer>().enabled = true;
    }

    public void TurnOff_Trail()
    {
        trail_mesh.GetComponent<Renderer>().enabled = false;
    }
    IEnumerator MoveTo()
    { 
        yield return nearEnemy;
        nav.isStopped = true;
    }

    IEnumerator WaitForTrail()
    {
        yield return traill_time;
        trail_mesh = GameObject.Find("Trail");
        
        trail_mesh.GetComponent<Renderer>().enabled = false;
        
    }

    public void Check_Class_Info()
    {
        if (SaveScript.uniqe_features_index == 0)
        {
            Debug.Log(SaveScript.class_Avarage + "" + SaveScript.class_Mage + "" + SaveScript.class_Seller + "" + SaveScript.class_Warrior);
            Debug.Log("None Ability");
        }
        else if (SaveScript.uniqe_features_index == 1)
        {
            Debug.Log(SaveScript.class_Avarage + "" + SaveScript.class_Mage + "" + SaveScript.class_Seller + "" + SaveScript.class_Warrior);
            Debug.Log("More Mana Regeneration and +20% spell/magic damage");
        }
        else if (SaveScript.uniqe_features_index == 2)
        {
            Debug.Log(SaveScript.class_Avarage + "" + SaveScript.class_Mage + "" + SaveScript.class_Seller + "" + SaveScript.class_Warrior);
            Debug.Log("Price in shop is -20% lower");
        }
        else if (SaveScript.uniqe_features_index == 3)
        {
            Debug.Log(SaveScript.class_Avarage + "" + SaveScript.class_Mage + "" + SaveScript.class_Seller + "" + SaveScript.class_Warrior);
            Debug.Log("You can survive lethal damage and regain 50% HP (500 sec CD)");
        }

    }

    public void Display_Correct_ArmorInShop()
    {
        if(isPlayerSelectScene == true)
        {
            if (SaveScript.player_index_character == 1 || SaveScript.player_index_character == 2 || SaveScript.player_index_character == 0)
            {
                GetComponent<Stats_Info>().armor_in_shop[0].SetActive(true);
                GetComponent<Stats_Info>().armor_in_shop[1].SetActive(false);
            }
            else
            {
                GetComponent<Stats_Info>().armor_in_shop[0].SetActive(false);
                GetComponent<Stats_Info>().armor_in_shop[1].SetActive(true);
            }
        }
    }

}

