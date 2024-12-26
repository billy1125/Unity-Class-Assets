using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; //移動速度

    public Transform waypoint;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        Vector3 targetPosition = waypoint.position;
        moveDirection = (targetPosition - transform.position).normalized;
    }

    void FixedUpdate()
    {
        // 移動敵人
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
