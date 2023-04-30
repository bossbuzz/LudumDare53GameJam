using System;
using Script.Deliverables.StateMachineD;
using UnityEngine;

namespace Script.Deliverables
{
    public class Deliverable : MonoBehaviour,IDeliverableId
    {
        [SerializeField] private int _id;
        public Controller2D Controller2D;
        public Collider2D Collider2D;
        public Vector2 velocity;
        public Vector2 topSpeed;
        public Vector2 throwForce;
        public Vector2 throwDecay;
        public Vector2 bounceDecay = Vector2.one;
        private DeliverableState _currentState;
        public readonly GroundedStateD IdleState = new GroundedStateD();
        public readonly PickupStateD PickupStateD = new PickupStateD();
        public readonly ThrowingStateD ThrowingStateD = new ThrowingStateD();
        public bool spawnOnGround = false;
        public delegate void dvoid();
        public event dvoid onBounce;

        public int Id => _id;

        public Vector2 Velocity
        {
            get => velocity;
            set
            {
                velocity.x = Mathf.Clamp(value.x,-topSpeed.x,topSpeed.x);
                velocity.y = Mathf.Clamp(value.y,-topSpeed.y,topSpeed.y);
            }
        }
        
        public float VelocityY
        {
            get => velocity.y;
            set
            {
                velocity.y = Mathf.Clamp(value,-topSpeed.y,topSpeed.y);
            }
        }
        
        public float VelocityX
        {
            get => velocity.x;
            set
            {
                velocity.x = Mathf.Clamp(value,-topSpeed.x,topSpeed.x);
            }
        }
        
        private void Awake()
        {
            Controller2D = GetComponent<Controller2D>();
            Collider2D = GetComponent<Collider2D>();
        }

        private void Start()
        {
            if (spawnOnGround)
            {
                _currentState = IdleState;
                velocity = Vector2.zero;
                Controller2D.Move(Vector2.down,false);
            }
            else _currentState = ThrowingStateD;
        }

        private void OnDisable()
        {
            onBounce = null;
        }

        public void InvokeBounce()
        {
            onBounce?.Invoke();
        }
        
        public void SetState(DeliverableState nextState)
        {
            _currentState?.ExitState(this);
            _currentState = nextState;
            _currentState?.EnterState(this);
        }
        
        private void Update()
        {
            _currentState?.Update(this);
        }

        public void Pickup(Transform pivot)
        {
            Collider2D.enabled = false;
            var transform1 = transform;
            transform1.parent = pivot;
            transform1.localPosition = Vector2.zero;
            SetState(PickupStateD);
        }

        public void Drop(Vector2 position)
        {
            Collider2D.enabled = true;
            var transform1 = transform;
            transform1.parent = null;
            Vector2 dif = (Vector2) transform1.position - position;
            transform1.position = position;
            Controller2D.Move(dif,false);
            ThrowingStateD.startThrow(new Vector2(0,5),Vector2.up);
            SetState(ThrowingStateD);
        }
        
        public void Throw(Vector2 direction,Transform thrower)
        {
            Collider2D.enabled = true;
            var transform1 = transform;
            transform1.parent = null;
            transform1.position = thrower.position;
            Controller2D.Move(Vector2.up,false);
            if (direction == Vector2.zero)
            {
                direction = new Vector2(Mathf.Sign(thrower.localScale.x), 0);
            }
            ThrowingStateD.startThrow(throwForce,direction);
            SetState(ThrowingStateD);
        }

        public void Stomp()
        {
            VelocityY = -Mathf.Abs(VelocityY);
        }
    }
}