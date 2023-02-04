using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class FuelTile : Tile {

        #region Inspector Fields

        [Header("Prefabs")]

        [SerializeField]
        private GameObject _particleEffectPrefab = null;

        [Header("Children")]

        [SerializeField]
        private Transform _particleSpawnPos = null;

        #endregion

        public int Fuel { get; set; }

        public override bool IsPassable => true;

        public override void Interact() {

            Game.Player.AddFuel(this.Fuel);

            Instantiate(_particleEffectPrefab, _particleSpawnPos.position, Quaternion.identity);
        }

    }
}