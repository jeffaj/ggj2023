using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class DirtTile : Tile {

        #region Inspector Fields

        [SerializeField]
        private Transform _particleSpawnPos = null;

        [Header("Prefabs")]

        [SerializeField]
        private GameObject _particleEffectPrefab = null;

        #endregion

        public override bool IsPassable => true;

        public override void Interact() {
            AudioManager.Instance.PlayDirtBreak();
            Instantiate(_particleEffectPrefab, _particleSpawnPos.position, Quaternion.identity);
        }
    }
}