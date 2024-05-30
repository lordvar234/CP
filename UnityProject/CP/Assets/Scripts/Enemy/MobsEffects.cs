using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobsEffects : MonoBehaviour
{
    public DataBase data;

    public LayerMask enemyLayers;

    public List<MobEffects> Effectslist = new List<MobEffects>();
    public List<int> EffectSlotid = new List<int>();

    public GameObject EffectMarkObject, EffectBar;

    public MobsStats stats;

    public int Effectsnumber;

    //Plague(1)
    public float PlagueTime;
    public bool PlagueActive;

    //Slow(4)
    public float SlowTime;
    public bool SlowActive;

    //Fire(6)
    public float FireTime;
    public bool FireActive;

    public GameObject Expl;
    public void Start()
    {
        AddGraphicks();
    }
    public void Update()
    {
        if(PlagueActive)
        {
            Plague();
        }

        if(SlowActive)
        {
            Slow();
        }

        if(FireActive)
        {
            Fire();
        }
    }
    //Действие Эффектов
    public void Plague()
    {
        if(PlagueTime > 0 )
        {
            stats.Heals -= (stats.Heals / 100 + 1) * Time.deltaTime;
            PlagueTime -= Time.deltaTime;
        }
        else 
        {
            AddMark(EffectSlotid[1], data.enemyEffect[0]);
            for (int i = EffectSlotid[1]; i < 15; i++)
            {
                int id = Effectslist[i + 1].id;
                AddMark(i, data.enemyEffect[id]);
            }
            for (int i = 0; i < Effectsnumber; i++)
            {
                if (EffectSlotid[i] > EffectSlotid[1])
                {
                    EffectSlotid[i] -= 1;
                }
            }
            PlagueActive = false;
            EffectSlotid[1] = -1;
        }
    }
    public void Slow()
    {
        if(SlowTime <= 0)
        {
            AddMark(EffectSlotid[4], data.enemyEffect[0]);
            for (int i = EffectSlotid[4]; i < 15; i++)
            {
                int id = Effectslist[i + 1].id;
                AddMark(i, data.enemyEffect[id]);
            }
            for (int i = 0; i < Effectsnumber; i++)
            {
                if (EffectSlotid[i] > EffectSlotid[4])
                {
                    EffectSlotid[i] -= 1;
                }
            }
            SlowActive = false;
            EffectSlotid[4] = -1;
            stats.MoveSpeed = stats.BaseMoveSpeed;
        }
        SlowTime -= Time.deltaTime;
    }
    public void Fire()
    {
        if (PlagueTime > 0)
        {
            PlagueTime = 0;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 5, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                double MagDamage = 30;
                enemy.GetComponent<MobsStats>().TakeMagickDamage(MagDamage);
                enemy.GetComponent<MobsEffects>().TakeFire();

                Instantiate(Expl, transform.position, transform.rotation);
            }
        }

        if (FireTime > 0)
        {
            stats.Heals -= stats.Heals - (2 *Time.deltaTime);
            FireTime -= Time.deltaTime;
        }
        else
        {
            AddMark(EffectSlotid[6], data.enemyEffect[0]);
            for (int i = EffectSlotid[6]; i < 15; i++)
            {
                int id = Effectslist[i + 1].id;
                AddMark(i, data.enemyEffect[id]);
            }
            for (int i = 0; i < Effectsnumber; i++)
            {
                if (EffectSlotid[i] > EffectSlotid[6])
                {
                    EffectSlotid[i] -= 1;
                }
            }
            FireActive = false;
            EffectSlotid[6] = -1;
        }
    }
    //Получение Эффекта
    public void TakePlague()
    {
        PlagueTime = 3;
        for (int i = 0; EffectSlotid[1] == -1; i++)
        {
            if (Effectslist[i].id == 0)
            {
                EffectSlotid[1] = i;
                AddMark(i, data.enemyEffect[1]);
            }
        }
        PlagueActive = true;
    }
    public void TakeSlow()
    {
        SlowTime = 5;
        for (int i = 0; EffectSlotid[4] == -1; i++)
        {
            if (Effectslist[i].id == 0)
            {
                EffectSlotid[4] = i;
                AddMark(i, data.enemyEffect[4]);
            }
        }
        SlowActive = true;
    }
    public void TakeFire()
    {
        FireTime = 5;
        for (int i = 0; EffectSlotid[6] == -1; i++)
        {
            if (Effectslist[i].id == 0)
            {
                EffectSlotid[6] = i;
                AddMark(i, data.enemyEffect[6]);
            }
        }
        FireActive = true;
    }
    //Отображение
    public void AddGraphicks()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject newEffect = Instantiate(EffectMarkObject, EffectBar.transform) as GameObject;

            newEffect.name = i.ToString();

            MobEffects ii = new MobEffects();
            ii.itemGameObj = newEffect;

            RectTransform rt = newEffect.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newEffect.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);


            Effectslist.Add(ii);
        }
    }
    public void AddMark(int id, EnemyEffectsList effect)
    {
        Effectslist[id].id = effect.id;
        Effectslist[id].itemGameObj.GetComponent<Image>().sprite = effect.img;
    }
}
[System.Serializable]
public class MobEffects
{
    public int id;
    public int Stucks;
    public GameObject itemGameObj;
}

