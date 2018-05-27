using Game.Characters.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters
{
    public class Character : MonoBehaviour
    {
        public const float INTERACTABLE_STOPPING_DISTANCE = 2f;
        public const float WALKABLE_STOPPING_DISTANCE = 0.5f;

        #region Editor tweakable fields

        [SerializeField]
        [Range(0, 1)]
        [Tooltip("Used for limitation limb amplitude during character movement")]
        private float animatorForwardCap = 1f;

        [Header("Movement")]
        [SerializeField]
        private float stoppingDistance = 1f;

        [SerializeField]
        private float moveSpeedMultiplier = 1f;

        [SerializeField]
        private float animationSpeedMultiplier = 1.5f;

        [SerializeField]
        private float movingTurnSpeed = 360;

        [SerializeField]
        private float stationaryTurnSpeed = 180;

        [SerializeField]
        private float steeringSpeed = 1.0f;

        #endregion

        #region Fields

        private IDamageable self;
        private NavMeshAgent agent;
        private Animator animator;
        private new Rigidbody rigidbody;
        float turnAmount;
        float forwardAmount;
        Vector3 groundNormal;

        #endregion

        #region Unity callbacks

        private void Start()
        {
            self = GetComponent<IDamageable>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (self.IsAlive())
            {
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    Move(agent.desiredVelocity);
                }
                else
                {
                    Move(Vector3.zero);
                }
            }
        }

        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (Time.deltaTime > 0)
            {
                Vector3 velocity = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

                // we preserve the existing y part of the current velocity.
                velocity.y = rigidbody.velocity.y;
                rigidbody.velocity = velocity;
            }
        }

        #endregion

        #region Public methods

        public void SetDestination(Vector3 worldPosition, float stoppingDistance = WALKABLE_STOPPING_DISTANCE)
        {
            agent.stoppingDistance = stoppingDistance;
            agent.destination = worldPosition;
        }

        #endregion

        #region Private methods

        public void Move(Vector3 movement)
        {
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }

            movement = transform.InverseTransformDirection(movement);
            movement = Vector3.ProjectOnPlane(movement, groundNormal);
            turnAmount = Mathf.Atan2(movement.x, movement.z);
            forwardAmount = movement.z;

            ApplyExtraTurnRotation();

            // send input and other state parameters to the animator
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            // update the animator parameters
            animator.SetFloat("Forward", forwardAmount * animatorForwardCap, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        }

        private void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }

        #endregion
    }
}