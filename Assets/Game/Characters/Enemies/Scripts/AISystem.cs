﻿using System.Diagnostics.CodeAnalysis;
using Game.Characters.Player.Scripts;
using Game.Characters.Scripts;
using Game.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;


namespace Game.Characters.Enemies
{
    /// <summary>
    /// This system using State Machine pattern. In current version there are 4 states:
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///             <see cref="EnemyState.ATTACKING"/>. If target inside <see cref="WeaponConfig.AttackRange"/> it will be attacked.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="EnemyState.CHASING"/>. If target inside <see cref="aggroRadius"/> it will be chased.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="EnemyState.IDLE"/>. Only used if there is no <see cref="patrolPath"/>. If there is no target inside <see cref="aggroRadius"/>, character will do nothing. Otherwise it will chase target.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="EnemyState.PATROLLING"/>. Only used if there is <see cref="patrolPath"/>. If there is no target inside <see cref="aggroRadius"/>, character will move from one patrol waypoint to another. Otherwise it will chase target.
    ///         </description>
    ///     </item>
    /// </list>
    /// For now "target" means <see cref="PlayerActor"/>.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class AISystem : MonoBehaviour
    {
        #region Editor tweakable fields

        [CanBeNull]
        [SerializeField]
        [Tooltip("The way that the enemy will patrol. Optional.")]
        private PatrolPath patrolPath;

        [SerializeField]
        [Tooltip("Time, the character will wait until move to another waypoint")]
        private float patrolPause = 5f;

        [SerializeField]
        [Tooltip("Distance, within enemy will chase player")]
        private float aggroRadius = 5f;

        [Tooltip("Distance, after which target waypoint considered as reached")]
        [SerializeField]
        private float waypointTolerance = 1f;

        #endregion

        #region Private fields

        [NotNull]
        private StateMachine<EnemyState> stateMachine;

        [NotNull]
        private PlayerActor player;

        [NotNull]
        private WeaponSystem weaponSystem;

        [NotNull]
        private NavMeshAgent navigationAgent;

        private Vector3 initialPosition;

        [NotNull]
        private IDamageable self;

        [NotNull]
        private AIMovement aiMovement;

        private float patrolWaypointReachedTime = 0;
        private bool isPatrolWaypointReached = false;

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            stateMachine = new StateMachine<EnemyState>();
            weaponSystem = GetComponent<WeaponSystem>();
            navigationAgent = GetComponent<NavMeshAgent>();
            initialPosition = transform.position;
            self = GetComponent<IDamageable>();
            aiMovement = GetComponent<AIMovement>();
            player = FindObjectOfType<PlayerActor>();
        }

        // Use this for initialization
        void Start()
        {
            stateMachine.AddState(EnemyState.IDLE, OnIdleStart, OnIdleUpdate);
            stateMachine.AddState(EnemyState.ATTACKING, OnAttackStart, OnAttackUpdate, OnAttackStop);
            stateMachine.AddState(EnemyState.PATROLLING, OnPatrolStart, OnPatrolUpdate);
            stateMachine.AddState(EnemyState.CHASING, onUpdate:OnChasingUpdate);

            SetInitialState();
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
        }

        private void OnDrawGizmos()
        {
            // draw aggro radius
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aggroRadius);
        }

        #endregion

        #region Private methods

        private void SetInitialState()
        {
            if (IsPlayerWithinAttackRadius())
            {
                stateMachine.CurrentState = EnemyState.ATTACKING;
            }
            else if (IsPlayerWithinAggroRadius())
            {
                stateMachine.CurrentState = EnemyState.CHASING;
            }
            else
            {
                SwitchToPatrolOrIdleState();
            }
        }

        private void OnIdleStart()
        {
            if (self.IsAlive())
            {
                aiMovement.SetDestination(initialPosition);
            }
        }

        private void OnIdleUpdate()
        {
            if (self.IsAlive() &&
                IsPlayerWithinAggroRadius() 
                && player.IsAlive())
            {
                stateMachine.CurrentState = EnemyState.CHASING;
            }
        }

        private void OnAttackStart()
        {
            weaponSystem.SetTarget(player);
        }

        private void OnAttackUpdate()
        {
            if (IsPlayerWithinAttackRadius())
            {
                if (!player.IsAlive())
                {
                    SwitchToPatrolOrIdleState();
                }
                else if (!self.IsAlive())
                {
                    stateMachine.CurrentState = EnemyState.IDLE;
                }
                else
                {
                    weaponSystem.Attack();
                }
            }
            else
            {
                stateMachine.CurrentState = EnemyState.CHASING;
            }
        }

        private void OnAttackStop()
        {
            weaponSystem.SetTarget(null);
        }

        private void OnPatrolStart()
        {
            aiMovement.SetDestination(patrolPath.GetTargetWaypoint());
        }

        private void OnPatrolUpdate()
        {
            if (IsPlayerWithinAggroRadius() && player.IsAlive())
            {
                stateMachine.CurrentState = EnemyState.CHASING;
                return;
            }

            if (isPatrolWaypointReached)
            {
                if (Time.time - patrolWaypointReachedTime >= patrolPause)
                {
                    patrolPath.OnTargetWaypointReached();
                    aiMovement.SetDestination(patrolPath.GetTargetWaypoint());
                    isPatrolWaypointReached = false;
                }
            }
            else
            {
                if (Vector3.Distance(patrolPath.GetTargetWaypoint(), transform.position) <= waypointTolerance)
                {
                    isPatrolWaypointReached = true;
                    patrolWaypointReachedTime = Time.time;
                }
            }
        }

        private void OnChasingUpdate()
        {
            if (IsPlayerWithinAttackRadius())
            {
                navigationAgent.isStopped = true;
                navigationAgent.ResetPath();
                stateMachine.CurrentState = EnemyState.ATTACKING;
            }
            else if (IsPlayerWithinAggroRadius())
            {
                aiMovement.SetDestination(player.transform.position, waypointTolerance);
            }
            else
            {
                SwitchToPatrolOrIdleState();
            }
        }

        private void SwitchToPatrolOrIdleState()
        {
            stateMachine.CurrentState = patrolPath != null ? EnemyState.PATROLLING : EnemyState.IDLE;
        }

        private bool IsPlayerWithinAggroRadius()
        {
            return Vector3.Distance(player.transform.position, transform.position) <= aggroRadius;
        }

        private bool IsPlayerWithinAttackRadius()
        {
            return Vector3.Distance(player.transform.position, transform.position) <= weaponSystem.GetWeaponAttackRadius();
        }

        #endregion
    }

    enum EnemyState
    {
        IDLE,
        ATTACKING,
        PATROLLING,
        CHASING
    }
}