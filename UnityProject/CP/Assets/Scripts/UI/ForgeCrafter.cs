using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeCrafter : MonoBehaviour
{
    public int id;
    public int firstItem, secondItem;
    public void Forge()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        switch (id)
        {
            case 7:
                 for(int i = 0; firstItem == -1 || i < 36; i++)
                {
                    if (Player.GetComponent<Innventory>().items[i].id == 1)
                    {
                        firstItem = i;                    }
                }
                 if(secondItem != 0)
                {
                    for (int i = 0; secondItem == -1 || i < 36; i++)
                    {
                        if (Player.GetComponent<Innventory>().items[i].id == 2)
                        {
                            secondItem = i;
                        }
                    }
                }
                 if (secondItem != -1 &&  firstItem != -1 && Player.GetComponent<PlayerStats>().CurrentGold >= 600)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-600);
                    Player.GetComponent<Innventory>().RemoveItem(firstItem);
                    Player.GetComponent<Innventory>().RemoveItem(secondItem);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                
                break;
            case 8:
                for (int i = 0; firstItem == -1 || i < 36; i++)
                {
                    if (Player.GetComponent<Innventory>().items[i].id == 1)
                    {
                        firstItem = i;
                    }
                }
                if (secondItem != 0)
                {
                    for (int i = 0; secondItem == -1 || i < 36; i++)
                    {
                        if (Player.GetComponent<Innventory>().items[i].id == 4)
                        {
                            secondItem = i;
                        }
                    }
                }
                if (secondItem != -1 && firstItem != -1 && Player.GetComponent<PlayerStats>().CurrentGold >= 990)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-990);
                    Player.GetComponent<Innventory>().RemoveItem(firstItem);
                    Player.GetComponent<Innventory>().RemoveItem(secondItem);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }

                break;
            case 9:
                for (int i = 0; firstItem == -1 || i < 36; i++)
                {
                    if (Player.GetComponent<Innventory>().items[i].id == 3)
                    {
                        firstItem = i;
                    }
                }
                if (secondItem != 0)
                {
                    for (int i = 0; secondItem == -1 || i < 36; i++)
                    {
                        if (Player.GetComponent<Innventory>().items[i].id == 4)
                        {
                            secondItem = i;
                        }
                    }
                }
                if (secondItem != -1 && firstItem != -1 && Player.GetComponent<PlayerStats>().CurrentGold >= 775)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-775);
                    Player.GetComponent<Innventory>().RemoveItem(firstItem);
                    Player.GetComponent<Innventory>().RemoveItem(secondItem);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                break;
            case 10:
                for (int i = 0; firstItem == -1 || i < 36; i++)
                {
                    if (Player.GetComponent<Innventory>().items[i].id == 3)
                    {
                        firstItem = i;
                    }
                }
                if (secondItem != 0)
                {
                    for (int i = 0; secondItem == -1 || i < 36; i++)
                    {
                        if (Player.GetComponent<Innventory>().items[i].id == 6)
                        {
                            secondItem = i;
                        }
                    }
                }
                if (secondItem != -1 && firstItem != -1 && Player.GetComponent<PlayerStats>().CurrentGold >= 795)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-795);
                    Player.GetComponent<Innventory>().RemoveItem(firstItem);
                    Player.GetComponent<Innventory>().RemoveItem(secondItem);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                break;
            case 11:
                for (int i = 0; firstItem == -1 || i < 36; i++)
                {
                    if (Player.GetComponent<Innventory>().items[i].id == 5)
                    {
                        firstItem = i;
                    }
                }

                if (firstItem != -1 && Player.GetComponent<PlayerStats>().CurrentGold >= 1500)
                {
                    Player.GetComponent<PlayerStats>().TakeCoin(-1500);
                    Player.GetComponent<Innventory>().RemoveItem(firstItem);
                    Player.GetComponent<Innventory>().TakeItem(id);
                }
                break;
        }
        firstItem = -1;
        secondItem = -1;
    }
}
