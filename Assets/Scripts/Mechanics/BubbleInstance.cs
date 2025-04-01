using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class contains the data required for implementing token collection mechanics.
    /// It does not perform animation of the token, this is handled in a batch by the 
    /// TokenController in the scene.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class BubbleInstance : MonoBehaviour
    {
        internal bool collected = false;
        internal SpriteRenderer _renderer;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                return;
            }
            OnPlayerEnter();
        }

        void OnPlayerEnter()
        {
            if (collected) return;
            collected = true;
            _renderer.enabled = false;
           Schedule<PlayerBubbleCollision>();
           GUIManager.Instance.SpawnText(GUIManager.EffectType.Fly,  transform.position, transform);

        }
        
    }
}