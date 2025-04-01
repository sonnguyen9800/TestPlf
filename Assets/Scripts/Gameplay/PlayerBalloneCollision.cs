using Platformer.Core;
using Platformer.Model;

namespace Platformer.Gameplay
{
    public class PlayerBalloneCollision : Simulation.Event<PlayerBalloneCollision>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            // Make character floating
            var player = model.player;
            player.ToggleFlying();
        }
    }
}