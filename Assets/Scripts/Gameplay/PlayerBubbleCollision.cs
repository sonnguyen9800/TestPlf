using Platformer.Core;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a Bubble.
    /// </summary>
    /// <typeparam name="PlayerCollision"></typeparam>
    public class PlayerBubbleCollision : Simulation.Event<PlayerBubbleCollision>
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