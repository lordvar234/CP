using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPositionSimulation : MonoBehaviour
{
    Transform playerpos;
    public void Start()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Update()
    {
        if(playerpos.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2); 
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}