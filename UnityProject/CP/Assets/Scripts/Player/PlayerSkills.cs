using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    public DataBase data;

    public PlayerStats Stat;
    public PlagueRobber Robber;

    public int firsfreeSkillSlot;

    public GameObject  SkillHud, SkillKdHud, SkillButtonHud, SkillManaHud;
    public GameObject SkillSlot, SkillKD, SkillButton, SkillMana;

    public List<SkillsButtons> Skillbutton = new List<SkillsButtons>();
    public List<PlayerCurrentSkill> PlayerSkill = new List<PlayerCurrentSkill>();
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            AddSkill(i);
            SkillSelect(i, data.skill[i]);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && PlayerSkill[0].Kd <= 0 && Stat.Mana >= PlayerSkill[0].ManaCost)
        {
            Stat.Mana -= PlayerSkill[0].ManaCost;
            PlayerSkill[0].Kd = PlayerSkill[0].KD;
            SkillUse(0);
        }
        PlayerSkill[0].Kd -= Time.deltaTime;
        PlayerSkill[0].SkillKD.GetComponent<Image>().fillAmount = PlayerSkill[0].Kd / PlayerSkill[0].KD;

        if (Input.GetKey(KeyCode.LeftShift) && PlayerSkill[1].Kd <= 0 && Stat.Mana >= PlayerSkill[1].ManaCost)
        {
            Stat.Mana -= PlayerSkill[1].ManaCost;
            PlayerSkill[1].Kd = PlayerSkill[1].KD;
            SkillUse(1);
        }
        PlayerSkill[1].Kd -= Time.deltaTime;
        PlayerSkill[1].SkillKD.GetComponent<Image>().fillAmount = PlayerSkill[1].Kd / PlayerSkill[1].KD;

        if (Input.GetKey(KeyCode.E) && PlayerSkill[2].Kd <= 0 && Stat.Mana >= PlayerSkill[2].ManaCost)
        {
            Stat.Mana -= PlayerSkill[2].ManaCost;
            PlayerSkill[2].Kd = PlayerSkill[2].KD;
            SkillUse(2);
        }
        PlayerSkill[2].Kd -= Time.deltaTime;
        PlayerSkill[2].SkillKD.GetComponent<Image>().fillAmount = PlayerSkill[2].Kd / PlayerSkill[2].KD;

        if (Input.GetKey(KeyCode.Q) && PlayerSkill[3].Kd <= 0 && Stat.Mana >= PlayerSkill[3].ManaCost)
        {
            Stat.Mana -= PlayerSkill[3].ManaCost;
            PlayerSkill[3].Kd = PlayerSkill[3].KD;
            SkillUse(3);
        }
        PlayerSkill[3].Kd -= Time.deltaTime;
        PlayerSkill[3].SkillKD.GetComponent<Image>().fillAmount = PlayerSkill[3].Kd / PlayerSkill[3].KD;
    }
    public void SkillUse(int CurrentSkill)
    {
        switch (PlayerSkill[CurrentSkill].id)
        {
            case 0:
                Robber.FlintlockShot();
                break;
            case 1:
                Robber.PlagueDash();
                break;
            case 2:
                Robber.MagnaticExploision();
                break;
            case 3:
                Robber.PlagueBomb();
                break;
        }
    }
    public void AddSkill(int i)
    {
        GameObject newSkill = Instantiate(SkillSlot, SkillHud.transform) as GameObject;
        GameObject newSkillKD = Instantiate(SkillKD, SkillKdHud.transform) as GameObject;
        GameObject newSkillButton = Instantiate(SkillButton, SkillButtonHud.transform) as GameObject;
        GameObject newSkillMana = Instantiate(SkillMana, SkillManaHud.transform) as GameObject;

        newSkill.name = i.ToString();
        newSkillKD.name = i.ToString();
        newSkillButton.name = i.ToString();
        newSkillMana.name = i.ToString();

        PlayerCurrentSkill ii = new PlayerCurrentSkill();

        ii.Skill = newSkill;
        ii.SkillKD = newSkillKD;
        ii.SkillButton = newSkillButton;
        ii.ManaText = newSkillMana;

        RectTransform rt = newSkill.GetComponent<RectTransform>();
        RectTransform rtk = newSkillKD.GetComponent<RectTransform>();
        RectTransform rtb = newSkillButton.GetComponent<RectTransform>();

        rt.localPosition = new Vector3(0, 0, 0);
        rt.localScale = new Vector3(1, 1, 1);
        newSkill.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

        rtk.localPosition = new Vector3(0, 0, 0);
        rtk.localScale = new Vector3(1, 1, 1);
        newSkillKD.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

        rtb.localPosition = new Vector3(0, 0, 0);
        rtb.localScale = new Vector3(1, 1, 1);
        newSkillButton.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);


        PlayerSkill.Add(ii);
    }
    public void SkillSelect(int id, PlayerSkill skil)
    {
        PlayerSkill[id].SkillButton.GetComponent<Image>().sprite = Skillbutton[firsfreeSkillSlot].img;

        PlayerSkill[id].id = skil.id;
        PlayerSkill[id].Skill.GetComponent<Image>().sprite = skil.Img;

        GameObject SkillInformatin = Instantiate(skil.SkillInfo, PlayerSkill[id].Skill.transform);

        RectTransform rt = SkillInformatin.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(-100, 170, -5);
        rt.localScale = new Vector3(1, 1, 1);
        SkillInformatin.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

        PlayerSkill[id].Skill.GetComponent<InfoScreen>().info = SkillInformatin;

        PlayerSkill[id].ManaCost = Stat.SkillCost[id].ManaCost;
        PlayerSkill[id].ManaText.GetComponent<TextMeshProUGUI>().text = "" + PlayerSkill[id].ManaCost;

        PlayerSkill[id].KD = Stat.SkillCost[id].Kd;

        firsfreeSkillSlot += 1;
    }
}
[System.Serializable]
public class PlayerCurrentSkill
{
    public int id;
    public GameObject Skill;
    public GameObject SkillKD;
    public GameObject SkillButton;
    public GameObject ManaText;
    public float Kd;
    public float KD;
    public float ManaCost;
}
[System.Serializable]
public class SkillsButtons
{
    public int id;
    public Sprite img;
}