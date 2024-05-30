using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject Player;
    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Seleceted()
    {
    }
}
