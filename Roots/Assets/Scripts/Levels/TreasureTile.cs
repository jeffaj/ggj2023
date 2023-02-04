using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class TreasureTile : Tile {

        #region Inspector Fields

        [Header("Prefabs")]

        [SerializeField]
        private GameObject _particleEffectPrefab = null;

        [Header("Children")]

        [SerializeField]
        private Transform _particleSpawnPos = null;

        #endregion

        public ArtifactData ArtifactData { get; set; }

        public override bool IsPassable => true;

        public override void Interact() {
            // launch ArtifactModelController
            Game.LaunchArtifactModal(this.ArtifactData);

            Instantiate(_particleEffectPrefab, _particleSpawnPos.position, Quaternion.identity);
        }

    }
}