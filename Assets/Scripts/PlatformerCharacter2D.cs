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

    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MoveSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpHeight = 1000f;                  // Amount of force added when the player jumps.
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private float m_MaxSlideTime = 1f;
        [SerializeField] private float m_SlideSpeedMultiplier = 1.5f;
        [SerializeField] private Vector2 m_SlideCapsuleSize = new Vector2(4, 4);
        [SerializeField] private float m_MinJumpTime = 0.2f;

        [SerializeField] private Animator m_Anim;            // Reference to the player's animator component.
        [SerializeField] private Rigidbody2D m_Rigidbody2D;

        [SerializeField] private CapsuleCollider2D m_DefaultCollider;
        [SerializeField] private CapsuleCollider2D m_SlideCollider;

        [SerializeField] private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.

        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        
        private PlayerState m_PlayerState;

        bool waitForJumpReset = false;
        bool delayGroundCheckForJump = false;

        private void FixedUpdate()
        {
            m_Grounded = false;
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            if (!delayGroundCheckForJump)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                        m_Grounded = true;
                }
            }
            if(m_Grounded && m_PlayerState != PlayerState.Sliding)
            {
                m_PlayerState = PlayerState.Running;
            }
        }
        private IEnumerator DelayGroundCheckForJump()
        {
            delayGroundCheckForJump = true;
            yield return new WaitForSeconds(0.2f);
            delayGroundCheckForJump = false;
        }

        private IEnumerator StopSliding()
        {
            yield return new WaitForSeconds(m_MaxSlideTime);
            HandleSliding(false);
        }

        private void HandleSliding(bool start)
        {
            if(start)
            {
                m_DefaultCollider.gameObject.SetActive(false);
                m_SlideCollider.gameObject.SetActive(true);
                m_Anim.SetBool("Slide", true);
                m_PlayerState = PlayerState.Sliding;
                StartCoroutine("StopSliding");
            }
            else
            {
                m_SlideCollider.gameObject.SetActive(false);
                m_DefaultCollider.gameObject.SetActive(true);
                m_Anim.SetBool("Slide", false);
                m_PlayerState = PlayerState.Running;
                StopCoroutine("StopSliding");
            }
        }
        
        public void Move(bool slide, bool jump)
        {
            float moveSpeed = m_MoveSpeed;
            m_Anim.SetBool("Running", false);

            if (slide && m_PlayerState == PlayerState.Running)
            {
                HandleSliding(true);
            }
            else if(!slide && m_PlayerState == PlayerState.Sliding)
            {
                HandleSliding(false);
            }
            if(m_PlayerState == PlayerState.Sliding)
            {
                moveSpeed *= m_SlideSpeedMultiplier;
            }
            waitForJumpReset = waitForJumpReset && jump;
            // If the player should jump...
            if (m_Grounded && jump && (m_PlayerState == PlayerState.Running || m_PlayerState == PlayerState.Sliding))
            {
                m_PlayerState = PlayerState.Jumping;
                var jumpVelocity = Mathf.Sqrt(2 * m_Rigidbody2D.gravityScale * m_JumpHeight);
                m_Grounded = false;
                m_Anim.SetInteger("JumpLevel", 1);
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0) + Vector2.up * jumpVelocity;
                waitForJumpReset = true;
                StartCoroutine(DelayGroundCheckForJump());
            }
            else if (jump && !waitForJumpReset && m_PlayerState == PlayerState.Jumping)
            {
                m_PlayerState = PlayerState.DoubleJump;
                var jumpVelocity = Mathf.Sqrt(2 * m_Rigidbody2D.gravityScale * m_JumpHeight);
                m_Grounded = false;
                m_Anim.SetInteger("JumpLevel", 2);
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0) + Vector2.up * jumpVelocity;
                StartCoroutine(DelayGroundCheckForJump());
            }
            if (m_Grounded && m_PlayerState == PlayerState.Running)
            {
                m_Anim.SetBool("Running", true);
                m_Anim.SetInteger("JumpLevel", 0);
            }
            ScrollingManager.SetSpeed(-moveSpeed);

        }
    }
}
