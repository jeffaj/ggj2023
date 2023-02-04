using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class DirtTile : Tile {

        public override bool IsPassable => true;

        public override void Interact() {
            AudioManager.Instance.PlayDirtBreak();
        }

    }
}