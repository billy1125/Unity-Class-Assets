using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("檢測設置")]
    [SerializeField] private float detectInterval = 0.2f;
    [SerializeField] private float detectMinDistance = 5f;
    [SerializeField] private string targetTag = "Tank";

    [Header("旋轉設置")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Transform turretPart;

    [Header("發射設置")]
    [SerializeField] private GameObject bulletPrefab;            // 子彈預製體
    [SerializeField] private Transform firePoint;                // 發射點
    [SerializeField] private float fireRate = 1f;               // 發射頻率
    [SerializeField] private float bulletSpeed = 10f;           // 子彈速度
    [SerializeField] private float bulletDamage = 10f;          // 子彈傷害
    [SerializeField] private bool canFire = true;               // 是否可以發射

    private GameObject currentTarget;
    private bool isTargetLocked;
    private float nextFireTime;

    private void Start()
    {
        InvokeRepeating(nameof(DetectNearestTarget), 0f, detectInterval);
    }

    private void Update()
    {
        if (isTargetLocked && currentTarget != null)
        {
            RotateTurret();

            // 檢查是否可以發射
            if (canFire && Time.time >= nextFireTime)
            {
                Fire();
            }
        }
    }

    private void DetectNearestTarget()
    {
        isTargetLocked = false;
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject target in possibleTargets)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearestTarget = target;
            }
        }

        if (shortestDistance <= detectMinDistance)
        {
            currentTarget = nearestTarget;
            isTargetLocked = true;
        }
        else
        {
            currentTarget = null;
        }
    }

    private void RotateTurret()
    {
        if (turretPart == null) return;

        Vector2 targetDirection = currentTarget.transform.position - turretPart.position;
        float angle = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);

        turretPart.rotation = Quaternion.Lerp(
            turretPart.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void Fire()
    {
        if (firePoint == null || bulletPrefab == null) return;

        // 更新下次發射時間
        nextFireTime = Time.time + fireRate;

        // 創建子彈
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // 設置子彈屬性
        if (bulletScript != null)
        {
            bulletScript.Initialize(bulletSpeed, bulletDamage, currentTarget);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectMinDistance);
    }
}
