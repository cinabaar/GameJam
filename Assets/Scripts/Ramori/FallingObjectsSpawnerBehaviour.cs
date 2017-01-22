using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class FallingObjectsSpawnerBehaviour : MonoBehaviour
{
    static Transform redBot;

    public float TriggerDistance = 25f;
    public GameObject FallingObjectPrefab;
    public bool ShakeOnEnd;

    void Start()
    {
        if ( redBot == null )
            redBot = GameObject.Find( "RedBot" ).transform;
    }

    void Update()
    {
        if ( Mathf.Abs( transform.position.x - redBot.position.x ) < TriggerDistance )
        {
            GameObject go = Instantiate(FallingObjectPrefab);
            go.transform.SetParent( transform.parent );
            go.transform.localPosition = transform.localPosition;
            go.transform.localRotation = transform.localRotation;

            var falling = go.GetComponent<FallingObstacleBehaviour>();
            if ( falling != null )
                falling.ShakeOnEnd = ShakeOnEnd;

            Destroy( gameObject );
        }
    }
}

[CustomEditor( typeof( FallingObjectsSpawnerBehaviour ) )]
public class DrawLineEditor : Editor
{
    void OnSceneGUI()
    {
        FallingObjectsSpawnerBehaviour t = target as FallingObjectsSpawnerBehaviour;
        Handles.DrawLine( t.transform.position, t.transform.position + new Vector3( 20.0f, 0.0f, 0.0f ) );
    }
}
