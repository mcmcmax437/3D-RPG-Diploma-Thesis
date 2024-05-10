using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    //for roof box colider
    public LayerMask boxLayer;

    public GameObject vfx_spawm_point;
    private WaitForSeconds nearEnemy = new WaitForSeconds(0.22f);

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

    }

    void Update()
    {
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
                for(int i = 0; i < player_mesh_parts.Length; i++)
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
            if (SaveScript.is_character_equip_a_weapon == true)
            {
                Basic_or_Critical_Attack();
            }
        }

    }

    public void Basic_or_Critical_Attack()
    {         
            float randomNumber = Random.value;            
            if (randomNumber <= SaveScript.critical_hit_chance)
            {
            anim.SetTrigger(attacks_tags[6]);
            audio_Player.clip = weapon_SFX[6];
            audio_Player.Play();

        }
        else
            {
            anim.SetTrigger(attacks_tags[SaveScript.weapon_index]);
            audio_Player.clip = weapon_SFX[SaveScript.weapon_index];
            //audio_Player.Play();
        }    
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

}

