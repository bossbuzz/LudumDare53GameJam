using UnityEngine;

namespace Script.Deliverables.StateMachineD
{
    public class PickupStateD : DeliverableState
    {
        public override void EnterState(Deliverable deliverable)
        {
            deliverable.velocity = Vector2.zero;
        }

        public override void Update(Deliverable deliverable)
        {
            
        }

        public override void ExitState(Deliverable deliverable)
        {
            
        }
    }
}