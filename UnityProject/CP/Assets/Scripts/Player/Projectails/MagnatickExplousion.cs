using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnatickExplousion : MonoBehaviour
{
    public float LiveTime;
    public LayerMask enemyLayers;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 3, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<MobsStats>().Magnit(transform.position.x, transform.position.y);
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
