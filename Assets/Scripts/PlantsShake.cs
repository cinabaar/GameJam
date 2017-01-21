using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    private GameObject[] smilingFoliage;

    public float amplitude;
    public float offset;
    public float frequency;

    // Use this for initialization
    void Start ()
    {
        smilingFoliage = GameObject.FindGameObjectsWithTag( "SmilingFoliage" );
	}
	
	// Update is called once per frame
	void Update ()
    {
        int index = 0;
        foreach ( GameObject instance in smilingFoliage )
        {
            instance.transform.Translate( new Vector3( 0, Mathf.Sin( Time.time * frequency + offset * index ) * amplitude * Time.deltaTime ) );
            ++index;
        }
    }
}
