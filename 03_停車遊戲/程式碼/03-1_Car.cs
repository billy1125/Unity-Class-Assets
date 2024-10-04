using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    float speed = 0;

    void Start() {
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {       //點擊滑鼠時
            this.speed = 0.2f;                  //設定初始速度
        }

        transform.Translate(this.speed, 0, 0);  //移動
        this.speed *= 0.98f;                    //減速
    }
}