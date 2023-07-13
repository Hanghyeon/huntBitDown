using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public interface InterfaceUnit
    {
        public int MaxHP { get; }
        public int CurHP { get; set; }
    }
}