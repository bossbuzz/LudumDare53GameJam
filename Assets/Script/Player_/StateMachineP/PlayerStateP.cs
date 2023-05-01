using Script.Debug;
using Script.Deliverables;
using Script.Managers;
using UnityEngine;

namespace Script.Player_.StateMachineP
{
    public abstract class PlayerState
    {
        public abstract int Id
        {
            get;
        }
        
        public abstract string Name
        {
            get;
        }
        
        public abstract void EnterState(Player player);

        public void Update(Player player)
        {
            Throw(player);
            Grab(player);
            OnUpdate(player);
        }
        
        public abstract void OnUpdate(Player player);

        public abstract void ExitState(Player player);
        
        protected void InputMovement(Player player)
        {
            float velocity = player.InputManager.DirectionalInput().x;
            velocity *= player.speed;
            player.VelocityX = velocity;
        }
        
        protected void GravityMovement(Player player)
        {
            float gravValue = player.gravity * TimeManager.DeltaTime;
            player.VelocityY += gravValue;
        }

        protected void GroundedGravityMovement(Player player)
        {
            float gravityMovement = -0.01f;
            player.VelocityY = gravityMovement;
        }

        protected void DoMovement(Player player)
        {
            player.controller2D.Move(player.Velocity * TimeManager.DeltaTime,false);
        }

        protected void DoFlip(Player player)
        {
            if (Mathf.Sign(player.VelocityX) != Mathf.Sign(player.transform.localScale.x) && player.VelocityX != 0)
            {
                var transform = player.transform;
                Vector3 scale = transform.localScale;
                scale.x = -scale.x;
                transform.localScale = scale;
            }
        }

        protected void Throw(Player player)
        {
            if (player.InputManager.PressedThrow())
            {
                player.GrabModule.Throw(player.InputManager.DirectionalInput());
            }
        }
        
        protected void Grab(Player player)
        {
            if (player.InputManager.PressedGrab())
            {
                player.GrabModule.Grab();
            }
        }

        
        
    }
}