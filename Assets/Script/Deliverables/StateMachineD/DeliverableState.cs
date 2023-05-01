using Script.Managers;
using UnityEngine;

namespace Script.Deliverables.StateMachineD
{
    public abstract class DeliverableState
    {
        public abstract void EnterState(Deliverable deliverable);
        public abstract void Update(Deliverable deliverable);
        public abstract void ExitState(Deliverable deliverable);
        
        protected void GravityDecay(Deliverable deliverable,float multiplier = 1)
        {
            float gravValue = -deliverable.throwForce.y * TimeManager.DeltaTime * multiplier;
            deliverable.VelocityY += gravValue;
        }
        
        protected void GroundedGravityMovement(Deliverable deliverable)
        {
            float gravityMovement = -0.01f;
            deliverable.VelocityY = gravityMovement;
        }
        
        protected void DoMovement(Deliverable deliverable)
        {
            deliverable.Controller2D.Move(deliverable.Velocity * TimeManager.DeltaTime,false);
        }
        
    }
}