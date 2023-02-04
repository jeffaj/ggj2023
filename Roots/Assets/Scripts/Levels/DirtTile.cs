using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class DirtTile : Tile {

        #region Inspector Fields

        [Header("Prefabs")]

        [SerializeField]
        private GameObject _particleEffectPrefab = null;

        [Header("Children")]

        [SerializeField]
        private Transform _particleSpawnPos = null;

        #endregion

        public override bool IsPassable => true;

        public override void Interact() {
            AudioManager.Instance.PlayDirtBreak();
            Instantiate(_particleEffectPrefab, _particleSpawnPos.position, Quaternion.identity);
        }

    }
}