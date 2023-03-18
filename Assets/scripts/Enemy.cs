using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 100;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
        anim.StopPlayback();
        anim.Play("Death");
        Debug.Log("Dead!!!");
        Destroy(this.gameObject);
        //this.enabled = false;
        //GetComponent<Collider2D>().enabled = false;
        
    }

} 
