using Platformer.Core;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class EmptyEvent : Simulation.Event<EmptyEvent>
    {
        public override void Execute()
        {
            Debug.LogError("Empty Event");
        }
    }
}