using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedBall : MonoBehaviour
{
    public Vector3 Playerpos;
    public LayerMask PlayerMask, EnemyMask;
    public GameObject ExplousionParticle;
    void Start()
    {
        Playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Playerpos, 28 * Time.deltaTime);
        if (Vector2.Distance(transform.position, Playerpos) < 0.1)
        {
            Explousion();
        }
    }
    public void Explousion()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, 1, PlayerMask);
        foreach (Collider2D Player in hitPlayer)
        {
            Player.GetComponent<PlayerStats>().TakeMagickDamage(20);
        }
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(transform.position, 1, EnemyMask);
        foreach (Collider2D Enemy in hitEnemy)
        {
            Enemy.GetComponent<MobsStats>().Heals += 50;
        }
        Instantiate(ExplousionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
