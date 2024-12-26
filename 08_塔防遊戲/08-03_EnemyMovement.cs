using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;              // 移動速度
    [SerializeField] private float rotationSpeed = 5f;          // 旋轉速度
    [SerializeField] private float waypointThreshold = 0.1f;    // 最短停止距離
    [SerializeField] private bool shouldRotate = true;          // 控制是否要旋轉

    private List<Vector2> waypoints;
    private int currentWaypointIndex = 0;
    private bool hasReachedEnd = false;

    private void Start()
    {
        waypoints = PathManager.Instance.GetPath();

        if (waypoints.Count > 0)
        {
            transform.position = waypoints[0];
        }
    }

    private void Update()
    {
        if (hasReachedEnd || waypoints == null || waypoints.Count == 0)
            return;

        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = waypoints[currentWaypointIndex];

        // 計算移動方向
        Vector2 moveDirection = (targetPosition - currentPosition).normalized;

        // 2D 旋轉處理
        if (shouldRotate && moveDirection != Vector2.zero)
        {
            // 計算角度（從右方向為 0 度）
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;

            // 使用 Lerp 平滑旋轉
            Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // 移動敵人
        transform.position = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // 檢查是否到達當前路徑點(邏輯：如果兩點距離已經低於最短停止距離，就換下一個打卡點)
        if (Vector2.Distance(currentPosition, targetPosition) < waypointThreshold)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Count)
            {
                OnReachEnd();
            }
        }
    }

    private void OnReachEnd()
    {
        hasReachedEnd = true;     
    }
}
