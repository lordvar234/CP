using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    //Герой
    public PlagueRobber plag;
    //Статичные статы
    //Зависящие от атрибутов
    public double Strenght;
    public double MaxHeals, MagickResist;
    public double MaxHealsItemBonus, MagickResistItemBonus;

    public double Agillity;
    public double Damage, DamageItemBonus;
    public double AttackSpeed, AttackSpeedItemBonus;

    public double Intelect;
    public double MaxMana, ManaRegeneration;
    public double MaxManaItemBonus, ManaRegenerationItemBonus;
    //Не зависящие от атрибутов
    public double MelleeRange;
    public double Armor, ArmorItemBonus;
    public double MoveSpeed, MoveSpeedItemBonus;
    public double MagickDamage, MagickItemBonus;
    public double BaseAttackKD;
    public double HealsRegeneration, HealsRegenerationItemBonus;

    public List<SkillsCost> SkillCost = new List<SkillsCost>();

    //Динамичные статы
    public double Heals, Mana;
    public double Defense, AttackKd;
    public float HealsProcent, ManaProcent;

    //Buff DeBuffList
    public List<double> MoveSpeedModifiers = new List<double>();
    public List<double> ArmorModifiers = new List<double>();

    public List<double> GradualDamage = new List<double>();

    //Lvl and Money
    public int CurrentGold;
    public float Lvl, Xp, LvlUpXp;

    public TextMeshProUGUI Level, CurrentXp;
    public Image XpBar;

    //Интерфейс
    public TextMeshProUGUI CurrentHeals, CurrentMana;
    public TextMeshProUGUI CurrentHealsRegen, CurrentManaRegen;
    public TextMeshProUGUI Gold;

    public TextMeshProUGUI DamageStat, AtackSpeedStat, ArmorStat, MagResStat, MoveSpeedStat;

    public Image HealsBar, ManaBar;
    public void Start()
    {
        StatsUpdate();
    }
    public void Update()
    {
        ManaUpdate();
        HealsUpdate();
    }
    private void ManaUpdate()
    {
        ManaProcent = (float)Mana / (float)MaxMana;
        CurrentMana.text = (int)Mana + "/" + (int)MaxMana;
        ManaBar.fillAmount = ManaProcent;

        if (Mana < MaxMana)
        {
            Mana += ManaRegeneration * Time.deltaTime;
        }
        else
        {
            Mana = MaxMana;
        }
    }
    private void HealsUpdate()
    {
        for(int id = 0; id < GradualDamage.Count; id++)
        {
            Heals -= GradualDamage[id] * Time.deltaTime;
        }

        HealsProcent = (float)Heals / (float)MaxHeals;
        CurrentHeals.text = (int)Heals + "/" + (int)MaxHeals;
        HealsBar.fillAmount = HealsProcent;

        if (Heals <= 0)
        {
            Dead();
        }
        else if (Heals < MaxHeals)
        {
            Heals += HealsRegeneration * Time.deltaTime;
        }
        else
        {
            Heals = MaxHeals;
        }
    }
    public void TakeXp(float Exp)
    {
        Xp += Exp;

        for (; Xp >= LvlUpXp;)
        {
            Lvl++;
            Level.text = "" + Lvl;
            Xp -= LvlUpXp;
            LvlUpXp = (250 + 250 * (Lvl - 1)) * Lvl;
            StatsUpdate();
            plag.SkillStatUpdate();
        }

        XpBar.fillAmount = Xp / LvlUpXp;
        CurrentXp.text = (int)Xp + "/" + (int)LvlUpXp;
    }
    public void TakeCoin(int Coin)
    {
        CurrentGold += Coin;
        Gold.text = "" + CurrentGold;
    }
    public void StatsUpdate()
    {
        //Зависит от Атрибутов
        MaxHeals = 80 + Strenght * 16 * (Lvl - 1) + MaxHealsItemBonus;
        HealsRegeneration = 0.2 * Strenght * (Lvl - 1) + HealsRegenerationItemBonus;

        Damage = 15 + Agillity * 4 * (Lvl - 1) + DamageItemBonus;
        AttackSpeed = 15 + Agillity * (Lvl - 1) + AttackSpeedItemBonus;

        MaxMana = 50 + Intelect * 12 * (Lvl - 1) + MaxManaItemBonus;
        ManaRegeneration = 1.5 + Intelect * 0.3 * (Lvl - 1) + ManaRegenerationItemBonus;

        Armor = ArmorItemBonus;
        MagickDamage = 1 + MagickItemBonus;
        MagickResist = MagickResistItemBonus;

        AttackKd = BaseAttackKD / (1 + AttackSpeed / 120);

        for (int id = 0; id < ArmorModifiers.Count; id++)
        {
            MoveSpeed -= ArmorModifiers[id];
        }
        if (Armor < 0)
        {
            Defense = Armor * 2.5;
        }
        else if (Armor < 7 && Armor > 0)
        {
            Defense = Armor * 5 - (Armor - 1) * 2;
        }
        else if (Armor >= 7 && Armor < 97)
        {
            Defense = 23 + ((Armor - 7) * (1.2 - 0.005 * (Armor - 8)));
        }
        else if (Armor >= 97)
        {
            Defense = 90;
        }

        MoveSpeed = 12 + MoveSpeedItemBonus;
        for(int id = 0; id < MoveSpeedModifiers.Count; id++)
        {
            MoveSpeed -= MoveSpeed / 100 * MoveSpeedModifiers[id];
        }
        if (MoveSpeed < 0)
        {
            MoveSpeed = 0;
        }

        Heals = MaxHeals * HealsProcent;
        Mana = MaxMana * ManaProcent;

        CurrentHealsRegen.text = "+" + (float)HealsRegeneration;
        CurrentManaRegen.text = "+" + (float)ManaRegeneration;

        double atackkd = Math.Round(AttackKd, 2);
        DamageStat.text = "" + Damage;
        AtackSpeedStat.text = AttackSpeed + "(" + atackkd + "S)";
        ArmorStat.text = Armor + "(" + Math.Round(Defense, 2) + "%)";
        MagResStat.text = MagickResist + "%";
        MoveSpeedStat.text = MoveSpeed + "";
    }
    public void TakePhisDamage(double dam)
    {
        Heals -= dam - dam / 100 * Defense;
    }
    public void TakeMagickDamage(double dam)
    {
        Heals -= dam - dam / 100 * MagickResist;
    }
    public void Dead()
    {
        SceneManager.LoadScene(0);
    }
}
[System.Serializable]
public class SkillsCost
{
    public int id;
    public float ManaCost;
    public float Kd;
}
