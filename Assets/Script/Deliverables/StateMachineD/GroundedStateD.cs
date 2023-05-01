namespace Script.Deliverables.StateMachineD
{
    public class GroundedStateD : DeliverableState
    {
        public override void EnterState(Deliverable deliverable)
        {
            
        }

        public override void Update(Deliverable deliverable)
        {
            GroundedGravityMovement(deliverable);
            DoMovement(deliverable);
            Transitions(deliverable);
        }

        private void Transitions(Deliverable deliverable)
        {
            if (!deliverable.Controller2D.IsGrounded)
            {
                deliverable.SetState(deliverable.ThrowingStateD);
            }
        }
        
        public override void ExitState(Deliverable deliverable)
        {
            
        }
    }
}