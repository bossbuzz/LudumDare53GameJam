namespace Script.Player_.StateMachineP
{
    public class JumpState : AirborneState
    {
        public override string Name => "JumpState";
        public override void EnterState(Player player)
        {
            player.VelocityY = player.minJumpVelocity;
        }
        
        
    }
}