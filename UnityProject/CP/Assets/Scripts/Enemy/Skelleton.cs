using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Unity.Burst.Intrinsics;

public class Skelleton : MonoBehaviour
{
    public Animator anim;

    Transform playerpos;
    NavMeshAgent agent;
    public float scale;

    public MobsStats stat;

    public float AtackRange, AgrRange;
    public double AtackTime, AtackPreparationTime, AtackKD; 
    public bool Atacktrue;

    public LayerMask PlayerLayers;
    void Start()
    {
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
    void Update()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
        if (stat.MicroStan == true)
        {
            AtackKD = 1;
            AtackTime = 0;
            Atacktrue = false;
            stat.MicroStan = false;
        }
        else
        {
            if (Math.Abs(transform.position.x - playerpos.position.x) <= AtackRange && Math.Abs(transform.position.y - playerpos.position.y) <= AtackRange)
            {
                AtackPreparation();
            }
            else if (Math.Abs(transform.position.x - playerpos.position.x) <= AgrRange && Math.Abs(transform.position.y - playerpos.position.y) <= AgrRange && AtackTime <= 0)
            {
                Pursuit();
            }

            if (Atacktrue == true && AtackPreparationTime <= 0)
            {
                Atack();
            }
        }
        AtackTime -= Time.deltaTime;
        AtackPreparationTime -= Time.deltaTime;
        AtackKD -= Time.deltaTime;
    }
    private void Pursuit()
    {
        flip();
        agent.speed = stat.MoveSpeed;
        agent.angularSpeed = stat.MoveSpeed;
        agent.SetDestination(playerpos.position);
    }
    private void flip()
    {
        if(playerpos.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(scale * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (playerpos.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        }
    }
    private void AtackPreparation()
    {
        if (AtackKD <= 0)
        {
            int AtackRandom = UnityEngine.Random.Range(1, 3);
            if (AtackRandom == 2)
            {
                anim.SetTrigger("AtackPreparation");
                Atacktrue = true;
                AtackTime = 1.3;
                AtackPreparationTime = 0.68;
                AtackKD = UnityEngine.Random.Range(3, 5);
            }
            else
            {
                AtackKD = 1;
            }
        }

    }
    private void Atack()
    {
        anim.SetTrigger("Atack");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, AtackRange, PlayerLayers);
        foreach (Collider2D enemy in hitPlayer)
        {
            enemy.GetComponent<PlayerStats>().TakePhisDamage(stat.Damage);
        }
        Atacktrue = false;
    }
}
