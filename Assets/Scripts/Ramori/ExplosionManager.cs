using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    private static ExplosionManager _instance;

    public GameObject ExplosionPrefab;

    void Awake()
    {
        _instance = this;
    }
    
    public static void Explode(GameObject obj, float scale = 1f)
    {
        if (_instance != null && _instance.ExplosionPrefab != null) {
            GameObject go = Instantiate(_instance.ExplosionPrefab, obj.transform.position, Quaternion.identity);
            go.transform.localScale = Vector3.one * scale;
        }

        Destroy(obj);
    }
}
