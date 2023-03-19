using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class heromove : MonoBehaviour // - Вместо «PlayerMove» должно быть имя файла скрипта
{
    //------- Функция/метод, выполняемая при запуске игры ---------

    public int maxHealth = 100;
    public int currentHealth;

    public Rigidbody2D rb;
    public Animator anim;
    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //-v- Для автоматического присваивания в переменную, радиуса коллайдера объекта «GroundCheck»
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }
    //------- Функция/метод, выполняемая каждый кадр в игре ---------
    void Update()
    {
        Walk();
        Reflect();
        Jump();
        CheckingGround();
        Dash();
        Somersault();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Atack();
        }
    }
    //------- Функция/метод для перемещения персонажа по горизонтали ---------
    public Vector2 moveVector;
    public int speed = 3;
    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        anim.SetFloat("MoveX", Mathf.Abs(moveVector.x));

    }
    //------- Функция/метод для отражения персонажа по горизонтали ---------
    public bool faceRight = true;
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }
    //------- Функция/метод для прыжка ---------
    public float jumpForce = 10f;
    private int jumpCount = 0;
    public int maxJumpValue = 2;
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (onGround || (++jumpCount < maxJumpValue)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (onGround) { jumpCount = 0; }

    }

    //------- Функция/метод для обнаружения земли ---------
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }
    //-----------------------------------------------------------------

    //------- Функция/метод для рывка ---------
    public int dashForce = 1000;
    public float dashCooldown = 1f; // время перезарядки в секундах
    private float lastDashTime = -Mathf.Infinity; // время последнего использования способности

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            anim.StopPlayback();
            anim.Play("dash");

            rb.velocity = Vector2.zero;

            if (!faceRight)
            {
                rb.AddForce(Vector2.left * dashForce);
            }
            else
            {
                rb.AddForce(Vector2.right * dashForce);
            }

            lastDashTime = Time.time; // устанавливаем время последнего использования способности
        }
    }
    //------- Функция/метод для кувырка ---------
    public int SomersaultForce = 1000;
    public float SomersaultCooldown = 1f; // время перезарядки в секундах
    private float lastSomersaultTime = -Mathf.Infinity; // время последнего использования способности

    void Somersault()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time > lastSomersaultTime + SomersaultCooldown)
        {
            anim.StopPlayback();
            anim.Play("Somersault");

            rb.velocity = Vector2.zero;

            if (!faceRight)
            {
                rb.AddForce(Vector2.left * SomersaultForce);
            }
            else
            {
                rb.AddForce(Vector2.right * SomersaultForce);
            }

            lastSomersaultTime = Time.time; // устанавливаем время последнего использования способности
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("Персонаж метв!");
        }
    }

    //------- Функция/метод для атаки ---------
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 10;
    
    void Atack()
    {
        anim.SetTrigger("Atack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
