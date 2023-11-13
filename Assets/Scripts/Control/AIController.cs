using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private float agrroCooldownTime = 5f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float patrolPointWaitTimeMax = 2f;
        private GameObject player;
        private Fighter fighter;
        private Health health;
        private Mover mover;
        private ActionScheduler actionScheduler;
        private Vector3 guardPosition;
        private Vector3 patrolPoint;
        private float timeSinceLastSawPlayer;
        private float timeSinceAggrevated;
        private int currentWaypointIndex = 0;
        private bool isAtPatrolPoint = false;
        private float patrolPointWaitTime = 0f;


        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }
        // Use this for initialization
        void Start()
        {
            guardPosition = transform.position;
            timeSinceLastSawPlayer = Mathf.Infinity;
            timeSinceAggrevated = Mathf.Infinity;
            currentWaypointIndex = 0;
            Health.OnTakeDamage += Aggravate;
        }

        private void Aggravate(float arg1, GameObject arg2)
        {
            timeSinceAggrevated = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDie())
                return;
            if(IsAggravated())
            {
                timeSinceLastSawPlayer = 0f;
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceAggrevated += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
            AggravateNearEnemies();
        }

        private void AggravateNearEnemies()
        {
            RaycastHit[] hits =  Physics.SphereCastAll(transform.position, 3f, Vector3.up, 0);
            foreach (RaycastHit hit in hits)
            {
                hit.collider.GetComponent<AIController>()?.Aggravate(0f,gameObject);
            }
        }

        //巡逻
        private void PatrolBehaviour()
        {
            
            if (patrolPath == null)
                return;
            if(isAtPatrolPoint)
            {
                patrolPointWaitTime += Time.deltaTime;
                if (patrolPointWaitTime >= patrolPointWaitTimeMax)
                {
                    patrolPointWaitTime = 0f;
                    isAtPatrolPoint = false;
                }
                return;
            }
            patrolPoint = patrolPath.GetWaypoint(currentWaypointIndex);
            
            if(AtWayPointRound())
            {
                isAtPatrolPoint = true;
                currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
            }
            mover.MoveTo(patrolPoint);
        }



        private bool AtWayPointRound()
        { 
            float distanceToWaypoint = Vector3.Distance(transform.position, patrolPoint);
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private bool IsAggravated()
        {
            return Vector3.Distance(player.transform.position, transform.position) <= chaseDistance || timeSinceAggrevated < agrroCooldownTime;
            
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}