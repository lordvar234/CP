using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{
    public Animator anim;

    Transform playerpos;
    NavMeshAgent agent;
    public float scale;

    public MobsStats stat;

    public float AtackKD, AtackRange, DashRange;
    public double AtackTime;

    public int combo;

    public bool DashActive;
    private Vector3 Playerpos;

    public double PoisonBuffTime;

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
            combo = 0;
            AtackKD = 1;
            AtackTime = 1;
            DashActive = false;
            stat.MicroStan = false;
        }

        flip();

        if (Math.Abs(transform.position.x - playerpos.position.x) <= AtackRange && Math.Abs(transform.position.y - playerpos.position.y) <= AtackRange && AtackKD <= 0)
        {
                AtackPreparation();
        }
        else if (Math.Abs(transform.position.x - playerpos.position.x) <= DashRange && Math.Abs(transform.position.y - playerpos.position.y) <= DashRange && Math.Abs(transform.position.x - playerpos.position.x) > 2 && Math.Abs(transform.position.y - playerpos.position.y) > 2 &&  AtackKD <= 0)
        {
            int Type = UnityEngine.Random.Range(0, 3);
            switch(Type)
            {
                case 0:
                    AtackKD = 1;
                    break;
                case 1:
                    PoisonBuff();
                    break;
                case 2:
                    DashPreparation();
                    break;
            }
        }
        if (AtackTime <= 0)
        {
            Action();
        }
        else
        {
            Playerpos = playerpos.position;
        }

        if(PoisonBuffTime <= 0)
        {
            anim.SetBool("Poison", false);
        }
        AtackTime -= Time.deltaTime;
        AtackKD -= Time.deltaTime;

    }
    private void Action()
    {
        if (combo > 0)
        {
            AttackCombo();
        }
        else if (DashActive)
        {
            Dash();
        }
        else
        {
            agent.SetDestination(playerpos.position);
        }
    }
    private void flip()
    {
        if (playerpos.position.x > transform.position.x)
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
        anim.SetTrigger("AttackPreparation");
        combo = 1;
        AtackTime = 0.68;
        AtackKD = UnityEngine.Random.Range(3, 5);
    }
    private void DashPreparation()
    {
        anim.SetTrigger("DashPreparation");
        AtackTime = 0.8;
        DashActive = true;
        AtackKD = UnityEngine.Random.Range(2, 4);
    }
    private void PoisonBuff()
    {
        anim.SetTrigger("PoisonBuff");
        PoisonBuffTime = 10;
        AtackTime = 0.68;
        AtackKD = UnityEngine.Random.Range(3, 5);
        anim.SetBool("Poison", true);
    }
    private void AttackCombo()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, AtackRange, PlayerLayers);
        switch (combo)
        {
            case 1:
                anim.SetTrigger("Attack1");          
                foreach (Collider2D player in hitPlayer)
                {
                    player.GetComponent<PlayerStats>().TakePhisDamage(stat.Damage);
                    if (PoisonBuffTime > 0)
                    {
                        player.GetComponent<PlayerBuffsAndDebuffs>().TakeEffect(1);
                    }
                }
                combo = 2;
                AtackTime = 0.2;
                break;
            case 2:
                anim.SetTrigger("Attack2");
                foreach (Collider2D player in hitPlayer)
                {
                    player.GetComponent<PlayerStats>().TakePhisDamage(stat.Damage);
                    if (PoisonBuffTime > 0)
                    {
                        player.GetComponent<PlayerBuffsAndDebuffs>().TakeEffect(1);
                    }
                }
                combo = 3;
                AtackTime = 0.2;
                break;
            case 3:
                anim.SetTrigger("Attack3");
                foreach (Collider2D player in hitPlayer)
                {
                    player.GetComponent<PlayerStats>().TakePhisDamage(stat.Damage * 2);
                    if (PoisonBuffTime > 0)
                    {
                        player.GetComponent<PlayerBuffsAndDebuffs>().TakeEffect(1);
                    }
                }
                combo = 0;
                AtackKD = UnityEngine.Random.Range(3, 5);
                break;
        }
    }
    private void Dash()
    {
        AtackKD += Time.deltaTime;
        AtackTime += Time.deltaTime;

        anim.SetBool("Dash", true);
        transform.position = Vector2.MoveTowards(transform.position, Playerpos, 28 * Time.deltaTime);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, 1, PlayerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerStats>().TakePhisDamage(stat.Damage / 10);

            int PoisonRandom = UnityEngine.Random.Range(0, 5);
            if (PoisonBuffTime > 0 && PoisonRandom == 2)
            {
                player.GetComponent<PlayerBuffsAndDebuffs>().TakeEffect(1);
            }
        }
        if (Vector2.Distance(transform.position, Playerpos) < 0.1)
        {
            DashActive = false;
            anim.SetBool("Dash", false);
            stat.MicroStan = true;
            transform.position = Playerpos;
        }
    }
}
