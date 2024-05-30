using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRotateTest : MonoBehaviour
{
    public GameObject Rot;
    private Animator anim;
    public double rotateZ;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        rotateZ = Rot.transform.localEulerAngles.z;
        
        if(rotateZ < 180)
        {

            anim.SetBool("Top", true);
            anim.SetBool("Bot", false);
        }
        else
        {
            anim.SetBool("Top", false);
            anim.SetBool("Bot", true);

        }
    }
}
