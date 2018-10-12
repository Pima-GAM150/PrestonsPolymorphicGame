using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{

    public float maxHealth;
    public float health;
    public float speed;
    public float attack;
    public float defense;

    public new Transform transform;

    void Start()
    {

    }

    void Update()
    {

    }

    //spawn a simple melee hitbox and check if it hits anyone
    public void MeleeAttack(float attack, float range)
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            IDied();
        }
    }

    public void RestoreHealth(float healValue)
    {
        if(health + healValue >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healValue;
        }
    }

    public void IDied()
    {
        Destroy(this.gameObject);
    }
}
