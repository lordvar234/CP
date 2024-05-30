using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillsStatInfo : MonoBehaviour
{
    public TextMeshProUGUI Damage;
    public int id;

    public void Update()
    {
        PlagueRobber plag = GameObject.FindGameObjectWithTag("Player").GetComponent<PlagueRobber>();
        switch (id)
        {
            case 1:
                Damage.text = "" + plag.FlintlockDamage;
                break;
            case 2:
                Damage.text = "" + plag.PlagueBombDamage;
                break;
        }
    }
}
