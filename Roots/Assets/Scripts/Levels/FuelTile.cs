using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class FuelTile : Tile {

        public int Fuel { get; set; }

        public override bool IsPassable => true;

        public override void Interact() {
            // TODO: play fuel sound?  Visual effect?

            Game.Player.AddFuel(this.Fuel);
        }

    }
}