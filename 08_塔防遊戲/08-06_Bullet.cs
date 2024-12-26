using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private bool isMissile;
    private float speed;
    private float damage;
    private GameObject target;
    private Vector2 direction = new Vector2(0, 1);

    public void Initialize(float bulletSpeed, float bulletDamage, GameObject bulletTarget)
    {
        speed = bulletSpeed;
        damage = bulletDamage;
        target = bulletTarget;

        if (target != null && isMissile)
        {
            // 計算初始方向
            direction = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
        }

        // 5秒後自動銷毀（避免子彈永久存在）
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        // 往前飛行
        transform.Translate(direction * speed * Time.deltaTime);
        //if (target == null)
        //{
        //    // 如果目標消失，保持原方向飛行
        //    transform.Translate(direction * speed * Time.deltaTime);
        //}
        //else
        //{
        //    // 更新方向以追蹤目標
        //    direction = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
        //    transform.Translate(direction * speed * Time.deltaTime);
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            // 獲取敵人腳本並造成傷害
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // 銷毀子彈
            Destroy(gameObject);
        }
    }
}
