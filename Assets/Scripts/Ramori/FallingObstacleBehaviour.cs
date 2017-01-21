using UnityEngine;

[RequireComponent(typeof(ScrollingBehaviour))]
public class FallingObstacleBehaviour : MonoBehaviour
{
    public float Duration = 2f;

    float time = 0f;

    void Start()
    {

    }

    void Update()
    {
        if (time > Duration)
            return;

        time += Time.deltaTime;

        float ratio = time / Duration;

        float y = 4 * (time - time * time);
    }
}
