﻿using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    private static ExplosionManager _instance;

    public GameObject ExplosionPrefab;
    public GameObject CollectPrefab;

    void Awake()
    {
        _instance = this;
    }
    
    public static void Explode(GameObject obj, float scale = 1f)
    {
        if (_instance != null && _instance.ExplosionPrefab != null) {
            GameObject go = Instantiate(_instance.ExplosionPrefab, obj.transform.position, Quaternion.identity);
            go.transform.localScale = Vector3.one * scale;
            go.transform.localEulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        }
    }

    public static void Collect(GameObject obj, float scale = 1f)
    {
        if (_instance != null && _instance.CollectPrefab != null) {
            GameObject go = Instantiate(_instance.CollectPrefab, obj.transform.position, Quaternion.identity);
            go.transform.localScale = Vector3.one * scale;
        }
    }
}
