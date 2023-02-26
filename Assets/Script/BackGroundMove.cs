using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{

    public float moveSpeed = 0f;        // 배경이 움직이는 속도를 제어하기위한 변수;
    public float resetPositionY = 24f;   // 배경이 화면을 벗어났을때 초기화하는 y좌표를 지정하기위한 변수;

    public GameObject[] images;

    private void FixedUpdate()
    {
        // Translate를 이용해 Vector3.down (-y)쪽으로 이동하는 식
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // y 좌표 값이 -12일때 조건문사용

        foreach (GameObject tiles in images)
        {
            if (tiles.transform.position.y < -11)
            // 이 오브젝트의 (x, y, z)위치를 (0, resetPositionY, 0) 위치로 이동
            { tiles.transform.position = new Vector3(0, resetPositionY, 0); }

          //  Debug.Log(tiles.name);
        }
    }
}
