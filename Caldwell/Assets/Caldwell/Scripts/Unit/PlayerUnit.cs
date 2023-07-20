using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unit
{
    public class PlayerUnit : MonoBehaviour, IUnit
    {
        public long SN { get; private set; }
        public int MaxHP { get; protected set; }
        public int CurHP { get; set; }
    }

}