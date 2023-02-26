using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{

    public float moveSpeed = 0f;        // ����� �����̴� �ӵ��� �����ϱ����� ����;
    public float resetPositionY = 24f;   // ����� ȭ���� ������� �ʱ�ȭ�ϴ� y��ǥ�� �����ϱ����� ����;

    public GameObject[] images;

    private void FixedUpdate()
    {
        // Translate�� �̿��� Vector3.down (-y)������ �̵��ϴ� ��
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // y ��ǥ ���� -12�϶� ���ǹ����

        foreach (GameObject tiles in images)
        {
            if (tiles.transform.position.y < -11)
            // �� ������Ʈ�� (x, y, z)��ġ�� (0, resetPositionY, 0) ��ġ�� �̵�
            { tiles.transform.position = new Vector3(0, resetPositionY, 0); }

          //  Debug.Log(tiles.name);
        }
    }
}
