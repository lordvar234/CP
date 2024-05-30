using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopKeeper : MonoBehaviour
{
    GameObject playerpos;
    public GameObject PressText, Shop;

    void Start()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Math.Abs(transform.position.x - playerpos.transform.position.x) <= 3 && Math.Abs(transform.position.y - playerpos.transform.position.y) <= 3)
        {
            PressText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenShop();
            }
        }
        else
        {
            PressText.SetActive(false);
            Shop.SetActive(false);
        }
    }
    public void OpenShop()
    {
        Shop.SetActive(!Shop.activeSelf);

    }
}
