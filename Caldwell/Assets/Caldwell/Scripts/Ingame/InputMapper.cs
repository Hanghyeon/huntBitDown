using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caldwell.Keys
{
    public class InputMapper
    {
        //비트연산 관련
        /*   A = (B | C)
            (A & B) == B -> TRUE
            (A & C) == C -> TRUE
            (A & D) == D -> FALSE
        */

        public static KeyCode Anyward = Forward | Backward | Leftward | Rightward;

        // move directions
        public static KeyCode Forward = KeyCode.W;
        public static KeyCode Backward = KeyCode.S;
        public static KeyCode Leftward = KeyCode.A;
        public static KeyCode Rightward = KeyCode.D;

        // change force like jump, roll
        public static KeyCode Splint = KeyCode.LeftShift;
        public static KeyCode Jump = KeyCode.Space;

        // state change like crouch, prone, stop bleeding, stop breathing, steady aim
        public static KeyCode Crouch = KeyCode.C | KeyCode.LeftControl;
        public static KeyCode StopBreath = KeyCode.LeftShift | KeyCode.Mouse1;
        public static KeyCode StopBleeding = KeyCode.F;
        public static KeyCode ShoulderAim = KeyCode.Mouse1;
        public static KeyCode SteadyAim = KeyCode.Mouse1 | KeyCode.LeftShift;

        // attack
        public static KeyCode meleeAttack = KeyCode.Mouse0;
        public static KeyCode gunFire = KeyCode.Mouse0;

        // interactive world
        public static KeyCode EnterDarkSite = KeyCode.E;
        public static KeyCode InteractiveObject = KeyCode.F;   // Open door, climb ladder, turn off lantern
    }
}