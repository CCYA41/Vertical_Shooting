using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemyCtrl : MonoBehaviour
{
    public float speed;
    public float health;

    public float curBulletDelay = 0f;
    public float maxBulletDelay = 1f;

    public float bulletSpeed = 3f;

    public Sprite[] sprites;

    public GameObject bulletPrefab00;
    public GameObject bulletPrefab01;
    public GameObject playerObject;

    Rigidbody2D rd;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        
        rd = GetComponent<Rigidbody2D>();
      

    }
    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Fire();
        ReloadBullet();
    }
    void Fire()
    {
        if (curBulletDelay > maxBulletDelay)
        {
            Power();

            curBulletDelay = 0;
        }
    }

    private void Power()
    {
        GameObject bulletObj = Instantiate(bulletPrefab00, transform.position, Quaternion.identity);
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        //GameManager gameManager = GetComponent<GameManager>();
        ////GameObject playerObject = GameObject.FindWithTag("Player");
        //Vector3 targetPos = gameManager.PlayerPosition();
        //Vector3 dir = targetPos - transform.position;

        Vector3 dirVec = playerObject.transform.position - transform.position;
        rb.AddForce(dirVec.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    void ReloadBullet()
    {
        curBulletDelay += Time.deltaTime;
    }

    public void Move(int nPoint)
    {
        if (nPoint == 5 || nPoint == 6)
        {
            transform.Rotate(Vector3.back * 45);
            rd.velocity = new Vector2(speed * (-1), -1);
        }
        else if (nPoint == 3 || nPoint == 4)
        {
            transform.Rotate(Vector3.forward * 45);
            rd.velocity = new Vector2(speed * (1), -1);
        }
        else
        {
            rd.velocity = Vector2.down * speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            BulletCtrl bulletCtrl = collision.GetComponent<BulletCtrl>();
            OnHit(bulletCtrl.power);
            Destroy(collision.gameObject);

        }
    }

    private void OnHit(float BulletPower)
    {
        health -= BulletPower;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {

            Destroy(gameObject);
        }
    }

    private void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }
}
