﻿using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float InvulnAfterHit = 0.5f;
    public float BlinkInterval = 0.1f;
    public int MaxHealth = 5;
    public Material BlinkMaterial;

    private int health;
    private float invuln = 0f;
    private float blink = 0f;
    private bool isBlinking = false;
    private Animator m_Anim;
    private SpriteRenderer m_Renderer;
    private Material m_DefaultMaterial;

    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Renderer = GetComponent<SpriteRenderer>();
        m_DefaultMaterial = m_Renderer.material;

        health = MaxHealth;
        UIManager.InitHealth(health);
    }

    void Update()
    {
        if (invuln <= 0f)
            return;

        invuln -= Time.deltaTime;
        blink -= Time.deltaTime;

        if (invuln <= 0f) {
            m_Renderer.material = m_DefaultMaterial;
        }
        else if (blink <= 0f) {
            FlipBlink();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.enabled = false;
        ExplosionManager.Explode(other.gameObject, 1f + Random.value * 0.2f);

        if (invuln > 0f)
            return;

        isBlinking = true;
        blink = BlinkInterval;
        m_Renderer.material = BlinkMaterial;
        m_Anim.SetTrigger("Hit");
        health--;
        UIManager.UpdateHealth(health);
        Screenshake.Shake(0.3f, 0.3f);
        invuln = InvulnAfterHit;
        if (health == 0) {
            Screenshake.Shake(1f, 1.2f);
            gameObject.SetActive(false);
            ScrollingManager.SetSpeed(0f);
        }
    }

    private void FlipBlink()
    {
        isBlinking = !isBlinking;
        blink = BlinkInterval;
        m_Renderer.material = isBlinking ? BlinkMaterial : m_DefaultMaterial;
    }
}