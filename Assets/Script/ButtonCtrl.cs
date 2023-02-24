using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    public void GameStart()
    {
        GameManager gmLogic = FindObjectOfType<GameManager>();
        if (!gmLogic.isStart)
        {
            gmLogic.UIInitialize();
        }
        Time.timeScale = 1.0f;
        gmLogic.isStart = false;
        gameObject.SetActive(false);
    }
}
