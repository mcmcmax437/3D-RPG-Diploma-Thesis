using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public bool Goblin_Warrior = false;
    public bool Piglins = false;
  

    public GameObject Loot_from_Enemy;

    public GameObject current_enemy;
    private bool is_outliner_active = false;

    private AnimatorStateInfo enemy_information;
    private NavMeshAgent nav;
    private Animator anim;
    private float x;
    private float z;
    private float velocitySpeed;
    public GameObject player;
    private float distance_to_player;
    private bool is_attacking;
    public float attack_Range = 2.0f;
    public float chasing_Range = 12.0f;   //range in which enemy will run after character
    public float rotation_speed = 500.0f; //perfect

    public Transform patrol_main_obj;  
    public float patrol_radius = 15.0f;  
    public float wait_time_at_point = 2.0f;  

    private Vector3 targetPoint;
    private bool is_waiting;
    private float wait_timer;
    private bool is_patroling = true;

    private int maxHP;

    public int full_HP = 100;
    private int curr_HP;
    private int fear_lvl = 100;
    private int fear_lvl_curr;
    private bool enemy_is_alive = true;

    private AudioSource audio_Player;
    public AudioClip[] get_Hit_SFX;

    public GameObject bar_Container;
    public Image HP_bar;
    private float fillHealt;
    public GameObject main_camera;

    private bool destination_run = false;

    private Vector3 escape_point;
    public Transform[] escape_target_point;

    // Start is called before the first frame update
    void Start()
    {
        audio_Player = GetComponent<AudioSource>();
        current_enemy.GetComponent<Outline>().enabled = false;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.avoidancePriority = Random.Range(5, 75);
        curr_HP = full_HP;
        maxHP = full_HP;

        if(Goblin_Warrior == true)
        {
            Set_Petrol_Destination();
        }
        if (Goblin_Warrior == true && patrol_main_obj == null)
        {
            is_patroling = false;
        }

    } 

    // Update is called once per frame
    void Update()
    {

        if (patrol_main_obj == null)
        {
            new WaitForSeconds(1);
        }

        bar_Container.transform.LookAt(main_camera.transform.position);
        //HP_bar.transform.LookAt(main_camera.transform.position);

        if (enemy_is_alive == true)
        {
            //outline
            if (is_outliner_active == false)
            {
                is_outliner_active = true;
                if (SaveScript.spell_target == current_enemy)
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
            //

            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            x = nav.velocity.x;
            z = nav.velocity.z;
            velocitySpeed = new Vector2(x, z).magnitude;
            // velocitySpeed = x+z;      
            if (velocitySpeed == 0)
            {
                anim.SetBool("running", false);
                // Debug.Log("RUN = " + check);
            }
            else if (velocitySpeed != 0)
            {

                anim.SetBool("running", true);
                // check = anim.GetBool("running");
                is_attacking = false;
                //Debug.Log("running = " + check);

            }

            enemy_information = anim.GetCurrentAnimatorStateInfo(0);
            distance_to_player = Vector3.Distance(transform.position, player.transform.position);


            if (is_patroling == false && Goblin_Warrior == true || Piglins == true)
            {
                if (distance_to_player < attack_Range || distance_to_player > chasing_Range && destination_run != true) //if character is out of view  range or attack range - than enemy stop
                {
                    if (destination_run != true)
                    {
                        nav.isStopped = true;
                    }

                    //if(distance_to_player < chasing_Range)
                    // {
                    //Look_At_Player_Spherical_LERP();        //can be claimed as self-directed attack
                    // }


                    if (distance_to_player < attack_Range && enemy_information.IsTag("nonAttack") && SaveScript.is_invisible != true && destination_run != true)
                    {

                        if (is_attacking == false)
                        {

                            is_attacking = true;
                            anim.SetTrigger("attack");
                            Look_At_Player_Spherical_LERP();   //little bit chunky
                        }
                    }

                    if (distance_to_player < attack_Range && enemy_information.IsTag("attack"))
                    {

                        if (is_attacking == true)
                        {
                            is_attacking = false;
                        }
                    }
                }
                else if (distance_to_player > attack_Range && enemy_information.IsTag("nonAttack") && !anim.IsInTransition(0))
                {
                    if (SaveScript.is_invisible == false && destination_run == false)
                    {
                        nav.isStopped = false;
                        nav.destination = player.transform.position;
                    }

                }

            }

            if(Goblin_Warrior == true)
            {
                is_patroling = true;
                if (!is_waiting && nav.remainingDistance <= 2.0f)
                {

                    is_waiting = true;
                    wait_timer = wait_time_at_point;
                    is_patroling = false;
                }
                if (is_waiting)
                {
                    wait_timer -= Time.deltaTime;

                    if (wait_timer <= 0)
                    {

                        is_waiting = false;
                        Set_Petrol_Destination();
                        nav.isStopped = false;
                    }
                }
                
            }
           
            if(distance_to_player <= chasing_Range)
            {
                is_patroling = false;
            }

            if (curr_HP > full_HP)
            {
                anim.SetTrigger("hit");
                curr_HP = full_HP;
                RandomAudio_Hit();
                fillHealt = full_HP;
                fillHealt /= 100.0f;
                HP_bar.fillAmount = fillHealt;
               
            }

            if(full_HP < maxHP / 2 && Piglins == true && destination_run == false)
            {
                destination_run = true;
                //Debug.Log("RUN AWAy");
                Run_Away();
            }
          
        }
       
        
        Vector3 dest = nav.destination;
        Vector3 curr_pos = current_enemy.transform.position.normalized;
         if(Vector3.Distance(current_enemy.transform.position, dest) <= 1.0f)
        {
            StartCoroutine(Reset_RunAwayTrigger());
        }
      
        //Debug.Log(nav.isStopped);
        //Debug.Log(Vector3.Distance(current_enemy.transform.position, dest));

        if (full_HP <= 1 && enemy_is_alive == true)
        {
            enemy_is_alive = false;
            nav.isStopped = true;
            anim.SetTrigger("death" );
            current_enemy.GetComponent<Outline>().enabled = false;
            is_outliner_active = false;
            nav.avoidancePriority = 1;
            StartCoroutine(Loot_Spawn());
        }

    }

    public void Look_At_Player_Spherical_LERP()
    {
        Vector3 Pos = (player.transform.position - transform.position).normalized;
        Quaternion PosRotation = Quaternion.LookRotation(new Vector3(Pos.x, 0, Pos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, PosRotation, Time.deltaTime * rotation_speed);
    }
     
   

    public void RandomAudio_Hit()
    {
        int randomNumber = UnityEngine.Random.Range(1, 101);
        if (randomNumber > 0 && randomNumber < 33)
        {
            audio_Player.clip = get_Hit_SFX[0];
        }
        else if (randomNumber >= 33 && randomNumber < 66)
        {
            audio_Player.clip = get_Hit_SFX[1];
        }
        else if (randomNumber >= 66 && randomNumber < 101)
        {
            audio_Player.clip = get_Hit_SFX[2];
        }
        audio_Player.Play();
    }

    IEnumerator Loot_Spawn()
    {
        yield return new WaitForSeconds(1);
        Instantiate(Loot_from_Enemy, transform.position, transform.rotation);
        SaveScript.killed_enemy++;
        Destroy(gameObject, 0.2f);
    }

    public void Run_Away()
    {
        anim.SetBool("running", true);
        nav.isStopped = false;

        //int pos = Random.Range(0, 3);
        //nav.destination = escape_target_point[pos].transform.position;
        CalculateEscapePoint();
        nav.destination = escape_point;
    }

    IEnumerator Reset_RunAwayTrigger()
    {
        yield return new WaitForSeconds(5);
        destination_run = false;
    }


    public void CalculateEscapePoint()
    {

        Vector3 escapeDirection = Vector3.zero;
        float maxDistance = 0f;

        for (int i = 0; i < 360; i += 45) 
        {
            Vector3 direction = Quaternion.Euler(0, i, 0) * transform.forward;
            NavMeshHit hit;
            if (NavMesh.Raycast(transform.position, transform.position + direction * 100f, out hit, NavMesh.AllAreas))
            {
                float distance = Vector3.Distance(transform.position, hit.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    escapeDirection = direction;
                }
            }
        }
        NavMeshHit escapeHit;
        if (NavMesh.SamplePosition(transform.position + escapeDirection * maxDistance, out escapeHit, maxDistance, NavMesh.AllAreas))
        {
            escape_point = escapeHit.position;
        }
        else
        {
            escape_point = transform.position;
        }
    }

    void Set_Petrol_Destination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrol_radius;
        randomDirection += patrol_main_obj.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, patrol_radius, -1);

        anim.SetBool("running", true);
        nav.isStopped = false;
        nav.destination = navHit.position;
    }

}
