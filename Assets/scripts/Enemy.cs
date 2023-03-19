using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;

    public float detectionRange = 10f; // ����������, �� ������� ���� �������� ������
    public float attackRange = 0.5f; // ����������, �� ������� ���� ����� ��������� ������
    public float movementSpeed = 5f; // �������� ������������ �����
    public int attackDamage = 5; // ���� �� ����� �����

    private Transform hero; // ������� ������
    private NavMeshAgent agent; // ��������� ��� ������������ �� �������


    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        hero = GameObject.FindGameObjectWithTag("Player").transform; // �������� ������� ������ �� ���� "Player"
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, hero.position); // ��������� ���������� �� ������

        if (distance <= detectionRange) // ���� ����� ��������� � ���� ��������� �����
        {
            Vector3 direction = (hero.position - transform.position).normalized; // ��������� ����������� � ������
            transform.Translate(direction * movementSpeed * Time.deltaTime); // ����������� ����� � ����������� ������

            if (distance <= attackRange) // ���� ����� ��������� � ���� ����� �����
            {
                Attack(); // ������� ������
            }
        }
    }


    public Transform attackPoint;
    public LayerMask enemyLayers;
    void Attack()
    {
        // � ������ ������ �� ������ ������� ��������� � �������, �� ����� ����� ���� ����� ������ ������ ����� ������

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        Debug.Log("Enemy attacked player for " + attackDamage + " damage.");
=======
using UnityEditor;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 100;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
>>>>>>> ecb4df9b0c1b1cde64df92d49f9588a17d655f5b
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
        animator.Play("isDamaged");
        //animator.SetTrigger("isDamaged");
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
<<<<<<< HEAD
        animator.Play("Die");
        //animator.SetBool("Die", true);
        Destroy(this.gameObject,1f);
    }
}
=======
        anim.StopPlayback();
        anim.Play("Death");
        Debug.Log("Dead!!!");
        Destroy(this.gameObject);
        //this.enabled = false;
        //GetComponent<Collider2D>().enabled = false;
        
    }

} 
>>>>>>> ecb4df9b0c1b1cde64df92d49f9588a17d655f5b
