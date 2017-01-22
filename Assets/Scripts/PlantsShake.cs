using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsShake : MonoBehaviour
{
    private GameObject[] smilingFoliage;

    public Transform player;

    public Sprite sad;

    public float burningOffset;

    public float amplitude;
    public float offset;
    public float frequency;

    // Use this for initialization
    void Start()
    {
        try
        {
            smilingFoliage = GameObject.FindGameObjectsWithTag("SmilingFoliage");
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        if (smilingFoliage == null) return;
        int index = 0;
        foreach ( GameObject instance in smilingFoliage )
        {
            instance.transform.Translate( new Vector3( 0, Mathf.Sin( Time.time * frequency + offset * index ) * amplitude * Time.deltaTime ) );
            ++index;

            if ( instance.transform.position.x + burningOffset < player.position.x )
            {
                // make it burn!
                SpriteRenderer renderer = instance.GetComponent<SpriteRenderer>();
                renderer.sprite = sad;

                Transform flameTransform = instance.transform.Find( "flame" );
                flameTransform.gameObject.SetActive( true );
            }
        }
    }
}
