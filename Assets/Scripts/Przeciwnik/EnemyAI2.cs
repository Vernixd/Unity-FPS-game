using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2 : MonoBehaviour
{

    public float walkSpeed = 5.0f;
    public float attackDistance = 2.0f;

    public float attackDamage = 20.0f;
    public float attackDelay = 1.0f;

    public int hp = 20;

    public Transform[] transforms;

    private float timer = 0;

    private string currentState;
    private Animator animator;
    private AnimatorStateInfo stateInfo;

    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int nextWayPoint = 0;
    private bool patrol = true;
    private float ok;


    static public float iloscEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            patrol = false;
            navMeshAgent.enabled = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && hp > 0)
        {
            navMeshAgent.enabled = false;
            patrol = false;
            Quaternion targetRotation = Quaternion.LookRotation(other.transform.position - transform.position);
            float oryginalX = transform.rotation.x;
            float oryginalZ = transform.rotation.z;

            Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
            finalRotation.x = oryginalX;
            finalRotation.z = oryginalZ;
            transform.rotation = finalRotation;

            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance > attackDistance)
            {
                animationSet("run");
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
            else if (distance <= attackDistance)
            {

                if (timer <= 0)
                {
                    animationSet("attack0");
                    other.SendMessage("takeHit", attackDamage);
                    timer = attackDelay;
                }
            }
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void Patrol()
    {
        animationSet("run");
        navMeshAgent.destination = waypoints[nextWayPoint].position;
        navMeshAgent.Resume();

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % waypoints.Length;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            navMeshAgent.enabled = true;
            patrol = true;
        }
    }

    private void animationSet(string animationToPlay)
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animationReset();

        if (animationToPlay != "run")
        {
            Debug.Log(stateInfo.IsName("Base Layer.wound"));
        }

        if (currentState == "")
        {
            currentState = animationToPlay;
            if (currentState != "run")
            {
                Debug.Log(currentState);
            }

            if (stateInfo.IsName("Base Layer.walk") && currentState != "walk")
            {
                animator.SetBool("walkToIdle0", true);
            }

            if (stateInfo.IsName("Base Layer.run") && currentState != "run")
            {
                animator.SetBool("runToIdle0", true);
            }

            if (stateInfo.IsName("Base Layer.death") && currentState != "death")
            {
                animator.SetBool("deathToIdle0", true);
            }

            string state = "idle0To" + currentState.Substring(0, 1).ToUpper() + currentState.Substring(1);
            animator.SetBool(state, true);
            currentState = "";
        }
    }

    private void animationReset()
    {
        if (!stateInfo.IsName("Base Layer.idle0"))
        {
            animator.SetBool("idle0ToIdle1", false);
            animator.SetBool("idle0ToWalk", false);
            animator.SetBool("idle0ToRun", false);
            animator.SetBool("idle0ToWound", false);
            animator.SetBool("idle0ToSkill0", false);
            animator.SetBool("idle0ToAttack1", false);
            animator.SetBool("idle0ToAttack0", false);
            animator.SetBool("idle0ToDeath", false);
        }
        else
        {
            animator.SetBool("walkToIdle0", false);
            animator.SetBool("runToIdle0", false);
            animator.SetBool("deathToIdle0", false);
        }
    }

    void takeHit(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            if (hp == 0)
            {
                patrol = false;
                animationSet("death");
                Destroy(gameObject, 1);
                iloscEnemy -= 1;
            }
        }
        else
        {
            animator.CrossFade("wound", 0.5f);
        }
    }
    // Use this for initialization
    void Start()
    {
        animator = transforms[0].GetComponent<Animator>();
        currentState = "";
        ok = Random.Range(2, 5);
        Patrol();

    }

    // Update is called once per frame
    void Update()
    {
        ok -= Time.deltaTime;
        if (ok <= 0)
        {
            navMeshAgent.enabled = true;
            ok += Time.deltaTime;
        }
        if (patrol)
        {
            Patrol();
        }
    }

    void Awake()
    {
        iloscEnemy = RespawnLosowy2.iloscPrzekazanie;

    }
}



