using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public AudioClip walkAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        private bool doubleJump;
        private bool canDash = true;
        private bool isDashing;
        public float dashingPower = 6f;
        public float dashingTime = 0.2f;
        public float dashingCooldown = 1f;
        public bool invincible = false;
        public TrailRenderer tr;
        /*internal new*/
        public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        private Color initialPlayerColor;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            initialPlayerColor = spriteRenderer.color;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                if (isDashing) 
                {
                    return;
                }

                move.x = Input.GetAxis("Horizontal");

                if(Mathf.Abs(Input.GetAxis("Horizontal")) == 1 && jumpState == JumpState.Grounded)
                {
                    if(!audioSource.isPlaying) 
                    {
                        audioSource.PlayOneShot(walkAudio);
                    }
                }

                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;

                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }

                if(Input.GetButtonDown("Dash") && canDash) 
                {
                    StartCoroutine(Dash());
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    doubleJump = true;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if(doubleJump && Input.GetButtonDown("Jump")) 
                    {
                        Schedule<PlayerJumped>().player = this;
                        velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                        doubleJump = false;
                    }
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Lollipop")) 
            {
                StartCoroutine(InvincibilityTimer());
                Destroy(collision.gameObject);
            }
        }

        IEnumerator InvincibilityTimer() 
        {
            invincible = true;
            spriteRenderer.color = new Color(1f, 1f, 0f);
            maxSpeed = 4.5f;
            yield return new WaitForSeconds(8f);
            invincible = false;
            spriteRenderer.color = initialPlayerColor;
            maxSpeed = 3f;
        }

        IEnumerator Dash() 
        {
            canDash = false;
            isDashing = true;
            velocity.y = 1f;
            Vector2 originalVelocity = body.velocity;

            if (spriteRenderer.flipX) 
            {
                body.velocity = new Vector2(transform.localScale.x * dashingPower * -1, 0f);
            }
            else 
            {
                body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            }
           
            tr.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            body.velocity = originalVelocity;
            tr.emitting = false;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}