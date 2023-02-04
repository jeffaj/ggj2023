using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class FuelTile : Tile {

        #region Inspector Fields

        [SerializeField]
        private Transform _particleSpawnPos = null;

        [Header("Prefabs")]

        [SerializeField]
        private GameObject _particleEffectPrefab = null;

        #endregion

        public int Fuel { get; set; }

        public override bool IsPassable => true;

        public override void Interact() {

		AudioManager.Instance.PlayDirtBreak();
		AudioManager.Instance.PlayFuelBreak();
            Game.Player.AddFuel(this.Fuel);

            Instantiate(_particleEffectPrefab, _particleSpawnPos.position, Quaternion.identity);
        }

    }
}