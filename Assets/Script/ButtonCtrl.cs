using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    public void GameStart()
    {
        GameManager gmLogic = FindObjectOfType<GameManager>();
        if (!GameManager.isStart)
        {
            gmLogic.UIInitialize();
        }
        Time.timeScale = 1.0f;
        GameManager.isStart = false;
        gameObject.SetActive(false);
    }
}
