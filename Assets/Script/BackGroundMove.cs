using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{

    public float moveSpeed = 0f;        // ����� �����̴� �ӵ��� �����ϱ����� ����;
    public float resetPositionY = 0f;   // ����� ȭ���� ������� �ʱ�ȭ�ϴ� y��ǥ�� �����ϱ����� ����;

    private void FixedUpdate()
    {
        // Translate�� �̿��� Vector3.down (-y)������ �̵��ϴ� ��
        transform.Translate(Vector3.down * moveSpeed); 

        // y ��ǥ ���� -12�϶� ���ǹ����
        if (transform.position.y < -12)
        {

            // �� ������Ʈ�� (x, y, z)��ġ�� (0, resetPositionY, 0) ��ġ�� �̵�
            transform.position = new Vector3(0, resetPositionY, 0);
        }
    }
}
