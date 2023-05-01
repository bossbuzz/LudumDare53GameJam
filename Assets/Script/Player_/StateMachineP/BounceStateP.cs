using Script.Managers;

namespace Script.Player_.StateMachineP
{
    public class BounceStateP : JumpState
    {
        public override void EnterState(Player player)
        {
            player.VelocityY = player.maxJumpVelocity;
        }
    }
}