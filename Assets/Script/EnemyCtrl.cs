using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{

    public float health;
    public float score;
    public float speed;

    public float animationScale = 1f;
    public float bulletSpeed = 3f;
    public float curBulletDelay = 0f;
    public float maxBulletDelay = 1f;

    public Sprite[] sprites;

    public GameObject bulletPrefab00;
    // public GameObject playerObject;
    public GameObject itemMakerPrefab;

    bool isDead = false;
    bool isGenerate = false;

    Animation anime;
    Animator anim;
    Rigidbody2D rd;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anime = GetComponent<Animation>();

    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Fire();
        ReloadBullet();

    }

    void Fire()
    {
        if (curBulletDelay > maxBulletDelay && !isDead)
        {
            Power();

            curBulletDelay = 0;
        }
    }

    private void Power()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject)
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


        spriteRenderer.color = Color.red;
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            DestroyOrder();

        }
    }

    public void DestroyOrder()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.enabled = false;

        isDead = true;

        rd.constraints = RigidbodyConstraints2D.FreezeAll;

        if (!isGenerate)
        {
            GameManager.curScore += (int)score;
            GameManager.curEnemyCount++;

            GameObject item = Instantiate(itemMakerPrefab, transform.position, Quaternion.identity);

            isGenerate = true;

        }

        this.transform.localScale = new Vector3(animationScale, animationScale, 0);
        anim.SetTrigger("IsDead");

        Destroy(this.gameObject, 0.5f);

    }

    private void ReturnSprite()
    {

        spriteRenderer.color = new Color(255f, 255f, 255f);

    }
}
