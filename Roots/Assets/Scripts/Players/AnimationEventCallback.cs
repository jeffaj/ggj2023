using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// shim to get animation events from the animation clips,
// and then send them to the player object.
public class AnimationEventCallback : MonoBehaviour
{
    public void BreakDownCompleting()
    {
        Game.Player.BreakDownCompleting();
    }
}
