using Script.Managers;

namespace Script.Player_.StateMachineP
{
    public class AirborneState : PlayerState
    {
        public override int Id => 2;
        public override string Name => "AirborneState";

        public override void EnterState(Player player)
        {
            
        }

        public override void OnUpdate(Player player)
        {
            InputMovement(player);
            CeilingBump(player);
            GravityMovement(player);
            DoFlip(player);
            DoMovement(player);
            Transitions(player);
        }

        public override void ExitState(Player player)
        {
            
        }

        private void CeilingBump(Player player)
        {
            if (player.VelocityY > 0 && player.controller2D.collisions.above) player.VelocityY = 0;
        }
        
        private void Transitions(Player player)
        {
            if (player.controller2D.IsGrounded)
            {
                Land();
                player.SetState(player.GroundedState);
            }
            else if (player.CanDoubleJump && player.InputManager.PressedJump())
            {
                player.SetState(player.DoubleJumpState);
            }
        }

        private void Land()
        {
            AudioManager.PlayClip(AudioManager.LandClip);
        }
    }
}