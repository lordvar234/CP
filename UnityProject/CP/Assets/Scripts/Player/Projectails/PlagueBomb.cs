using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueBomb : MonoBehaviour
{
    public LayerMask enemyLayers;
    [SerializeField] private Camera mainCamera;

    public GameObject ExplousionParticle;
    public GameObject Explosion;

    Vector3 TargetPos;
    public void Start()
    {
        mainCamera = Camera.main;
        TargetPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    void Update()
    { 
        if (Vector2.Distance(transform.position, TargetPos) < 0.1)
        {
            Explousion();
        }
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, 28 * Time.deltaTime);

    }
    public void Explousion()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 3, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            PlagueRobber Dam = GameObject.FindGameObjectWithTag("Player").GetComponent("PlagueRobber") as PlagueRobber;
            enemy.GetComponent<MobsStats>().TakeMagickDamage(Dam.PlagueBombDamage);
            enemy.GetComponent<MobsEffects>().TakePlague();
        }

        Instantiate(ExplousionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 5, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                PlayerStats Dam = GameObject.FindGameObjectWithTag("Player").GetComponent("PlagueStats") as PlayerStats;
                double MagDamage = 20 * Dam.Lvl;
                enemy.GetComponent<MobsStats>().TakeMagickDamage(MagDamage);
                enemy.GetComponent<MobsEffects>().TakeFire();
            }

            Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
            Destroy(gameObject);
        }
    }
}
