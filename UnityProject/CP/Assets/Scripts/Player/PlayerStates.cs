using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    private Animator anim;

    public PlayerStats stat;

    public float RotScale;
    //Бег
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    private double moveFalse;

    
    //АвтоАтака
    public LayerMask enemyLayers;

    public float Combo1, Combo2;
    public float NowAtackKD;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && moveFalse <= 0 && stat.MoveSpeed != 0)
        {
            Move();
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
            moveFalse -= Time.deltaTime;
        }



        if (Input.GetMouseButton(0) && NowAtackKD <= 0)
        {
            Atack();
        }
        NowAtackKD -= Time.deltaTime;
        Combo1 -= Time.deltaTime;
        Combo2 -= Time.deltaTime;
    }
    public void Atack()
    {
        moveFalse = 0.4;

        if (Combo2 > 0)
        {
            anim.speed = 1 + (float)stat.AttackSpeed / 100 + 0f;
            anim.SetFloat("Combo", 2);
            anim.SetTrigger("Atack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, (float)stat.MelleeRange * (float)1.3, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<MobsStats>().TakePhisDamage(stat.Damage);
            }
            NowAtackKD = (float)stat.AttackKd;

            anim.speed = 1f;
            Combo1 = 0;
            Combo2 = 0;
        }
        else if (Combo1 > 0 && Combo2 <= 0)
        {
            anim.speed = 1 + (float)stat.AttackSpeed / 100 + 0f;
            anim.SetFloat("Combo", 1);
            anim.SetTrigger("Atack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, (float)stat.MelleeRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<MobsStats>().TakePhisDamage(stat.Damage);
            }
            NowAtackKD = (float)stat.AttackKd;

            anim.speed = 1f;
            Combo1 = 0;
            Combo2 = 2;
        }
        else
        {
            anim.speed = 1 + (float)stat.AttackSpeed / 100 + 0f;
            anim.SetFloat("Combo", 0);
            anim.SetTrigger("Atack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, (float)stat.MelleeRange * (float)0.6, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<MobsStats>().TakePhisDamage(stat.Damage);
            }
            NowAtackKD = (float)stat.AttackKd;

            anim.speed = 1f;
            Combo1 = 2;
            Combo2 = 0;
        }

    }
    public void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(moveInput.x > 0)
        {
            RotScale = 1;
        }
        else if (moveInput.x < 0)
        {
            
            RotScale = -1;
        }
        
        transform.localScale = new Vector3(1 * RotScale, transform.localScale.y, transform.localScale.z);
        moveVelocity = moveInput.normalized * (float)stat.MoveSpeed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
