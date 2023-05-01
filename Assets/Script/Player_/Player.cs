using Script.Debug;
using Script.Deliverables;
using Script.Player_.StateMachineP;
using UnityEngine;

namespace Script.Player_
{
    public class Player : MonoBehaviour , IDeliverableId
    {
        public Controller2D controller2D;
        public readonly InputManager InputManager = new InputManager(false);
        public GrabModule GrabModule = new GrabModule();
        public StompModule StompModule;
        [SerializeField] private Vector2 velocity;
        [SerializeField] public float speed;
        [HideInInspector] public float gravity;
        [SerializeField] public float maxJumpHeight;
        [SerializeField] public float minJumpHeight;
        [SerializeField] public float timeToJumpApex;
        [HideInInspector] public float minJumpVelocity;
        public float maxJumpVelocity;
        [SerializeField] public Vector2 topSpeed;
        public bool canDoubleJump;
        public PlayerState CurrentState;
        [HideInInspector] public Animator Animator; 
        public readonly AirborneState FallingState = new AirborneState();
        public readonly JumpState JumpState = new JumpState();
        public readonly GroundedState GroundedState = new GroundedState();
        public readonly RunningState RunningState = new RunningState();
        public readonly DoubleJumpState DoubleJumpState = new DoubleJumpState();
        public readonly BounceStateP BounceStateP = new BounceStateP();
        
        public int Id => 0;
        
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

        public bool CanDoubleJump
        {
            get => canDoubleJump && !GrabModule.IsCarryingObject;
            set => canDoubleJump = value;
        }

        private void Awake()
        {
            canDoubleJump = true;
            Animator = GetComponent<Animator>();
            controller2D = GetComponent<Controller2D>();
            StompModule = GetComponentInChildren<StompModule>();
            SetState(FallingState);
            gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
            maxJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * maxJumpHeight);
        }

        public void SetState(PlayerState newState)
        {
            CurrentState?.ExitState(this);
            CurrentState = newState;
            DebugUI.DisplayState(CurrentState);
            CurrentState?.EnterState(this);
            Animator.SetInteger("Id",CurrentState.Id);
        }
        
        private void Update()
        {
            CurrentState.Update(this);   
        }
    }
}