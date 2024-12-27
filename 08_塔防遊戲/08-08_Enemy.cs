using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject explsionPrefab;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (explsionPrefab != null) 
            Instantiate(explsionPrefab, transform.position, Quaternion.identity);
        // 可以添加死亡效果、掉落物品等
        Destroy(gameObject);
    }
}
