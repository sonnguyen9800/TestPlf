using System.Collections;
using System.Collections.Generic;
using _Custom;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Unity.VisualScripting;
using UnityEngine.Serialization;

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
        public AudioClip flyAudio;

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
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();


        [Header("Flying Mechanics")] 
        private float _flyAcceleration;
        private float _maxFlySpeed;
        private float _descendSpeed;
        private Color _flyingColor = Color.yellow;
        private Color _originalColor;
        public bool _isFlying = false;

        
        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            _originalColor = spriteRenderer.color;
        }

        protected override void Start()
        {
            base.Start();
            
            health.SetupMaxHealth(ConfigManager.Instance.GetInitHealth());
            _flyAcceleration = ConfigManager.Instance.GetFlyAcceleration();
            _maxFlySpeed = ConfigManager.Instance.GetMaxFlySpeed();
            _descendSpeed = ConfigManager.Instance.GetDescendSpeed();
        }
        
        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                
                // Quick Test for flying
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ToggleFlying();
                }

                if (!_isFlying)
                {
                    if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                        jumpState = JumpState.PrepareToJump;
                    else if (Input.GetButtonUp("Jump"))
                    {
                        stopJump = true;
                        Schedule<PlayerStopJump>().player = this;
                    }
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        public void ToggleFlying(bool enable = true)
        {
            _isFlying = enable;
            if (_isFlying)
            {
                velocity.y = 0;
                spriteRenderer.color = _flyingColor;
                if (audioSource && flyAudio)
                    audioSource.PlayOneShot(flyAudio);
            }
            else
            {
                spriteRenderer.color = _originalColor;
                velocity.y = 0;
            }
        }

        void UpdateJumpState()
        {
            if (_isFlying)
            {
                HandleFlying();
            }
            else
            {
                HandleGround();
            }
        }

        private void HandleFlying()
        {
            // Handle flying mechanics
            if (Input.GetButtonUp("Jump"))
            {
                // Descending
                velocity.y = -_descendSpeed;
            }
            else
            {
                // Ascending with acceleration
                velocity.y = Mathf.Min(velocity.y + _flyAcceleration, _maxFlySpeed);
            }
        }

        private void HandleGround()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
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
            if (!_isFlying)
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
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                return;
            }
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && _isFlying)
            {
                // Return to normal state when hitting obstacles
                _isFlying = false;
                spriteRenderer.color = _originalColor;
                velocity.y = 0;
            }
 
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}