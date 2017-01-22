using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float Delay = 1f;

    void Update()
    {
        Delay -= Time.deltaTime;
        if (Delay < 0f)
            Destroy(gameObject);
    }
}