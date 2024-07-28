using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    void FixedUpdate()
    {
        //簡單的左右控制，這個範例與過去的貓咪移動都是類似的
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-0.1f, 0, 0);
        }
    }
}
