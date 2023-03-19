using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
        Debug.Log("Dead!!!");
        animator.SetBool("Death", true);
        Destroy(this.gameObject);
        //this.enabled = false;
        //GetComponent<Collider2D>().enabled = false;
        
    }
} 
