using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyer : MonoBehaviour
{
    public int id;

    public void Buy()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        switch (id)
        {
            case 1:
                if (Player.GetComponent<PlayerStats>().CurrentGold >= 450 && Player.GetComponent<Innventory>().FirstFreeSlot < 38)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-450);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                break;
            case 2:
                if (Player.GetComponent<PlayerStats>().CurrentGold >= 400 && Player.GetComponent<Innventory>().FirstFreeSlot < 38)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-400);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                break;
            case 3:
                if (Player.GetComponent<PlayerStats>().CurrentGold >= 500 && Player.GetComponent<Innventory>().FirstFreeSlot < 38)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-500);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                break;
            case 4:
                if (Player.GetComponent<PlayerStats>().CurrentGold >= 630 && Player.GetComponent<Innventory>().FirstFreeSlot < 38)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-630);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                break;
            case 5:
                if (Player.GetComponent<PlayerStats>().CurrentGold >= 600 && Player.GetComponent<Innventory>().FirstFreeSlot < 38)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-600);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                break;
            case 6:
                if (Player.GetComponent<PlayerStats>().CurrentGold >= 585 && Player.GetComponent<Innventory>().FirstFreeSlot < 38)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-585);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                break;
        }
    }
}
