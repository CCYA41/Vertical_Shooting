using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float destroyTimer = 0f;

    private void OnEnable()
    {
        Destroy(gameObject,destroyTimer);
    }
}
