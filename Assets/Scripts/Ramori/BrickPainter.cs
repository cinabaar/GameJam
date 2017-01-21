using System.Collections.Generic;
using UnityEngine;

public class BrickPainter : MonoBehaviour
{
    public List<GameObject> BrickTemplates;
    public int BricksToSpawn = 20;

    void Start()
    {
        float posX = 0;
        for (int i = 0; i < BricksToSpawn; i++) {
            int idx = Random.Range(0, BrickTemplates.Count);
            GameObject go = Instantiate(BrickTemplates[idx]);
            float width = go.transform.childCount * 2f;
            go.transform.SetParent(transform);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = new Vector3(posX + width / 2f, 0f, 0f);
            go.SetActive(true);
            posX += width;
        }

        for (int i = 0; i < transform.childCount; i++) {
            Color color = new Color(0.95f + 0.05f * Random.value, 0.95f + 0.05f * Random.value, 0.95f + 0.05f * Random.value, 1f);
            Transform child = transform.GetChild(i);
            for (int j = 0; j < child.childCount; j++) {
                SpriteRenderer sr = child.GetChild(j).GetComponent<SpriteRenderer>();
                sr.color = color;
            }
        }
    }
}