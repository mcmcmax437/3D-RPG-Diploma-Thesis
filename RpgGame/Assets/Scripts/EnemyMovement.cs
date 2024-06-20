using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public bool Goblin_Warrior = false;
    public bool Piglins = false;
    public bool Skeleton = false;
    public bool temp_Priority = false; // temp to check
    private bool can_call_support = false;
    private Vector3 buffed_Skeleton = new Vector3(15.0f, 15.0f, 15.0f);
    private int buffed_probability = 10;
    private bool sup_skill_used = false;
    private bool change_position = false;
    private float sup_skill_CD = 10f;
    private int amount_of_reinforcment = 2;
    public GameObject support_enemy;


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
    public float chasing_Range = 9.0f;   //range in which enemy will run after character
    public float rotation_speed = 500.0f; //perfect
    private float stop_distance = 2f;
    private float group_brain_radius = 13f;

    public Transform patrol_main_obj;
    public float patrol_radius = 10.0f;
    public float wait_time_at_point = 2.0f;

    private bool is_patroling = true; //true? 

    private int maxHP;

    public int full_HP = 100;
    private int curr_HP;

    private bool enemy_is_alive = true;

    private AudioSource audio_Player;
    public AudioClip[] get_Hit_SFX;

    public GameObject Eye_Canvas;
    public GameObject Question_Canvas;
    public GameObject Attack_Canvas;

    public GameObject bar_Container;
    public Image HP_bar;
    private float fillHealth;
    public GameObject main_camera;

    private bool destination_run = false;

    private Vector3 escape_point;
    public Transform[] escape_target_point;

    private bool roll_out = false;
    private bool roll_is_active = false;
    public float dodgeDistance = 5f;
    public float aggression_lvl = 0.5f; // 0 (passive) to 1 (aggressive)
    private bool playerNearby = false;
    private float aggression_increase = 0.05f;
    private float aggression_decrease = 0.025f;
    public float max_aggression = 1.0f;
    public float min_aggression = 0.0f;

    public bool piglin_was_hit = false;

    public float distance_of_ray = 12f;
    public float time_for_search = 3f;
    private Vector3 last_seen_position;
    private float search_Timer;
    private bool player_is_inSight;
    private bool look_for_player = true;
    private bool cant_see_player = true;
    private bool reset_piglins_chase_range = false;
    public float fov_angle = 60f;

    // Start is called before the first frame update
    void Start()
    {
        audio_Player = GetComponent<AudioSource>();
        current_enemy.GetComponent<Outline>().enabled = false;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.avoidancePriority = UnityEngine.Random.Range(5, 75);
        curr_HP = full_HP;
        maxHP = full_HP;

        Eye_Canvas.SetActive(false);
        Question_Canvas.SetActive(false);
        Attack_Canvas.SetActive(false);

        if (Goblin_Warrior == true)
        {
            Set_Patrol_Destination();
        }
        if (Goblin_Warrior == true && patrol_main_obj == null)
        {
            is_patroling = false;
        }
        if (GetComponent<Enemy_Type>().enemyType == Enemy_Type.EnemyType.Piglin)
        {
            chasing_Range = 0;
        }

        if (Skeleton == true)
        {
            int random = UnityEngine.Random.Range(1, 101);
            if (random <= buffed_probability || temp_Priority == true) //10 per cent to be able to call support
            {
                can_call_support = true;
                transform.localScale = buffed_Skeleton;
            }
            else
            {
                can_call_support = false;
            }
            if (SaveScript.weapon_index == -1 && Piglins == true)
            {
                reset_piglins_chase_range = true;
            }
        }

        if (!Piglins)
        {
            distance_of_ray = chasing_Range;
        }
    

    }

    // Update is called once per frame
    void Update()
    {

        if (main_camera == null)
        {
            main_camera = GameObject.Find("Main Camera");
        }
        if (patrol_main_obj == null)
        {
            new WaitForSeconds(1);
        }
        if(SaveScript.weapon_index != -1)
        {
            reset_piglins_chase_range = false;
        }
        else
        {
            reset_piglins_chase_range = true;
            piglin_was_hit = true;
        }
        if(reset_piglins_chase_range == true)
        {
            chasing_Range = 12.0f;
           
        }
        if (reset_piglins_chase_range == false && Piglins == true && chasing_Range != 60)
        {
            Attack_Canvas.SetActive(false);
            chasing_Range = 0.0f;
        }

        bar_Container.transform.LookAt(main_camera.transform.position);

        if(SaveScript.is_invisible == true)
        {
            cant_see_player = true;
        }

        if (enemy_is_alive == true)
        {

            if (Input.GetKeyDown(KeyCode.Z) && distance_to_player < 5f && SaveScript.stamina > 0.2)
            {
                roll_out = true;
            }
            Enemy_Outline();
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            Enemy_Running();

            enemy_information = anim.GetCurrentAnimatorStateInfo(0);
            distance_to_player = Vector3.Distance(transform.position, player.transform.position);
            if(distance_to_player > chasing_Range)
            {
                Question_Canvas.SetActive(false);
                Attack_Canvas.SetActive(false);
            }

            if (destination_run == true && Piglins == true)
            {
                chasing_Range = 0;
            }

            if (distance_to_player <= chasing_Range && destination_run == false && is_patroling == false)
            {
                //Debug.Log(fov_angle);
                if (SaveScript.is_invisible == false)
                {
                    Question_Canvas.SetActive(true);
                    Check_If_Player_is_InSight();
                  // Debug.Log(last_seen_position);
                  //  Debug.Log(player_is_inSight);
                    if (player_is_inSight == true)
                    {
                        cant_see_player = false;
                        Question_Canvas.SetActive(false);
                        //last_seen_position = player.transform.position;
                        search_Timer = 0f;
                        nav.destination = player.transform.position;
                        Main_Attack_System();
                    }
                    else if (player_is_inSight == false && last_seen_position != Vector3.zero)
                    {
                        //Debug.Log("INSIDE");
                        NavMeshPath path = new NavMeshPath();
                        nav.CalculatePath(last_seen_position, path); 
                        StartCoroutine(Reset_Angle());
                        if (path.status != NavMeshPathStatus.PathComplete)
                        {
                            Look_Aroun_Yourself();
                        }
                        else if (look_for_player == true)
                        {
                            nav.destination = last_seen_position;
                            search_Timer += Time.deltaTime;
                            //Debug.Log(search_Timer);
                            if (search_Timer >= time_for_search)
                            {
                                cant_see_player = true;
                                look_for_player = false;
                                search_Timer = 0f;
                            }       
                           // Debug.Log("Look around");
                            Look_Aroun_Yourself();

                        }
                    }
                }
                else
                {
                    Check_If_Player_is_InSight();
                    Attack_Canvas.SetActive(false);
                    Question_Canvas.SetActive(true);               
                }

            }
            //Debug.Log(search_Timer);
            //Debug.Log(is_patroling);
           // Debug.Log("Goblin_Warrior: " + Goblin_Warrior + " look_for_player: " + look_for_player + " cant_see_player: " + cant_see_player);
            if (Goblin_Warrior == true && look_for_player == false && cant_see_player == true)
            {
                if(patrol_main_obj != null)
                {
                  //if(Enemy_Type.EnemyType.Goblin == GetComponent<Enemy_Type>().enemyType)
                   // {
                  //      is_patroling = true;
                  //  }
                    Set_Patrol_Destination();
                }
                Correct_Aggression();
            }

            if (distance_to_player <= chasing_Range)
            {
               // Question_Canvas.SetActive(true);
                is_patroling = false;
            }

            if (roll_out == true && roll_is_active == false)
            {
                roll_is_active = true;
                Roll();
                StartCoroutine(Reset_Roll_Triger());
            }

            //Debug.Log(Skeleton + " " + can_call_support + " " + sup_skill_used);
            if (Skeleton == true && can_call_support == true && sup_skill_used == false && player_is_inSight == true)
            {
                bool enemy_is_near_skeleton = Search_Enemy_Near_Skeleton();
                if (enemy_is_near_skeleton == false && distance_to_player <= 9f && SaveScript.agression_lvl > 0.7f)
                {
                    sup_skill_used = true;
                    SaveScript.agression_lvl -= 0.5f;
                    anim.SetTrigger("skill");
                    Spawn_Reinforcment();
                    StartCoroutine(Reset_Sup_Skill());
                    change_position = false;
                }
            }

            //curr_HP = was
            //full_hp - are
            if (curr_HP > full_HP)
            {
                anim.SetTrigger("hit");
                curr_HP = full_HP;
                RandomAudio_Hit();
                fillHealth = Convert.ToSingle(full_HP) / Convert.ToSingle(maxHP);
               // Debug.Log(fillHealth);
                HP_bar.fillAmount = fillHealth;
                if (GetComponent<Enemy_Type>().enemyType == Enemy_Type.EnemyType.Piglin)
                {
                    piglin_was_hit = true;
                    chasing_Range = 60f;
                    StartCoroutine(Reset_Piglin_Renge());
         
                }

            }
            Debug.Log(destination_run);

            if (full_HP < maxHP / 2 && Piglins == true && destination_run == false)
            {
                destination_run = true;
                chasing_Range = 0;
                //Debug.Log("RUN AWAy");
                Run_Away();
            }

        }


        Vector3 dest = nav.destination;
        if (Vector3.Distance(current_enemy.transform.position, dest) <= 1.0f)
        {
            StartCoroutine(Reset_RunAwayTrigger());
        }

        //Debug.Log(nav.isStopped);
        //Debug.Log(Vector3.Distance(current_enemy.transform.position, dest));

        if (full_HP <= 1 && enemy_is_alive == true)
        {
            Enemy_is_Dead();
        }

    }

    public void Main_Attack_System()
    {
        if (is_patroling == false && Goblin_Warrior == true || Piglins == true && piglin_was_hit == true || Skeleton == true && change_position == false)
        {
            if (distance_to_player < attack_Range || distance_to_player > chasing_Range && destination_run != true) //if character is out of view  range or attack range - than enemy stop
            {
                if (destination_run != true)
                {
                    nav.isStopped = true;
                }
                Question_Canvas.SetActive(false);
                Attack_Canvas.SetActive(true);
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
                        fov_angle = 360f;
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
                    Go_To_Player();
                    Attack_Canvas.SetActive(true);
                }

            }

        }

    }

    public void Go_To_Player()
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, player.transform.position, NavMesh.AllAreas, path))
        {
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                nav.destination = player.transform.position;
                nav.isStopped = false;
            }
            else if (path.status == NavMeshPathStatus.PathPartial)
            {
                nav.destination = path.corners[path.corners.Length - 1];
                nav.isStopped = false;
            }
            else
            {
                nav.isStopped = true;
            }
        }
        else
        {
            nav.isStopped = true;
        }
        if (nav.isStopped && nav.velocity.sqrMagnitude < 0.1f)
        {
            nav.speed = 0; 
        }
        else
        {
            nav.speed = 3.5f; 
        }
        if(Piglins == true)
        {
            nav.stoppingDistance = 2f;
        }
        else
        {
            nav.stoppingDistance = stop_distance;
        }
        
    }


    public void Look_At_Player_Spherical_LERP()
    {
        //Debug.Log("LERP");
        Vector3 Pos = (player.transform.position - transform.position).normalized;
        Quaternion PosRotation = Quaternion.LookRotation(new Vector3(Pos.x, 0, Pos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, PosRotation, Time.deltaTime * rotation_speed);
       // Debug.Log("Correct Angle");
    }
    public void Look_At_Escape_Point_LERP()
    {
        Vector3 Pos = (escape_point - transform.position).normalized;
        Quaternion PosRotation = Quaternion.LookRotation(new Vector3(Pos.x, 0, Pos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, PosRotation, Time.deltaTime * rotation_speed);
        // Debug.Log("Correct Angle");
    }


    public void Enemy_is_Dead()
    {
        SaveScript.agression_lvl = SaveScript.agression_lvl + 0.2f;
        enemy_is_alive = false;
        nav.isStopped = true;
        anim.SetTrigger("death");
        SaveScript.amount_of_chasing_enemies--;
        current_enemy.GetComponent<Outline>().enabled = false;
        is_outliner_active = false;
        nav.avoidancePriority = 1;
        StartCoroutine(Loot_Spawn());
    }

    public void Enemy_Outline()
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
    }

    public void Enemy_Running()
    {
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
            nav.isStopped = false;
            is_attacking = false;
            //Debug.Log("running = " + check);

        }
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
        Enemy_Type enemy_type = GetComponent<Enemy_Type>();
        if (enemy_type.enemyType == Enemy_Type.EnemyType.Skelet)
        {
            yield return new WaitForSeconds(2);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        Instantiate(Loot_from_Enemy, transform.position, transform.rotation);
        SaveScript.killed_enemy++;
        Destroy(gameObject, 0.2f);
    }

    public void Run_Away()
    {
        //int pos = Random.Range(0, 3);
        //nav.destination = escape_target_point[pos].transform.position;
        Calculate_Escape_Point();
        Look_At_Escape_Point_LERP();
        nav.speed = 1.8f;
        nav.destination = escape_point;
        anim.SetBool("running", true);
        nav.isStopped = false;

    }

    IEnumerator Reset_RunAwayTrigger()
    {
        yield return new WaitForSeconds(5);
        destination_run = false;
    }

    IEnumerator Reset_Roll_Triger()
    {
        yield return new WaitForSeconds(5f);
        roll_out = false;
        roll_is_active = false;
    }

    public IEnumerator Reset_Angle()
    {
        yield return new WaitForSeconds(1f);
        fov_angle = 60f;
    }
    IEnumerator Reset_Piglin_Renge()
    {
        yield return new WaitForSeconds(7f);
        if(destination_run == false)
        {
            Look_At_Player_Spherical_LERP();
        }  
        piglin_was_hit = false;
        if(SaveScript.weapon_index != -1)
        {
            chasing_Range = 3f;
        }
        else
        {
            chasing_Range = 12f;
        }
        
    }
    IEnumerator Reset_Sup_Skill()
    {
        yield return new WaitForSeconds(sup_skill_CD);
        sup_skill_used = false;
    }

    
    public void Calculate_Escape_Point()
    {

        Vector3 escape_dir = Vector3.zero;
        float max_escape_distance = 0f;
        Vector3 player_dir = (player.transform.position - transform.position).normalized;

        for (int i = 0; i < 360; i += 5)
        {
            Vector3 new_direction = Quaternion.Euler(0, i, 0) * transform.forward;
            if (Vector3.Dot(new_direction.normalized, player_dir) < 0)
            {
                NavMeshHit hit;
                if (NavMesh.Raycast(transform.position, transform.position + new_direction * 100f, out hit, NavMesh.AllAreas))
                {
                    float distance = Vector3.Distance(transform.position, hit.position);
                    if (distance > max_escape_distance)
                    {
                        max_escape_distance = distance;
                        escape_dir = new_direction;
                    }
                }
            }
        }
        if (max_escape_distance > 0f && escape_dir != Vector3.zero)
        {
            NavMeshHit ray_hit_for_escape;
            if (NavMesh.SamplePosition(transform.position + escape_dir * max_escape_distance, out ray_hit_for_escape, max_escape_distance, NavMesh.AllAreas))
            {
                escape_point = ray_hit_for_escape.position;
            }
            else
            {
                escape_point = transform.position;
            }
        }

    }

    public void Set_Patrol_Destination()
    {
        if (!is_patroling || patrol_main_obj == null) return;

        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrol_radius;
        randomDirection += patrol_main_obj.position;

        NavMeshHit navHit;
        bool foundValidPosition = NavMesh.SamplePosition(randomDirection, out navHit, patrol_radius, NavMesh.AllAreas);

        if (foundValidPosition)
        {
            NavMeshPath path = new NavMeshPath();
            nav.CalculatePath(navHit.position, path);

            if (path.status == NavMeshPathStatus.PathComplete)
            {
                nav.destination = navHit.position;
                nav.isStopped = false;

                anim.SetBool("running", true);
                is_patroling = true;
                StartCoroutine(CheckIfReachedDestination());
            }
            else
            {   
                Set_Patrol_Destination();
            }
        }
        else
        {
            Set_Patrol_Destination();
        }
    }

    private IEnumerator CheckIfReachedDestination()
    {
        while (nav.pathPending || nav.remainingDistance > nav.stoppingDistance)
        {
            yield return null;
        }  
        nav.isStopped = true;
        anim.SetBool("running", false);
        yield return new WaitForSeconds(2f);
        is_patroling = true;
        Set_Patrol_Destination();
    }





    public void Roll()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();

        Vector3[] roll_dirrections = {
            -transform.forward,   // roll back
            transform.forward,    // roll forward
            -transform.right,     // roll left
            transform.right       // roll right
        };

        string[] anim_Roll_triggers = {
            "roll_F",
            "roll_B",
            "roll_L",
            "roll_R"
        };
        float[] weights = new float[roll_dirrections.Length];
        for (int i = 0; i < roll_dirrections.Length; i++)
        {
            Vector3 roll_pos = transform.position + roll_dirrections[i] * dodgeDistance;
            if (NavMesh.SamplePosition(roll_pos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                // Calculate weight based on direction, distance to player, and aggression level
                float weight_of_dirrection = Vector3.Dot(playerDirection, roll_dirrections[i]);
                weight_of_dirrection = (1 - Mathf.Abs(weight_of_dirrection)) * (1 - aggression_lvl);
                weights[i] = weight_of_dirrection;
            }
            else
            {
                weights[i] = -1; 
            }
        }
        int the_best_dirrection = -1;
        float the_best_weight = -1;
        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] > the_best_weight)
            {
                the_best_weight = weights[i];
                the_best_dirrection = i;
            }
        }

        if (the_best_dirrection != -1)
        {
            anim.SetTrigger(anim_Roll_triggers[the_best_dirrection]);
        }
    }
    public void Correct_Aggression()
    {
        if (curr_HP < 0.5f)
        {
            aggression_lvl -= aggression_increase * Time.deltaTime;
        }
        else
        {
            aggression_lvl += aggression_decrease * Time.deltaTime;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f) 
        {
            aggression_lvl += aggression_increase * Time.deltaTime;
            playerNearby = true;
        }
        else
        {
            playerNearby = false;
        }

        aggression_lvl = Mathf.Clamp(aggression_lvl, min_aggression, max_aggression);

        if(aggression_lvl == 1)
        {
            StartCoroutine(Reset_Aggression_Lvl());
        }

       // Debug.Log("Aggression Level: " + aggression_lvl);
    }

    IEnumerator Reset_Aggression_Lvl()
    {
        yield return new WaitForSeconds(3f);
        aggression_lvl = 0.2f;
    }
    public bool Search_Enemy_Near_Skeleton()
    {
        Collider[] all_colliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider collider in all_colliders)
        {
            if (collider.CompareTag("enemy") && collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public void Spawn_Reinforcment()
    {
        for (int i = 0; i < amount_of_reinforcment; i++)
        {

            Instantiate(support_enemy, GetRandom_Point_Around(), Quaternion.identity);
            support_enemy.GetComponent<EnemyMovement>().Goblin_Warrior = true;
            support_enemy.GetComponent<EnemyMovement>().patrol_main_obj = current_enemy.transform;
            SaveScript.amount_of_chasing_enemies++;
        }
    }

    public Vector3 GetRandom_Point_Around()
    {
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
        float x = Mathf.Cos(angle) * 8f;
        float z = Mathf.Sin(angle) * 8f;
        Vector3 point_for_spawn = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        return point_for_spawn;
    }


    void Check_If_Player_is_InSight()
    {
        Vector3 player_dir = player.transform.position - transform.position;
        float angle = Vector3.Angle(player_dir, transform.forward);

        if (angle < fov_angle && player_dir.magnitude < distance_of_ray && SaveScript.is_invisible == false)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, player_dir.normalized, out hit, distance_of_ray))
            {
                //Debug.DrawRay(transform.position, player_dir * 10f, Color.red);
                if (hit.transform == player.transform)
                {
                    Debug.DrawRay(transform.position, player_dir * 10f, Color.green);

                    Nearby_Enemy_Will_Know();
                    cant_see_player = false;
                    look_for_player = false;
                    player_is_inSight = true;
                    last_seen_position = player.transform.position;
                }
            }
        }
        else if (player_is_inSight || SaveScript.is_invisible == true)
        {
            Debug.DrawRay(transform.position, player_dir * 10f, Color.red);
            player_is_inSight = false;
            nav.SetDestination(last_seen_position);
            if(transform.position == last_seen_position)
            {
                Look_Aroun_Yourself();
            }
            look_for_player = true;
            cant_see_player = true;
        }
    }

    public void Nearby_Enemy_Will_Know()
    {
        try
        {
            Vector3 player_dir = player.transform.position - transform.position;
            Collider[] all_colliders = Physics.OverlapSphere(transform.position, group_brain_radius);
            foreach (var collider in all_colliders)
            {
                EnemyMovement raycast_system = collider.GetComponent<EnemyMovement>();
                if (raycast_system != null && collider.gameObject != gameObject)
                {
                    //Debug.Log(raycast_system + " KNOW");
                    Debug.DrawRay(transform.position, player_dir * 10f, Color.green);
                    raycast_system.player_is_inSight = true;
                    raycast_system.look_for_player = false;
                    raycast_system.last_seen_position = player.transform.position;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    public void Look_Aroun_Yourself()
    {
        Attack_Canvas.SetActive(false);
        Question_Canvas.SetActive(true);
        transform.Rotate(0, 120 * Time.deltaTime, 0);
    }
}
