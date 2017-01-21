using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMove : MonoBehaviour
{
    public float MoveX;
    public float MoveY;

    float offsetX;
    float offsetY;

    float scaleX;
    float scaleY;

    float z;

    void Start()
    {
        offsetX = Random.value * Mathf.PI * 2f;
        offsetY = Random.value * Mathf.PI * 2f;
        scaleX = 0.9f + 0.2f * Random.value;
        scaleY = 0.9f + 0.2f * Random.value;
        z = transform.localPosition.z;
    }
    
    void Update()
    {
        float x = Mathf.Sin(offsetX + Time.time * scaleX) * MoveX;
        float y = Mathf.Sin(offsetY + Time.time * scaleY) * MoveY;
        transform.localPosition = new Vector3(x, y, z);
    }
}
