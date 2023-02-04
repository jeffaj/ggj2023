using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players {
    public static class PlayerInput {
        public static bool LeftHeld => Input.GetKey(KeyCode.LeftArrow);
        public static bool RightHeld => Input.GetKey(KeyCode.RightArrow);

    }
}