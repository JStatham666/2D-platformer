using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //private Health _health;
    public int health;

    //private void Awake()
    //{
    //    _health = new Health();
    //}

    private void Update()
    {
        //if (_health.CurrentHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    //private void OnEnable()
    //{
    //    _health.Died += Die;
    //}

    //private void OnDisable()
    //{
    //    _health.Died -= Die;
    //}

    //public void TakeDamage(float damage)
    //{
    //    _health.TakeDamage(damage);
    //}

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    //private void Die()
    //{
    //    Destroy(gameObject);
    //}
}
