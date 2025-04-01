using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            if (player.health.IsAlive)
            {
                
                var currentHealth = player.health.Decrement();
                
                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);
                player.animator.SetTrigger("hurt");
                
                if (currentHealth == 0)
                {
                    model.virtualCamera.m_Follow = null;
                    model.virtualCamera.m_LookAt = null;
                    player.animator.SetBool("dead", true);
                    Simulation.Schedule<PlayerSpawn>(2);
                    player.controlEnabled = false;

                }
                GUIManager.Instance.SetHearth(currentHealth);

            }
            else
            {
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                player.animator.SetBool("dead", true);
                Simulation.Schedule<PlayerSpawn>(2);
                player.controlEnabled = false;
                GUIManager.Instance.SetHearth(0);
            }
        }
    }
}