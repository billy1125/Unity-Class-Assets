using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    void Start() {
    }

    void Update() {
        // 每個影格等速往下移動
        transform.Translate(0, -0.1f, 0);

        // 當箭頭超出遊戲畫面時就捨棄物件
        if(transform.position.y < -5.0f) {
            Destroy(gameObject);
        }
    }
}