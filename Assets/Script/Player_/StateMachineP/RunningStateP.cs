namespace Script.Player_.StateMachineP
{
    public class RunningState : GroundedState
    {
        public override int Id => 1;
        public override string Name => "RunningState";

        protected override void Transitions(Player player)
        {
            if (player.InputManager.PressedJump())
            {
                player.SetState(player.JumpState);
            }
            else if (!player.controller2D.IsGrounded)
            {
                player.SetState(player.FallingState);
            }
            else if (player.VelocityX == 0)
            {
                player.SetState(player.GroundedState);
            }
        }
    }
}