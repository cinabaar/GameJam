using UnityEngine;

public class ChildPainter : MonoBehaviour
{
    public float BaseColor = 0.95f;
    public float DiffColor = 0.05f;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) {
            Color color = new Color(BaseColor + DiffColor * Random.value, BaseColor + DiffColor * Random.value, BaseColor + DiffColor * Random.value, 1f);
            Transform child = transform.GetChild(i);
            for (int j = 0; j < child.childCount; j++) {
                SpriteRenderer sr = child.GetChild(j).GetComponent<SpriteRenderer>();
                sr.color = color;
            }
        }
    }
}