using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueRobber : MonoBehaviour
{
    public PlayerStats stat;
    public GameObject RotPos;

    [SerializeField] private Camera mainCamera;

    //Skill Damage
    public double FlintlockDamage, PlagueBombDamage;

    //Объекты
    public GameObject Bullet, Bomb, MagnatickExplousion;
    public GameObject DashEffect;

    public double DashTime;
    Vector3 DashPos;
    public void Start()
    {
        mainCamera = Camera.main;
        SkillStatUpdate();
    }
    public void Update()
    {
        if (DashTime <= 0)
        {
            stat.MoveSpeed = 12 + stat.MoveSpeedItemBonus;
            DashEffect.SetActive(false);
        }
        DashTime -= Time.deltaTime;
    }
    public void SkillStatUpdate()
    {
        FlintlockDamage = (42 + 16 * (stat.Lvl - 1)) * stat.MagickDamage;
        PlagueBombDamage = (25 + 12 * (stat.Lvl - 1)) * stat.MagickDamage;
    }
    public void FlintlockShot()
    {
        Instantiate(Bullet, RotPos.transform.position, RotPos.transform.rotation);
    }
    public void PlagueDash()
    {
        DashEffect.SetActive(true);
        stat.MoveSpeed = (12 + stat.MoveSpeedItemBonus) * 1.6;
        DashTime = 1.2;
    }
    public void MagnaticExploision()
    {
        Instantiate(MagnatickExplousion, mainCamera.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
    }
    public void PlagueBomb()
    {
        Instantiate(Bomb, RotPos.transform.position, RotPos.transform.rotation);
    }
}
