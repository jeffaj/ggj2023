using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players {
    public class PlayerFuel : MonoBehaviour {
        #region Inspector Fields

        [SerializeField]
        private int _fuelMax = 100;
        [SerializeField]
        private int _moveCost = 5;

        #endregion

        public int Fuel { get; private set; }

        public int FuelMax => _fuelMax;

        public void ResetToFull() {
            this.Fuel = this.FuelMax;
        }

        public void MoveDecrement() {
            this.Fuel = Mathf.Max(0, this.Fuel - _moveCost);
        }
        public void FindFuel(int additionalFuel) {
            this.Fuel = Mathf.Min(this.FuelMax, this.Fuel + additionalFuel);
        }

        public bool IsEmpty => this.Fuel <= 0;
        public float Percentage => (float)this.Fuel / this.FuelMax;


    }
}