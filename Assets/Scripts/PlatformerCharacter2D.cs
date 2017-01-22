using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public enum PlayerState
    {
        Running = 0,
        Jumping = 1,
        DoubleJump = 2,
        Sliding
    }

    [RequireComponent(typeof(AudioSource))]
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField]
        private float m_MoveSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField]
        private float m_JumpHeight = 1000f;                  // Amount of force added when the player jumps.
        [SerializeField]
        private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField]
        private float m_MaxSlideTime = 1f;
        [SerializeField]
        private float m_SlideSpeedMultiplier = 1.5f;
        [SerializeField]
        private Vector2 m_SlideCapsuleSize = new Vector2(4, 4);
        [SerializeField]
        private float m_MinJumpTime = 0.2f;
        [SerializeField]
        private float m_SlideCooldownTime = 0.5f;

        [SerializeField]
        private Animator m_Anim;            // Reference to the player's animator component.
        [SerializeField]
        private Rigidbody2D m_Rigidbody2D;

        [SerializeField]
        private CapsuleCollider2D m_DefaultCollider;
        [SerializeField]
        private CapsuleCollider2D m_SlideCollider;

        private AudioSource m_Audio;
        [SerializeField] private AudioClip m_jumpClip;
        [SerializeField] private AudioClip m_slideClip;

        [SerializeField]
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.

        const float k_GroundedRadius = .3f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.

        private PlayerState m_PrevPlayerState;
        private PlayerState m_PlayerState;

        bool waitForJumpInputReset = false;
        bool delayGroundCheckForJump = false;
        float slideDuration = 0f;
        float slideCooldown = 0f;

        float currentSpeedMultipler = 1;
        float desiredSpeedMultipler = 1;
        float currentVelocity;

        [SerializeField]
        private ParticleSystem JumpParticles;
        [SerializeField]
        private ParticleSystem DoubleJumpParticles;
        [SerializeField]
        private ParticleSystem SlideParticles;

        private void SetPlayerState(PlayerState newState)
        {
            if (newState == m_PlayerState) return;
            m_PrevPlayerState = m_PlayerState;
            m_PlayerState = newState;
        }

        private void Start()
        {
            m_Audio = GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            if (slideCooldown > 0f)
            {
                slideCooldown -= Time.fixedDeltaTime;
            }

            if (slideDuration > 0f)
            {
                slideDuration -= Time.fixedDeltaTime;

                if (slideDuration <= 0f)
                {
                    StopSliding();
                }
            }

            m_Grounded = false;
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            if (!delayGroundCheckForJump)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i] != m_DefaultCollider && colliders[i] != m_SlideCollider)
                        m_Grounded = true;
                }
            }
            if (m_Grounded && m_PlayerState != PlayerState.Sliding)
            {
                SetPlayerState(PlayerState.Running);
            }
        }
        private IEnumerator DelayGroundCheckForJump()
        {
            delayGroundCheckForJump = true;
            yield return new WaitForSeconds(0.2f);
            delayGroundCheckForJump = false;
        }

        private void StartSliding()
        {
            SlideParticles.Play();
            m_Anim.SetInteger("JumpLevel", 0);
            m_SlideCollider.gameObject.SetActive(true);
            m_DefaultCollider.gameObject.SetActive(false);
            m_Anim.SetBool("Slide", true);
            SetPlayerState(PlayerState.Sliding);
            slideDuration = m_MaxSlideTime;
            m_Audio.pitch = 1f;
            m_Audio.PlayOneShot( m_slideClip );
        }

        private void StopSliding()
        {
            SlideParticles.Stop();
            m_DefaultCollider.gameObject.SetActive(true);
            m_SlideCollider.gameObject.SetActive(false);
            m_Anim.SetBool("Slide", false);
            SetPlayerState(PlayerState.Running);
            slideCooldown = m_SlideCooldownTime;
        }

        public void Move(bool slide, bool jump)
        {
            float moveSpeed = m_MoveSpeed;
            bool shouldRun = false;

            if (slide && m_PlayerState == PlayerState.Running && slideCooldown <= 0f)
            {
                StartSliding();
            }
            else if (!slide && m_PlayerState == PlayerState.Sliding)
            {
                StopSliding();
            }
            waitForJumpInputReset = waitForJumpInputReset && jump;
            // If the player should jump...
            if (m_Grounded && jump && (m_PlayerState == PlayerState.Running || m_PlayerState == PlayerState.Sliding))
            {
                if (m_PlayerState == PlayerState.Sliding)
                {
                    StopSliding();
                }

                JumpParticles.Play();
                SetPlayerState(PlayerState.Jumping);
                m_Grounded = false;
                m_Anim.SetInteger("JumpLevel", 1);
                var jumpVelocity = Mathf.Sqrt(2 * m_Rigidbody2D.gravityScale * m_JumpHeight);
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0) + Vector2.up * jumpVelocity;
                waitForJumpInputReset = true;
                m_Audio.pitch = 1f;
                m_Audio.PlayOneShot( m_jumpClip );
                StartCoroutine(DelayGroundCheckForJump());
            }
            else if (jump && !waitForJumpInputReset && (m_PlayerState == PlayerState.Jumping || (m_PlayerState == PlayerState.Running && !m_Grounded))) {
                DoubleJumpParticles.Play();
                SetPlayerState(PlayerState.DoubleJump);
                var jumpVelocity = Mathf.Sqrt(2 * m_Rigidbody2D.gravityScale * m_JumpHeight);
                m_Grounded = false;
                m_Anim.SetInteger("JumpLevel", 2);
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0) + Vector2.up * jumpVelocity;
                m_Audio.pitch = 1.5f;
                m_Audio.PlayOneShot(m_jumpClip);
                StartCoroutine(DelayGroundCheckForJump());
            }
            if (m_Grounded && m_PlayerState == PlayerState.Running)
            {
                shouldRun = true;
                m_Anim.SetInteger("JumpLevel", 0);
            }
            if (m_PrevPlayerState == PlayerState.Sliding && m_PlayerState == PlayerState.Running)
            {
                currentSpeedMultipler = Mathf.SmoothDamp(currentSpeedMultipler, 1, ref currentVelocity, 0.2f);
            }
            else if (m_PlayerState == PlayerState.Sliding)
            {
                currentSpeedMultipler = m_SlideSpeedMultiplier;
            }
            else
            {
                currentSpeedMultipler = 1;
            }
            ScrollingManager.SetSpeed(-moveSpeed * currentSpeedMultipler);
            m_Anim.SetBool("Running", shouldRun);
        }
    }
}