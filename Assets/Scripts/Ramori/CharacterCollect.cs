﻿
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollect : MonoBehaviour
{
    int collected = 0;
    public Text CollectedText;

    private void Start()
    {
        if (CollectedText != null)
            CollectedText.text = "0";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CollectableComponent>() == null)
            return;
        var collectable = other.gameObject.GetComponent<CollectableComponent>();
        collected++;
        CollectedText.text = collected + "";
        ExplosionManager.Collect(collectable.gameObject);
        Destroy(collectable.gameObject);
    }
}

