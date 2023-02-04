using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public abstract class Tile : MonoBehaviour {

        public abstract bool IsPassable { get; }

        public virtual void Interact() { }

    }
}