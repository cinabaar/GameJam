using UnityEngine;

[RequireComponent(typeof(ScrollingBehaviour))]
public class FallingObstacleBehaviour : MonoBehaviour
{
    public float Duration = 2f;
    public float Height = 5f;

    float Time = 0f;
    SpriteRenderer[] Renderers;
    Color[] RendererColors;
    bool swapped = false;
    float StartY = 0f;
    Vector3 StartScale;
    ScrollingBehaviour Scroll;

    void Start()
    {
        Renderers = GetComponentsInChildren<SpriteRenderer>();
        RendererColors = new Color[Renderers.Length];
        for (int i = 0; i < Renderers.Length; i++) {
            Renderers[i].sortingOrder = -16;
            RendererColors[i] = Renderers[i].color;
            Renderers[i].color = new Color(RendererColors[i].r * 0.5f, RendererColors[i].g * 0.5f, RendererColors[i].b * 0.5f);
        }

        StartY = transform.localPosition.y;
        StartScale = transform.localScale;

        Scroll = GetComponent<ScrollingBehaviour>();
        Scroll.enabled = false;
    }

    void Update()
    {
        if (Time > Duration)
            return;

        Time += UnityEngine.Time.deltaTime;

        float ratio = Time / Duration;

        float y = Height * 4f * (ratio - ratio * ratio);
        float s = Mathf.Clamp01(0.5f + ratio);

        transform.localPosition = new Vector3(transform.localPosition.x, StartY + y, transform.localPosition.z);
        transform.localScale = StartScale * s;

        for (int i = 0; i < Renderers.Length; i++) {
            Renderers[i].color = new Color(RendererColors[i].r * s, RendererColors[i].g * s, RendererColors[i].b * s);
        }

        if (!swapped && ratio > 0.5f) {
            swapped = true;
            for (int i = 0; i < Renderers.Length; i++) {
                Renderers[i].sortingOrder = 5;
            }
            Scroll.enabled = true;
        }

        if (Time > Duration) {
            transform.localPosition = new Vector3(transform.localPosition.x, StartY, transform.localPosition.z);
            transform.localScale = StartScale;
        }
    }
}
