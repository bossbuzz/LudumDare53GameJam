using Script.Managers;

namespace Script.Player_.StateMachineP
{
    public class JumpState : AirborneState
    {
        public override string Name => "JumpState";
        public override void EnterState(Player player)
        {
            player.Animator.SetTrigger("flap");
            AudioManager.PlayClip(AudioManager.FlapClip);
            player.VelocityY = player.minJumpVelocity;
        }
        
        
    }
}