using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players {
    public static class PlayerInput {
        public static bool LeftPressed => Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow);
        public static bool LeftHeld => Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
        public static bool RightPressed => Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow);
        public static bool RightHeld => Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
        public static bool DownPressed => Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKeyDown(KeyCode.UpArrow);


    }
}