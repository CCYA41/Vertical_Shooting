using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float power = 0f;

    //public GameObject hitEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.tag == "Enemy"&&gameObject.tag == "PlayerBullet")
        //{
        //    Instantiate(hitEffect, transform.position, Quaternion.identity);
        //}
        if(collision.gameObject.tag == "Border")
        {
            
            Destroy(gameObject);
        }
    }
}
