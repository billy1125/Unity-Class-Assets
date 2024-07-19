using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid2D;          // 用來放貓咪的剛體公開變數
    float jumpForce = 680.0f;     // 跳躍力預設值

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();  // 取得貓咪的剛體
    }

    void Update()
    {
        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid2D.AddForce(transform.up * jumpForce);
        }
    }
}
