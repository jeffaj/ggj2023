using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {

    [System.Serializable]
    public class LevelConfig {

        [SerializeField]
        [Range(0f, 0.5f)]
        private float _stoneDistribution = 0.1f;

        public float StoneDistribution => _stoneDistribution;
    }
}