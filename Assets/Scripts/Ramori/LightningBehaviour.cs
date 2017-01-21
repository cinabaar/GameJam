using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBehaviour : MonoBehaviour
{
    float time;
    SpriteRenderer renderer;
    float blink;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        time = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        
        if (time < 0f) {
            time = Random.value * 5f;
            blink = 0.1f;
            Color c = renderer.color;
            c.a = 0.1f;
            renderer.color = c;
        }

        if (blink > 0f) {
            blink -= Time.deltaTime;

            if (blink <= 0f) {
                Color c = renderer.color;
                c.a = 0f;
                renderer.color = c;
            }
        }
    }
}
