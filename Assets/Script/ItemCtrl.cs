using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    public GameObject[] items;

    float dropSpeed = 2;

    int itemCode = 99;

    

    Rigidbody2D rigi2D;
    PlayerCtrl playerCtrl;


    private void Awake()
    {
        rigi2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rigi2D.velocity = Vector2.down * 2;
    }

    private void OnEnable()
    {

        int randN = Random.Range(0, 20);

        if (randN >= 4 && randN <= 10)
        {
            GameObject item = Instantiate(items[2], transform.position, Quaternion.identity);
            item.transform.SetParent(this.transform, true);
            item.SetActive(true);

            itemCode = 2;
        }
        else if (randN > 10 && randN <=13)
        {
            GameObject item = Instantiate(items[0], transform.position, Quaternion.identity);
            item.transform.SetParent(this.transform, true);
            item.SetActive(true);
            
            itemCode = 0;
        }
        else if (randN > 13 && randN <= 15)
        {
            GameObject item = Instantiate(items[1], transform.position, Quaternion.identity);
            item.transform.SetParent(this.transform, true);
            item.SetActive(true);

            itemCode = 1;
        }
        else
            Destroy(gameObject);

        //int randN = Random.Range(0, 10);

        //if (randN > 2 && randN < 8)
        //{

        //    randN = Random.Range(0, items.Length);
        //    GameObject item = Instantiate(items[randN], transform.position, Quaternion.identity);
        //    item.transform.SetParent(this.transform, true);
        //    item.SetActive(true);
        //    itemCode = randN;

        //}
        //else
        //    Destroy(gameObject);



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerNodamage")
        {
            Transform child = GetComponentInChildren<Transform>();
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerCtrl playerCtrl = FindObjectOfType<PlayerCtrl>();
            switch (itemCode)
            {
                case 0:
                    if (playerCtrl.power < 3)
                    {
                        playerCtrl.power += 1;
                    }
                    else
                    {
                        GameManager.curScore += 100;
                    }
                    Destroy(child.gameObject);
                    Destroy(gameObject);
                    break;
                case 1:
                    GameManager.curScore += 500;
                    GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
                    GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
                    for (int i = 0; i < enemys.Length; i++)
                    {
                        EnemyCtrl enemyCtrl = enemys[i].GetComponent<EnemyCtrl>();
                        enemyCtrl.DestroyOrder();
                        GameManager gmLogic = FindObjectOfType<GameManager>();
                        gmLogic.curEnemySpwanDelay = -2;
                    }
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        Destroy(bullets[i], 0.5f);
                    }
                    Destroy(child.gameObject);
                    Destroy(gameObject);
                    break;
                case 2:
                    GameManager.curScore += 100;
                    Destroy(child.gameObject);
                    Destroy(gameObject);
                    break;
            }

        }
    }

}
