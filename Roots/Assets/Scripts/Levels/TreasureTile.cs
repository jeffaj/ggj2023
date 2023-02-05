using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class TreasureTile : Tile
    {
        #region Inspector Fields

        [SerializeField]
        private Transform _particleSpawnPos = null;

        [Header("Prefabs")]

        [SerializeField]
        private GameObject _particleEffectPrefab = null;

        #endregion

        public ArtifactData ArtifactData { get; set; }

        public override bool IsPassable => true;

        public override void Interact()
        {
            AudioManager.Instance.PlayDirtBreak();
            AudioManager.Instance.PlayArtBreak();
            Instantiate(_particleEffectPrefab, _particleSpawnPos.position, Quaternion.identity);
        }

        public override void MovedIntoAfterDestroyed()
        {
            Game.OnInteractWithArtifact(this.ArtifactData);
        }
    }
}