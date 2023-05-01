using Script.Managers;
using UnityEngine;

namespace Script.Deliverables.StateMachineD
{
    public class ThrowingStateD : DeliverableState
    {
        private Vector2 direction;
        private Vector2 force;
        public float velocity;
        public float directionSignX;

        public void startThrow(Vector2 force_, Vector2 direction_)
        {
            force = force_;
            direction = direction_;
        }
        
        public override void EnterState(Deliverable deliverable)
        {
            deliverable.useTopSpeed = deliverable.topSpeed;
            Vector2 vel = force * direction;
            if (vel.y == 0) vel.y = 1;
            deliverable.VelocityY = vel.y;
            velocity = Mathf.Abs(vel.x);
            directionSignX = Mathf.Sign(vel.x);
        }

        public override void Update(Deliverable deliverable)
        {
            ForceDecay(deliverable);
            VerticalBounce(deliverable);
            GravityDecay(deliverable,3);
            DoMovement(deliverable);
            Transitions(deliverable);
        }

        private void ForceDecay(Deliverable deliverable)
        {
            velocity -= deliverable.throwDecay.x * TimeManager.DeltaTime;
            if (velocity <= 0) velocity = 0;
            if (Mathf.Abs(deliverable.VelocityY) <= 0.01) deliverable.VelocityY = 0;
            if (deliverable.Controller2D.collisions.right || deliverable.Controller2D.collisions.left)
            {
                directionSignX = -directionSignX;
                velocity /= deliverable.bounceDecay.x;
            }
            deliverable.VelocityX = velocity * directionSignX;
        }

        protected void GravityDecay(Deliverable deliverable,float multiplier = 1)
        {
            float gravValue = -deliverable.throwForce.y * TimeManager.DeltaTime * multiplier;
            deliverable.VelocityY += gravValue;
        }
        
        private void VerticalBounce(Deliverable deliverable)
        {
            if (deliverable.useTopSpeed.y <= 0.2f)
            {
                deliverable.VelocityY = 0;
                return;
            }
            if (deliverable.Controller2D.collisions.above)
            {
                deliverable.InvokeBounce();
                deliverable.VelocityY = -deliverable.VelocityY;
            }
            if (deliverable.Controller2D.collisions.below)
            {
                deliverable.InvokeBounce();
                if (deliverable.bounceDecay.y != 1)
                {
                    deliverable.VelocityY /= deliverable.bounceDecay.y;
                }
                deliverable.VelocityY = -deliverable.VelocityY;
                deliverable.useTopSpeed.y = Mathf.Abs(deliverable.VelocityY);
            }
        }

        private void Transitions(Deliverable deliverable)
        {
            if (deliverable.Controller2D.IsGrounded && Mathf.Abs(deliverable.VelocityY) <= 2f && Mathf.Abs(deliverable.VelocityX) <= 2f)
            {
                UnityEngine.Debug.Log("Landed");
                deliverable.velocity = Vector2.zero;
                deliverable.Controller2D.Move(Vector2.down,false);
                deliverable.SetState(deliverable.IdleState);
            }
        }
        
        public override void ExitState(Deliverable deliverable)
        {
            direction = Vector2.zero;
            velocity = 0;
            directionSignX = 0;
            deliverable.useTopSpeed = deliverable.topSpeed;
            deliverable.cantCrack = false;
        }
    }
}