using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    Rigidbody2D rigid2D;          // 用來放貓咪的剛體公開變數
    public float jumpForce = 680.0f;     // 跳躍力預設值
    public float walkForce = 30.0f;      // 移動推力預設值
    public float maxWalkSpeed = 2.0f;    // 限制移動的速度值

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

        // 左右移動
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // 遊戲角色的速度
        float speedx = Mathf.Abs(rigid2D.velocity.x);

        // 速度限制
        if (speedx < maxWalkSpeed)
        {
            rigid2D.AddForce(transform.right * key * walkForce);
        }

        // (追加)依照行進方向翻轉
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
    }
}
