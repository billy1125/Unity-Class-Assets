using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("檢測設置")]
    [SerializeField] private float detectInterval = 0.2f;        // 檢測間隔時間
    [SerializeField] private float detectMinDistance = 5f;       // 最小檢測距離
    [SerializeField] private string targetTag = "Tank";          // 目標標籤

    [Header("旋轉設置")]
    [SerializeField] private float rotationSpeed = 5f;           // 旋轉速度
    [SerializeField] private Transform turretPart;               // 砲塔旋轉部分

    private GameObject currentTarget;                            // 當前目標
    private bool isTargetLocked;                                // 是否鎖定目標

    private void Start()
    {
        // 開始定期檢測目標
        InvokeRepeating(nameof(DetectNearestTarget), 0f, detectInterval);
    }

    private void Update()
    {
        if (isTargetLocked && currentTarget != null)
        {
            RotateTurret();
        }
    }

    private void DetectNearestTarget()
    {
        // 重置目標鎖定狀態
        isTargetLocked = false;
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        // 獲取所有具有指定標籤的物件
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(targetTag);

        // 遍歷所有可能的目標
        foreach (GameObject target in possibleTargets)
        {
            // 計算與目標的距離
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

            // 更新最短距離和最近目標
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearestTarget = target;
            }
        }

        // 檢查是否在最小檢測距離內
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

        // 計算目標相對位置
        Vector2 targetDirection = currentTarget.transform.position - turretPart.position;

        // 計算目標角度
        float angle = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;

        // 創建目標旋轉
        Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);

        // 使用線性插值平滑旋轉
        turretPart.rotation = Quaternion.Lerp(
            turretPart.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    // 在 Unity 編輯器中繪製檢測範圍
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectMinDistance);
    }
}
