using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caldwell.Unit
{
    public interface IUnit
    {
        public long SN { get; }
        public int MaxHP { get; }
        public int CurHP { get; set; }
        public UnityEngine.GameObject Own { get; }
        public void Init();
    }
}