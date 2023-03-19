using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;

    public float detectionRange = 10f; // расстояние, на котором враг замечает игрока
    public float attackRange = 0.5f; // расстояние, на котором враг может атаковать игрока
    public float movementSpeed = 5f; // скорость передвижения врага
    public int attackDamage = 5; // урон от атаки врага

    private Transform hero; // позиция игрока
    private NavMeshAgent agent; // компонент для передвижения по навмешу


    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        hero = GameObject.FindGameObjectWithTag("Player").transform; // получаем позицию игрока по тегу "Player"
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, hero.position); // вычисляем расстояние до игрока

        if (distance <= detectionRange) // если игрок находится в зоне видимости врага
        {
            Vector3 direction = (hero.position - transform.position).normalized; // вычисляем направление к игроку
            transform.Translate(direction * movementSpeed * Time.deltaTime); // передвигаем врага в направлении игрока

            if (distance <= attackRange) // если игрок находится в зоне атаки врага
            {
                Attack(); // атакуем игрока
            }
        }
    }


    public Transform attackPoint;
    public LayerMask enemyLayers;
    void Attack()
    {
        // в данном случае мы просто выведем сообщение в консоль, но здесь может быть любая другая логика атаки игрока

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        Debug.Log("Enemy attacked player for " + attackDamage + " damage.");
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        animator.Play("isDamaged");
        //animator.SetTrigger("isDamaged");
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        animator.Play("Die");
        //animator.SetBool("Die", true);
        Destroy(this.gameObject, 1f);


        anim.StopPlayback();
        anim.Play("Death");
        Debug.Log("Dead!!!");
        animator.SetBool("Death", true);
        Destroy(this.gameObject);
        //this.enabled = false;
        //GetComponent<Collider2D>().enabled = false;
    }
} 
