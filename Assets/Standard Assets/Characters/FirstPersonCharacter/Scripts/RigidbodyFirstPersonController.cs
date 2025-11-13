using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (CapsuleCollider))]
	[RequireComponent(typeof (AudioSource))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {
		[SerializeField] public AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
		[SerializeField] public AudioClip m_JumpSound;           // the sound played when character leaves the ground.
		[SerializeField] public AudioClip m_LandSound;           // the sound played when character touches back on ground.

		private AudioSource m_AudioSource;
		public bool underwater = false; // If true the footstep sounds are not played.
        private bool underwaterTriggered = false;

        public Animator animator;

        [Serializable]
        public class MovementSettings
        {
            public float ForwardSpeed = 8.0f;   // Speed when walking forward
            public float BackwardSpeed = 4.0f;  // Speed when walking backwards
            public float StrafeSpeed = 4.0f;    // Speed when walking sideways
            public float RunMultiplier = 2.0f;   // Speed when sprinting
	        public KeyCode RunKey = KeyCode.LeftShift;
            public float JumpForce = 30f;
            public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float CurrentTargetSpeed = 8f;

#if !MOBILE_INPUT
            public bool m_Running;
#endif

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
	            if (input == Vector2.zero) return;
				if (input.x > 0 || input.x < 0)
				{
					//strafe
					CurrentTargetSpeed = StrafeSpeed;
				}
				if (input.y < 0)
				{
					//backwards
					CurrentTargetSpeed = BackwardSpeed;
				}
				if (input.y > 0)
				{
					//forwards
					//handled last as if strafing and moving forward at the same time forwards speed should take precedence
					CurrentTargetSpeed = ForwardSpeed;
				}
#if !MOBILE_INPUT
	            if (Input.GetKey(RunKey))
	            {
		            CurrentTargetSpeed *= RunMultiplier;
		            m_Running = true;
	            }
	            else
	            {
		            m_Running = false;
	            }
#endif
            }

#if !MOBILE_INPUT
            public bool Running
            {
                get { return m_Running; }
            }
#endif
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
            public float stickToGroundHelperDistance = 0.5f; // stops the character
            public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }


        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();

        public float fuerzaAgua = 10.0f;

        private Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private Vector3 m_GroundContactNormal;
        private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;

        // Valores del FPS Controller a ser disminuidos bajo el agua.
        private float JumpForceO = 0.0f;
        private float JumpForceD = 0.0f;

        private float BackwardSpeedO = 0.0f;
        private float BackwardSpeedD = 0.0f;

        private float ForwardSpeedO = 0.0f;
        private float ForwardSpeedD = 0.0f;

        private float RunMultiplierO = 0.0f;
        private float RunMultiplierD = 0.0f;

        private float StrafeSpeedO = 0.0f;
        private float StrafeSpeedD = 0.0f;

        public float disminuirCapacidadesBajoElAgua = 0.5f;
        public float yUnderWaterOffset = 1.0f;

        private GameObject planoAgua;


        public Vector3 Velocity
        {
            get { return m_RigidBody.linearVelocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }

        public bool Jumping
        {
            get { return m_Jumping; }
        }

        public bool Running
        {
            get
            {
 #if !MOBILE_INPUT
				return movementSettings.Running;
#else
	            return false;
#endif
            }
        }


        private void Start()
        {
            this.JumpForceO = this.movementSettings.JumpForce;
            this.JumpForceD = this.JumpForceO * this.disminuirCapacidadesBajoElAgua;

            this.BackwardSpeedO = this.movementSettings.BackwardSpeed;
            this.BackwardSpeedD = this.BackwardSpeedO * this.disminuirCapacidadesBajoElAgua;

            this.ForwardSpeedO = this.movementSettings.ForwardSpeed;
            this.ForwardSpeedD = this.ForwardSpeedO * this.disminuirCapacidadesBajoElAgua;

            this.RunMultiplierO = this.movementSettings.RunMultiplier;
            this.RunMultiplierD = this.RunMultiplierO * this.disminuirCapacidadesBajoElAgua;

            this.StrafeSpeedO = this.movementSettings.StrafeSpeed;
            this.StrafeSpeedD = this.StrafeSpeedO * this.disminuirCapacidadesBajoElAgua;

            this.planoAgua = GameObject.FindWithTag("PlanoAgua");

            m_RigidBody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
			m_AudioSource = GetComponent<AudioSource>();
            mouseLook.Init (transform, cam.transform);
        }


        private void Update()
        {
            RotateView();

            if (CrossPlatformInputManager.GetButtonDown("Jump") && !m_Jump)
            {
                m_Jump = true;
            }
        }

        /**
        * Aplica los valores disminuidos en el porcentaje de disminucion dado.
        */
        void reducirSalto() {
            this.underwater = true;
            this.movementSettings.JumpForce = this.JumpForceD;
            this.movementSettings.BackwardSpeed = this.BackwardSpeedD;
            this.movementSettings.ForwardSpeed = this.ForwardSpeedD;
            this.movementSettings.RunMultiplier = this.RunMultiplierD;
            this.movementSettings.StrafeSpeed = this.StrafeSpeedD;
        }

        /**
        * Restaura los valores originales configurados para el FPS Controller. 
        */
        void dejarSaltoOriginal() {
            this.underwater = false;
            this.movementSettings.JumpForce = this.JumpForceO;
            this.movementSettings.BackwardSpeed = this.BackwardSpeedO;
            this.movementSettings.ForwardSpeed = this.ForwardSpeedO;
            this.movementSettings.RunMultiplier = this.RunMultiplierO;
            this.movementSettings.StrafeSpeed = this.StrafeSpeedO;
        }

        /**
         * Set the appropiate triggers to enter into the water animations states.
         */
        private void checkFlagsToTriggerUnderWaterAnimations() {
            // The rigidbody is under water?
            underwater = ((this.getPosition().y + yUnderWaterOffset) < this.planoAgua.transform.position.y);
            if (underwater) {
                if (!underwaterTriggered) {
                    underwaterTriggered = true;
                    this.animator.SetTrigger("Swimming");
                }
            } else {
                if (underwaterTriggered) {
                    underwaterTriggered = false;
                    this.animator.SetBool("isSwimmingNormal", false);
                    this.animator.SetBool("isSwimmingFast", false);
                    this.animator.SetTrigger("OutOfWater");
                }
            }
        }


        private void FixedUpdate()
        {
            GroundCheck();
            Vector2 input = GetInput();

            checkFlagsToTriggerUnderWaterAnimations();

            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
            {
                if (!underwater) {
                    this.animator.SetBool("isWalking", true);
                } else {
                    this.animator.SetBool("isSwimmingNormal", true);
                }
                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = cam.transform.forward*input.y + cam.transform.right*input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

                desiredMove.x = desiredMove.x*movementSettings.CurrentTargetSpeed;
                desiredMove.z = desiredMove.z*movementSettings.CurrentTargetSpeed;
                desiredMove.y = desiredMove.y*movementSettings.CurrentTargetSpeed;
                if (m_RigidBody.linearVelocity.sqrMagnitude <
                    (movementSettings.CurrentTargetSpeed*movementSettings.CurrentTargetSpeed))
                {
                    m_RigidBody.AddForce(desiredMove*SlopeMultiplier(), ForceMode.Impulse);
					PlayFootStepAudio ();
                    
                }

                //this.cam.transform.position = m_RigidBody.position;
            } else {
                if (!underwater) {
                    this.animator.SetBool("isWalking", false);
                } else {
                    this.animator.SetBool("isSwimmingNormal", false);
                }
            }

            if (!underwater) {
                if (this.m_RigidBody.linearVelocity == Vector3.zero) {
                    this.movementSettings.m_Running = false;
                }
                this.animator.SetBool("isRunning",this.movementSettings.m_Running);
            } else {
                
                if (this.movementSettings.m_Running) {
                    this.animator.SetBool("isSwimmingNormal", false);
                }
                this.animator.SetBool("isSwimmingFast",this.movementSettings.m_Running);
            }

            if (!underwater) {
                Physics.gravity = new Vector3(0, -9.8f, 0);
                dejarSaltoOriginal();
            } else {
                Physics.gravity = new Vector3(0, -5.8f, 0);
                reducirSalto ();
                m_RigidBody.AddForce(transform.up * fuerzaAgua);
                //rigidBody.useGravity = false;
                //https://answers.unity.com/questions/11754/how-to-make-water-swimmable.html
            }

            if (m_IsGrounded)
            {
                m_RigidBody.linearDamping = 5f;

                if (m_Jump && !underwater)
                {
                    m_RigidBody.linearDamping = 0f;
                    m_RigidBody.linearVelocity = new Vector3(m_RigidBody.linearVelocity.x, 0f, m_RigidBody.linearVelocity.z);
                    m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                    m_Jumping = true;
                    animator.SetBool("isJumping",true);
					PlayJumpSound();
                }

                if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.linearVelocity.magnitude < 1f)
                {
                    m_RigidBody.Sleep();
                }
            }
            else
            {
                m_RigidBody.linearDamping = 0f;
                if (m_PreviouslyGrounded && !m_Jumping)
                {
                    StickToGroundHelper();
                }
            }
            m_Jump = false;
        }

        public void stopAllForces() {
            m_RigidBody.linearVelocity = Vector3.zero;
            m_RigidBody.angularVelocity = Vector3.zero;
        }

        public Vector3 getPosition() {
            return m_RigidBody.position;
        }

		private void PlayFootStepAudio()
		{
			if (underwater){
				return;
			}

			if (!m_IsGrounded)
			{
				return;
			}
			if (m_AudioSource.isPlaying) {
				return;
			}
			// pick & play a random footstep sound from the array,
			// excluding sound at index 0
			int n = Random.Range(1, m_FootstepSounds.Length);
			m_AudioSource.clip = m_FootstepSounds[n];
			m_AudioSource.PlayOneShot(m_AudioSource.clip);
			// move picked sound to index 0 so it's not picked next time
			m_FootstepSounds[n] = m_FootstepSounds[0];
			m_FootstepSounds[0] = m_AudioSource.clip;
		}

		private void PlayJumpSound()
		{
			if (underwater){
				return;
			}
			m_AudioSource.clip = m_JumpSound;
			m_AudioSource.Play();
		}

		private void PlayLandingSound()
		{
			if (underwater){
				return;
			}
			m_AudioSource.clip = m_LandSound;
			m_AudioSource.Play();
		}


        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }


        private void StickToGroundHelper()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) +
                                   advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
                {
                    m_RigidBody.linearVelocity = Vector3.ProjectOnPlane(m_RigidBody.linearVelocity, hitInfo.normal);
                }
            }
        }


        private Vector2 GetInput()
        {
            
            Vector2 input = new Vector2
                {
                    x = CrossPlatformInputManager.GetAxis("Horizontal"),
                    y = CrossPlatformInputManager.GetAxis("Vertical")
                };
			movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation (transform, cam.transform);

            if (m_IsGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                m_RigidBody.linearVelocity = velRotation*m_RigidBody.linearVelocity;
            }
        }

        public void LateUpdate()
        {
            Transform transformSpine = GameObject.FindGameObjectWithTag("PlayerSpine").transform;
            transformSpine.localEulerAngles = new Vector3(
                cam.transform.eulerAngles.x,
                transformSpine.localEulerAngles.y,
                transformSpine.localEulerAngles.z
            );
        }

        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_IsGrounded = true;
                m_GroundContactNormal = hitInfo.normal;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundContactNormal = Vector3.up;
            }
            if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
            {
                m_Jumping = false;
                animator.SetBool("isJumping",false);
				PlayLandingSound ();
            }
        }
    }
}
