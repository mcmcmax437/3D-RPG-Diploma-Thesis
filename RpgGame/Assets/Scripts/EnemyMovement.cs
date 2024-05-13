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
    private float rotation_speed = 500.0f; //perfect

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

    // Start is called before the first frame update
    void Start()
    {
        audio_Player = GetComponent<AudioSource>();
        current_enemy.GetComponent<Outline>().enabled = false;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.avoidancePriority = Random.Range(5, 75);
        curr_HP = full_HP;
    } 

    // Update is called once per frame
    void Update()
    {
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

            if (distance_to_player < attack_Range || distance_to_player > chasing_Range) //if character is out of view  range or attack range - than enemy stop
            {
                nav.isStopped = true;

                if(distance_to_player < chasing_Range)
                {
                    //Look_At_Player_Spherical_LERP();        //can be claimed as self-directed attack
                }


                if (distance_to_player < attack_Range && enemy_information.IsTag("nonAttack"))
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
            else //if character is in the field of view  range or attack range - than enemy move towards you 
            {
                nav.isStopped = false;
                nav.destination = player.transform.position;
            }

            if(curr_HP > full_HP)
            {
                anim.SetTrigger("hit");
                curr_HP = full_HP;
                RandomAudio_Hit();
                fillHealt = full_HP;
                fillHealt /= 100.0f;
                HP_bar.fillAmount = fillHealt;
            }
        }

        if(full_HP <= 1 && enemy_is_alive == true)
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
        Destroy(gameObject, 0.2f);
    }
}
