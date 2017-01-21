using UnityEngine;

public class ChildRotator : MonoBehaviour
{
    public float Angle = 10f;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            child.localEulerAngles = new Vector3(0f, 0f, (Random.value - 0.5f) * 2f * Angle);
        }
    }
}