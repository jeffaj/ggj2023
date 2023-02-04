using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class TreasureTile : Tile {

        public ArtifactData ArtifactData { get; set; }

        public override bool IsPassable => true;

        public override void Interact() {
            // launch ArtifactModelController
            Game.LaunchArtifactModal(this.ArtifactData);
        }

    }
}