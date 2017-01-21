using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float InvulnAfterHit = 0.5f;
    public int MaxHealth = 5;

    private int health;
    private float invuln = 0f;
    private Animator m_Anim;

    void Start()
    {
        m_Anim = GetComponent<Animator>();

        health = MaxHealth;
        UIManager.InitHealth(health);
    }

    void Update()
    {
        if (invuln > 0f)
            invuln -= Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.enabled = false;
        ExplosionManager.Explode(other.gameObject, 1f + Random.value * 0.2f);

        if (invuln > 0f)
            return;

        m_Anim.SetTrigger("Hit");
        health--;
        UIManager.UpdateHealth(health);
        Debug.LogWarning("HIT! " + health + "\n");
        Screenshake.Shake(0.3f, 0.3f);
        invuln = InvulnAfterHit;
        if (health == 0) {
            Screenshake.Shake(1f, 1.2f);
            gameObject.SetActive(false);
            ScrollingManager.SetSpeed(0f);
        }
    }
}