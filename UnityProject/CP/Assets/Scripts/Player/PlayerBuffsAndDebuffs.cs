using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffsAndDebuffs : MonoBehaviour
{
    public DataBase data;

    public List<Effects> Effect = new List<Effects>();

    public GameObject EffectBar;

    //Эффекты Контроля
    public bool CursedArmor;
    public bool Poison;

    public void TakeEffect(int id)
    {
        switch(id)
        {
            case 0:
                if (CursedArmor)
                {
                    for(int i = 0; i < Effect.Count; i++)
                    {
                        if(Effect[i].id == id)
                        {
                            Effect[i].effect.GetComponent<ControllEffects>().TakeStuck();
                        }
                    }
                }
                else
                {
                    CursedArmor = true;
                    GameObject newEffect = Instantiate(data.playerEffects[id].effect, EffectBar.transform) as GameObject;
                    newEffect.name = "CuresedArmor";

                    newEffect.GetComponent<ControllEffects>().RemoveID = Effect.Count - 1;

                    Effects ef = new Effects();

                    ef.name = newEffect.name;
                    ef.id = Effect.Count;
                    ef.effect = newEffect;

                    GameObject Player = GameObject.FindGameObjectWithTag("Player");

                    Player.GetComponent<PlayerStats>().ArmorModifiers.Add(2);
                    Player.GetComponent<PlayerStats>().MoveSpeedModifiers.Add(10);
                    Player.GetComponent<PlayerStats>().StatsUpdate();

                    newEffect.GetComponent<ControllEffects>().ArmorEfectStatID = Player.GetComponent<PlayerStats>().ArmorModifiers.Count - 1;
                    newEffect.GetComponent<ControllEffects>().MoveSpeedEffectStatID = Player.GetComponent<PlayerStats>().MoveSpeedModifiers.Count - 1;

                    Effect.Add(ef);
                }
                break;
            case 1:
                if (Poison)
                {
                    for (int i = 0; i < Effect.Count; i++)
                    {
                        if (Effect[i].id == id)
                        {
                            Effect[i].effect.GetComponent<ControllEffects>().TakeStuck();
                        }
                    }
                }
                else
                {
                    Poison = true;
                    GameObject newEffect = Instantiate(data.playerEffects[id].effect, EffectBar.transform) as GameObject;
                    newEffect.name = "Poison";

                    newEffect.GetComponent<ControllEffects>().RemoveID = Effect.Count - 1;

                    Effects ef = new Effects();

                    ef.name = newEffect.name;
                    ef.id = Effect.Count;
                    ef.effect = newEffect;

                    GameObject Player = GameObject.FindGameObjectWithTag("Player");

                    Player.GetComponent<PlayerStats>().GradualDamage.Add(1);

                    newEffect.GetComponent<ControllEffects>().GradualDamaageStatID = Player.GetComponent<PlayerStats>().GradualDamage.Count - 1;

                    Effect.Add(ef);
                }
                break;
        }
    }
    public void RemoveEffect(int id)
    {
        Effect.Remove(Effect[id]);
    }
}
[System.Serializable]
public class Effects
{
    public int id;
    public string name;
    public GameObject effect;
}