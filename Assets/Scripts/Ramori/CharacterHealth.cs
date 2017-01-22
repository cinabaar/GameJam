using System;
using UnityEngine;

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
    public Action PlayerDied;

    static AudioSource redBot;
    public AudioClip[] hitSounds;

    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Renderer = GetComponent<SpriteRenderer>();
        m_DefaultMaterial = m_Renderer.material;

        if ( redBot == null )
            redBot = GameObject.Find( "RedBot" ).GetComponent<AudioSource>();

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
        if (other.gameObject.GetComponent<CollectableComponent>() != null)
            return;
        other.enabled = false;
        ExplosionManager.Explode(other.gameObject, 1f + UnityEngine.Random.value * 0.2f);
        Destroy(other.gameObject);

        redBot.PlayOneShot( hitSounds[ (int)Math.Round( UnityEngine.Random.value * hitSounds.Length ) ] );

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
            //ScrollingManager.SetSpeed(0f);
            var cam = Camera.main.GetComponent<CameraEndBehaviour>();
            if (cam != null)
            {
                cam.Restart();
            }
        }
    }

    private void FlipBlink()
    {
        isBlinking = !isBlinking;
        blink = BlinkInterval;
        m_Renderer.material = isBlinking ? BlinkMaterial : m_DefaultMaterial;
    }
}