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
        public virtual void Init()
        {

            SN = this.GetInstanceID();
            MaxHP = 150;
            CurHP = MaxHP;
            UnitManager.Instance.AllUnits.Add(this);
        }

        private void Awake()
        {
            Init();
        }
    }

}