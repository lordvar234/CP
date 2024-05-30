using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Unity.Burst.Intrinsics;

public class Warlock : MonoBehaviour
{
    public Animator anim;

    Transform playerpos;
    NavMeshAgent agent;
    public float scale;

    public MobsStats stat;

    public float AtackKD, CursedBallRange;
    public double AtackTime;

    public bool CursedBall;
    public GameObject CursedBallOb;

    public float CurseAuraTime;
    public float CursedAuraKd;

    public LayerMask PlayerLayers;
    void Start()
    {
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        AtackTime -= Time.deltaTime;
        AtackKD -= Time.deltaTime;
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;

        if (stat.MicroStan == true)
        {
            AtackKD = 1;
            stat.MicroStan = false;
            CursedBall = false;
        }

        flip();

        if (Math.Abs(transform.position.x - playerpos.position.x) <= CursedBallRange && Math.Abs(transform.position.y - playerpos.position.y) <= CursedBallRange && AtackKD <= 0)
        {
            int AtackRandom = UnityEngine.Random.Range(1, 5);
            if (AtackRandom == 2 && stat.Mana >= 80)
            {
                CursedBallPreparation();
            }
            else if(AtackRandom == 3 && stat.Mana >= 22)
            {
                anim.SetTrigger("CursedAuraPreparation");
                CurseAuraTime = 12;
                AtackTime = 0.9;
                AtackKD = 13;
            }
            else
            {
                AtackKD = 1;
            }
        }



        if (CursedBall == true && AtackTime <= 0)
        {
            Instantiate(CursedBallOb, transform.position, transform.rotation);
            CursedBall = false;
        }
        else if(AtackTime <= 0 && CurseAuraTime > 0 && stat.Mana >= 22)
        {
            CursedAura();
        }
        else 
        {
            anim.SetBool("CursedAura", false);
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
    public void CursedBallPreparation()
    {
        stat.Mana -= 80;
        anim.SetTrigger("CursedBall");
        CursedBall = true;
        AtackTime = 0.5;
        AtackKD = UnityEngine.Random.Range(2, 5);
    }
    public void CursedAura()
    {
        anim.SetBool("CursedAura", true);
        CurseAuraTime -= Time.deltaTime;
        stat.Mana -= 22 * Time.deltaTime;

        if (stat.MicroStan)
        {
            CurseAuraTime = 0;
            stat.MicroStan = false;
        }

        if(CursedAuraKd <= 0)
        {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, 12, PlayerLayers);
            foreach (Collider2D Player in hitPlayer)
            {
                Player.GetComponent<PlayerBuffsAndDebuffs>().TakeEffect(0);
            }
            CursedAuraKd = 1;
        }
        CursedAuraKd -= Time.deltaTime;
    }
}
