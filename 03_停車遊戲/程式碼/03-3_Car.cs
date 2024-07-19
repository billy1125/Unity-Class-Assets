using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    float speed = 0;
    Vector2 startPos;

    void Start() {
    }

    void Update() {

        // ���o�ưʪ���
        if(Input.GetMouseButtonDown(0)) {            
            this.startPos = Input.mousePosition; // �I���ƹ��ɪ��y��
        } else if(Input.GetMouseButtonUp(0)) {           
            Vector2 endPos = Input.mousePosition;  // ��}�ƹ��ɪ��y��
            float swipeLength = endPos.x - this.startPos.x;           
            this.speed = swipeLength / 500.0f;  // ��ưʪ����ഫ����l���ʳt��
        }

        transform.Translate(this.speed, 0, 0);
        this.speed *= 0.98f;
    }
}