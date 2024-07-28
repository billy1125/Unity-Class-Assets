using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    void Start() {
    }

    void Update() {
        // 點擊左方向鈕時
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.Translate(-3, 0, 0); // 往左移動「3」
        }

        // 點擊左方向鈕時
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.Translate(3, 0, 0); // 往右移動「3」
        }
    }
}