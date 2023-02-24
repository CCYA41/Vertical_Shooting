using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public float multiple = 1f;
    public float speed = 1f;

    private void FixedUpdate()
    {
        transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, multiple, Time.deltaTime * speed), Mathf.Lerp(transform.localScale.y, multiple, Time.deltaTime * speed));
    }
}
