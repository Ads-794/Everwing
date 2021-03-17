using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    int health;

    [SerializeField] GameObject coin;

    void Awake()
    {
        health = GetMaxHealth();
    }

    protected virtual void Die()
    {
        int coinCount = GetCoins();

        for (int i = 0; i < coinCount; i++)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);

        if (health == 0)
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Bullet")
        {
            //Take Damage
            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet.isFromPlayer)
            {
                TakeDamage(bullet.GetDamage());         
                Destroy(other.gameObject);       
            }

        }
    }
    public abstract int GetMaxHealth();
    public abstract int GetCoins();
}
