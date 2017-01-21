using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFlames : MonoBehaviour
{
    public Transform[] flamesLayers;
    public float targetHeight;

    private int currentLayerIndex = 0;
    private Vector3 _currentVelocity;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ( currentLayerIndex >= flamesLayers.Length )
            return;

        Transform currentLayerTransform = flamesLayers[currentLayerIndex];
        currentLayerTransform.localPosition = Vector3.SmoothDamp( currentLayerTransform.localPosition, 
            new Vector3( currentLayerTransform.localPosition.x, targetHeight ) , ref _currentVelocity, 1.0f, 1.0f );

        if ( currentLayerTransform.localPosition.y >= targetHeight - 0.1f )
            ++currentLayerIndex;
	}
}
