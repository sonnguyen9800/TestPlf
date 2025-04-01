using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    public class PlayerGroundCollision : Simulation.Event<PlayerGroundCollision>
    {
        public PlayerController player;

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}