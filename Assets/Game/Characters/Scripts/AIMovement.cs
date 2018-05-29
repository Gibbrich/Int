using Game.Characters.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters
{
    public class AIMovement : MonoBehaviour
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
        private float movingTurnSpeed = 360;

        [SerializeField]
        private float stationaryTurnSpeed = 180;

        #endregion

        #region Fields

        private IDamageable self;
        private NavMeshAgent agent;
        private Animator animator;
        private new Rigidbody rigidbody;

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            self = GetComponent<IDamageable>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            MoveNavMesh();
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

        private void MoveNavMesh()
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

        public void Move(Vector3 movement)
        {
            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }

            movement = transform.InverseTransformDirection(movement);
            animator.SetFloat("Forward", movement.z * animatorForwardCap, 0.1f, Time.deltaTime);
            
            float turnAmount = Mathf.Atan2(movement.x, movement.z);
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, movement.z);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        }

        #endregion
    }
}