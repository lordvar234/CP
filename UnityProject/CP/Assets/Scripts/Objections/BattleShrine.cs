using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class BattleShrine : MonoBehaviour
{
    public int Value;
    public GameObject Skelleton, Warlock, Rogue;

    float InputKD;

    Transform playerpos;
    public GameObject PressText;
    
    void Start()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (Math.Abs(transform.position.x - playerpos.position.x) <= 3 && Math.Abs(transform.position.y - playerpos.position.y) <= 3 && InputKD <= 0)
        {
            PressText.SetActive(true);
            if(Input.GetKey(KeyCode.F))
            {
                BattleActive();
            }
        }
        else
        {
            InputKD -= Time.deltaTime;  
            PressText.SetActive(false);
        }
    }
    public void BattleActive()
    {
        for (int i = 0; i < Value; i++)
        {
            int Type = UnityEngine.Random.Range(0, 0);
            switch (Type)
            {
                case 2:
                    Instantiate(Warlock, new Vector2(transform.position.x + UnityEngine.Random.Range(-8, 8), transform.position.y + UnityEngine.Random.Range(-8, 8)), transform.rotation);
                    i += 3;
                    break;
                case 3:
                    Instantiate(Rogue, new Vector2(transform.position.x + UnityEngine.Random.Range(-8, 8), transform.position.y + UnityEngine.Random.Range(-8, 8)), transform.rotation);
                    i += 3;
                    break;
            }           
            Instantiate(Skelleton, new Vector2(transform.position.x + UnityEngine.Random.Range(-5, 5), transform.position.y + UnityEngine.Random.Range(-8, 8)), transform.rotation);

        }
        Value += 1;
        InputKD = 5;
    }
}
