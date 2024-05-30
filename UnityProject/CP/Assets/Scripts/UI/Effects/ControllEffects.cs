using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class ControllEffects : MonoBehaviour
{
    public int effectid, effectKD;

    public int LastStuck, stucks;
    public int RemoveID;
    public int ArmorEfectStatID, MoveSpeedEffectStatID, GradualDamaageStatID;

    public TextMeshProUGUI StuckText;
    public Image KDbar;
    public List<float> effectstuckKD = new List<float>();

    public void Start()
    {
        TakeStuck();
    }
    public void TakeStuck()
    {
        effectstuckKD.Add(effectKD);
        stucks = effectstuckKD.Count;
        if (effectstuckKD.Count > 1)
        {
            StuckText.text = "" + stucks;
        }
        else
        {
            StuckText.text = "";
        }
        LastStuck = stucks - 1;
    }
    public void Update()
    {

        for(int i = 0 ; i < stucks; i++)
        {

            if (effectstuckKD[i] <= 0)
            {
                LastStuck = stucks - 1;
                if (LastStuck < 0)
                {
                    EndEffect();
                }
                effectstuckKD.RemoveAt(i);


                if (stucks > 1)
                {
                    StuckText.text = "" + stucks;
                }
                else
                {
                    StuckText.text = "";
                }
            }
            else
            {
                effectstuckKD[i] -= Time.deltaTime;
                KDbar.fillAmount = effectstuckKD[LastStuck] / effectKD;
            }
        }
    }
    public void EndEffect()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        switch (effectid)
        {
            case 0:
                Player.GetComponent<PlayerBuffsAndDebuffs>().CursedArmor = false;
                break;
            case 1:
                Player.GetComponent<PlayerBuffsAndDebuffs>().Poison = false;
                break;
        }
        Player.GetComponent<PlayerBuffsAndDebuffs>().RemoveEffect(RemoveID);
        Destroy(gameObject);
    }
}
