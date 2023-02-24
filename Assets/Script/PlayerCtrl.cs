using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public float speed;
    public float power = 1f;
    public float life = 3f;

    public bool isTouchTop = false;
    public bool isTouchBottom = false;
    public bool isTouchLeft = false;
    public bool isTouchRight = false;

    Animator anim;
    Rigidbody2D rigi2D;

    public GameObject bulletPrefab01;
    public GameObject bulletPrefab02;

    public GameObject gameMgrObj;

    public float curBulletDelay = 0f;
    public float maxBulletDelay = 0.3f;

    SpriteRenderer spriteCache;
    Color colorCache;

    bool isHit = false;

    private void Awake()
    {

    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        rigi2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        Move();
        Fire();
        ReloadBullet();
    }


    private void FixedUpdate()
    {
        if (!isHit)
            return;

        if (isHit)
        {
            float val = MathF.Sin(Time.time * 50);
            Debug.LogWarning(val);
            if (val > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;

            }
            return;
        }
    }
    private void Move()
    {
        if (!isHit)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            {
                h = 0;
            }
            if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            {
                v = 0;
            }

            Vector3 curPos = transform.position;
            Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

            transform.position = curPos + nextPos;


            anim.SetInteger("Input", (int)h);
        }
    }
    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curBulletDelay < maxBulletDelay)
            return;

        Power();

        curBulletDelay = 0;
    }
    void Power()
    {
        switch (power)
        {
            case 1:
                {
                    GameObject bulletC1 = Instantiate(bulletPrefab01, transform.position, Quaternion.identity);
                    Rigidbody2D rdC1 = bulletC1.GetComponent<Rigidbody2D>();
                    rdC1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);


                }
                break;
            case 2:
                {
                    GameObject bulletL2 = Instantiate(bulletPrefab01, transform.position + Vector3.left * 0.1f, Quaternion.identity);
                    Rigidbody2D rdL2 = bulletL2.GetComponent<Rigidbody2D>();
                    rdL2.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                    GameObject bulletR2 = Instantiate(bulletPrefab01, transform.position + Vector3.right * 0.1f, Quaternion.identity);
                    Rigidbody2D rdR2 = bulletR2.GetComponent<Rigidbody2D>();
                    rdR2.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                }
                break;
            case 3:
                {
                    GameObject bulletC3 = Instantiate(bulletPrefab02, transform.position, Quaternion.identity);
                    Rigidbody2D rdC3 = bulletC3.GetComponent<Rigidbody2D>();
                    rdC3.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                    GameObject bulletL3 = Instantiate(bulletPrefab01, transform.position + Vector3.left * 0.25f, Quaternion.identity);
                    Rigidbody2D rdL3 = bulletL3.GetComponent<Rigidbody2D>();
                    rdL3.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                    GameObject bulletR3 = Instantiate(bulletPrefab01, transform.position + Vector3.right * 0.25f, Quaternion.identity);
                    Rigidbody2D rdR3 = bulletR3.GetComponent<Rigidbody2D>();
                    rdR3.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                }

                break;

        }
    }

    void ReloadBullet()
    {
        curBulletDelay += Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBorder")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }

        if (collision.gameObject.tag == "EnemyBullet" && gameObject.tag == "Player")
        {
            if (isHit)
                return;

            isHit = true;
            life--;

            GameManager gmLogic = gameMgrObj.GetComponent<GameManager>();
            if (life <=0)
            {
                anim.SetTrigger("IsDead");
                gmLogic.GameOver();
            }
            else
            {
                gmLogic.RespawnPlayer();
            }

            Invoke("HitEnd", 0.6f);
        }
    }

    void HitEnd()
    {

        isHit = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.SetActive(false);

    }
    private void OnEnable()
    {

        spriteCache = GetComponent<SpriteRenderer>();
        colorCache = spriteCache.color;
        colorCache.a = 0.5f;
        spriteCache.color = colorCache;

        gameObject.tag = "PlayerNodamage";

        //PolygonCollider2D polyCol = GetComponent<PolygonCollider2D>();
        //polyCol.enabled = false;
        Invoke("ReturnNodamage", 2.0f);
    }
    void ReturnNodamage()
    {
        spriteCache = GetComponent<SpriteRenderer>();
        colorCache = spriteCache.color;
        colorCache.a = 1f;
        spriteCache.color = colorCache;

        //PolygonCollider2D polyCol = GetComponent<PolygonCollider2D>();
        //polyCol.enabled = true;
        gameObject.tag = "Player";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBorder")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }
}
