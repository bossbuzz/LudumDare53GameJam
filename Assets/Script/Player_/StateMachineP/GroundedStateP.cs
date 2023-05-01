namespace Script.Player_.StateMachineP
{
    public class GroundedState : PlayerState
    {
        public override int Id => 0;
        public override string Name => "GroundedState";
        
        public override void EnterState(Player player)
        {
            player.DoubleJumps = player.maxDoubleJumps;
        }

        public override void OnUpdate(Player player)
        {
            InputMovement(player);
            GroundedGravityMovement(player);
            DoFlip(player);
            DoMovement(player);
            Transitions(player);
        }

        protected virtual void Transitions(Player player)
        {
            if (player.InputManager.PressedJump())
            {
                player.SetState(player.JumpState);
            }
            else if (!player.controller2D.IsGrounded)
            {
                player.SetState(player.FallingState);
            }
            else if (player.VelocityX != 0)
            {
                player.SetState(player.RunningState);
            }
        }
        
        public override void ExitState(Player player)
        {
            
        }
    }
}