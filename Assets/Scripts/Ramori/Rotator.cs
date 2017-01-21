using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        transform.localEulerAngles += new Vector3(0f, 0f, Speed * Time.deltaTime);
    }
}
