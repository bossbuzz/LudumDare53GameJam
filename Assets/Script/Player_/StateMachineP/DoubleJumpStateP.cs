namespace Script.Player_.StateMachineP
{
    public class DoubleJumpState : JumpState
    {
        public override string Name => "DoubleJumpState";

        public override void EnterState(Player player)
        {
            player.DoubleJumps--;
            base.EnterState(player);
        }
    }
}