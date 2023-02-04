using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States {

    [System.Serializable]
    public class Idle : PlayerState {

        private const float _maxSpeed = 10;

        public void Start() {
            this.ChangeStateToSelfForce();
        }

        protected override void FixedUpdate() {

            bool leftHeld = PlayerInput.LeftHeld;
            bool rightHeld = PlayerInput.RightHeld;

            Vector3 v = this.Player.Velocity;

            if (leftHeld) {
                v.x = -_maxSpeed;
            } else if (rightHeld) {
                v.x = _maxSpeed;
            } else {
                v.x = 0;
            }

            this.Player.Velocity = v;

        }

    }
}