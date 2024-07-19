using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float rotSpeed = 0;  // 旋轉速度

    void Start()
    {

    }

    void Update()
    {
        // 若點擊滑鼠就要設定旋轉速度
        if (Input.GetMouseButtonDown(0))
        {
            rotSpeed = 10;
        }

        // 旋轉速度、讓輪盤旋轉
        transform.Rotate(0, 0, rotSpeed);
    }
}
