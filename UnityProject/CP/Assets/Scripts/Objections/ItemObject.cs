using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public int Itemid;
    public GameObject Player;

    public void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Player.GetComponent<Innventory>().FirstFreeSlot < 38)
        {
            Player.GetComponent<Innventory>().TakeItem(Itemid);
            Destroy(gameObject);
        }
    }
}
