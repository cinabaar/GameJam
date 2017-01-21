using UnityEngine;

public class FallingObjectsSpawnerBehaviour : MonoBehaviour
{
    static Transform redBot;

    public float TriggerDistance = 25f;
    public GameObject FallingObjectPrefab;

    void Start()
    {
        if (redBot == null)
            redBot = GameObject.Find("RedBot").transform;
    }
    
    void Update()
    {
        if (Mathf.Abs(transform.position.x - redBot.position.x) < TriggerDistance) {
            GameObject go = Instantiate(FallingObjectPrefab);
            go.transform.SetParent(transform.parent);
            go.transform.localPosition = transform.localPosition;
            go.transform.localRotation = transform.localRotation;

            Destroy(gameObject);
        }
    }
}
