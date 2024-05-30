using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasks : MonoBehaviour
{
    public GameObject Player;
    public bool Mana;
    public bool Heal;

    public void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Heal)
            {
                Player.GetComponent<PlayerStats>().Heals += Player.GetComponent<PlayerStats>().MaxHeals / 100 * Random.Range(10, 30);
            }
            else if(Mana)
            {
                Player.GetComponent<PlayerStats>().Mana += Player.GetComponent<PlayerStats>().MaxMana / 100 * Random.Range(20, 50);
            }
            Destroy(gameObject);
        }
    }
}
