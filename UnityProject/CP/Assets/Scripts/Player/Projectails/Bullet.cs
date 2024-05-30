using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask enemyLayers;
    public float DamageRange;

    public float LifeTime;
    void Update()
    {
        transform.Translate(Vector3.right * 80 * Time.deltaTime);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, DamageRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            PlagueRobber Dam = GameObject.FindGameObjectWithTag("Player").GetComponent("PlagueRobber") as PlagueRobber;
            enemy.GetComponent<MobsStats>().TakeMagickDamage(Dam.FlintlockDamage);
            Destroy(gameObject);
        }

        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
