using UnityEngine;
using UnityEngine.UI;

public class MobsStats : MonoBehaviour
{
    public Animator anim;

    public MobsEffects effect;

    public Image Hpbar, Manabar;
    public double MaxHeals, Heals, HealsRegeneration;
    public double MaxMana, Mana, ManaRegeneration;

    public bool MicroStan;

    public float MoveSpeed, BaseMoveSpeed;
    public double Armor, Defense;
    public double Damage;

    public GameObject HealFlask, ManaFlask;
    public float XpDrop;
    void Start()
    {
        anim = GetComponent<Animator>();
        Heals = MaxHeals;
        Mana = MaxMana;

        if(Armor < 0)
        {
            Defense = Armor * 2.5;
        }
        else if(Armor < 7 && Armor > 0)
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
    }
    void Update()
    {
        if(Mana <= MaxMana && MaxMana != 0)
        {
            Mana += ManaRegeneration * Time.deltaTime;
            Manabar.fillAmount = (float)Mana / (float)MaxMana;
        }
        else
        {
            Mana = MaxMana;
        }

        if(Heals <= MaxHeals)
        {
            Heals += HealsRegeneration * Time.deltaTime;
            Hpbar.fillAmount = (float)Heals / (float)MaxHeals;
        }
        else
        {
            Heals = MaxHeals;
        }
    }
    public void TakePhisDamage(double AttackDamage)
    {
        Heals -= AttackDamage - (AttackDamage / 100 * Defense);
        anim.SetTrigger("TakeDamage");
        Hpbar.fillAmount = (float)Heals / (float)MaxHeals;

        MicroStan = true;

        if (Heals <= 0)
        {
            Dead();
        }
    }
    public void TakeMagickDamage(double MagicDamage)
    {
        Heals -= MagicDamage;
        anim.SetTrigger("TakeDamage");
        Hpbar.fillAmount = (float)Heals / (float)MaxHeals;

        MicroStan = true;

        if (Heals <= 0)
        {
            Dead();
        }
    }
    public void Magnit(float posX, float posY)
    {
        MicroStan = true;

        transform.position = new Vector2(posX, posY);
        effect.TakeSlow();
        MoveSpeed = MoveSpeed - MoveSpeed / 100 * 40;
    }
    public void Dead()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerStats>().TakeXp(XpDrop);
        Player.GetComponent<PlayerStats>().TakeCoin(Random.Range(8, 15));

        float HealsDrop = Random.Range(1, 20);
        float ManaDrop = Random.Range(1, 20);

        if (HealsDrop == 4)
        {
            Instantiate(HealFlask, transform.position, transform.rotation);
        }
        if (ManaDrop == 4)
        {
            Instantiate(ManaFlask, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
