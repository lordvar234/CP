using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explousion : MonoBehaviour
{
    public LayerMask enemyLayers;
    public float LiveTime;
    void Start()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 3, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            PlayerStats Dam = GameObject.FindGameObjectWithTag("Player").GetComponent("PlagueStats") as PlayerStats;
            double MagDamage = 20 * Dam.Lvl;
            enemy.GetComponent<MobsStats>().TakeMagickDamage(MagDamage);
            enemy.GetComponent<MobsEffects>().TakeFire();
        }
    }

    void Update()
    {
        if (LiveTime <= 0)
        {
            Destroy(gameObject);
        }
        LiveTime -= Time.deltaTime;
    }
}
