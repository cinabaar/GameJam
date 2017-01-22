﻿
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollect : MonoBehaviour
{
    static AudioSource redBot;
    public AudioClip collectSound;

    int collected = 0;
    public Text CollectedText;

    private void Start()
    {
        CollectedText.text = "0";

        if ( redBot == null )
            redBot = GameObject.Find( "RedBot" ).GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CollectableComponent>() == null)
            return;
        var collectable = other.gameObject.GetComponent<CollectableComponent>();
        collected++;
        CollectedText.text = collected + "";
        Destroy(collectable.gameObject);

        redBot.PlayOneShot( collectSound );
    }
}

