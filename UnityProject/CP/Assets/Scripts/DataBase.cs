using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public List<ItemsList> item = new List<ItemsList>();
    public List<PlayerEffectsList> playerEffects = new List<PlayerEffectsList>();
    public List<EnemyEffectsList> enemyEffect = new List<EnemyEffectsList>();
    public List<EnemyList> enemy = new List<EnemyList>();
    public List<PlayerSkill> skill = new List<PlayerSkill>();
    public List<EnemyProjectailList> enemyProjectails = new List<EnemyProjectailList>();
    public List<PlayerProjectailList> PlayerProjectails = new List<PlayerProjectailList>();
}
[System.Serializable]
public class ItemsList
{
    public int id;
    public string name;
    public Sprite img;

    //Stats
    public double MaxHeals, Armor, HealsRegeneration;
    public double Damage, AttackSpeed;
    public double MaxMana, ManaRegeneration;
    public double MoveSpeed;
}
[System.Serializable]
public class PlayerEffectsList
{
    public int id;
    public string name;
    public GameObject effect;
}
[System.Serializable]
public class EnemyEffectsList
{
    public int id;
    public string name;
    public Sprite img;
}
[System.Serializable]
public class EnemyList
{
    public int id;
    public string name;
    public GameObject mob;
}
[System.Serializable]
public class PlayerSkill
{
    public int id;
    public string name;
    public Sprite Img;
    public GameObject SkillInfo;
}
[System.Serializable]
public class EnemyProjectailList
{
    public int id;
    public string name;
    public GameObject mob;
}
[System.Serializable]
public class PlayerProjectailList
{
    public int id;
    public string name;
    public GameObject mob;
}
