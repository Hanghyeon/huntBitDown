using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Caldwell.Unit
{
    public abstract class NonePlayerUnit : MonoBehaviour, IUnit
    {
        public enum State
        {
            Sleep,      // 비활성 상태
            Alert,      // 경고 상태
            Roaming,    // 대상을 잃어버리고 찾는 상태 -> 인게이징에 들어가면 로밍 아래로 내려가지 않음
            Engauge,     // 대상을 발견하고 해당 위치로 이동하여 공격하는 상태
            MAX
        }

        protected const float UPDATE_DELAY = 0.3f;
        protected const float RANGE_SCALING_VALUE = 1f;

        [SerializeField]
        protected int AlertTimerValueMilliSec = 10000;
        [SerializeField]
        protected int EngaugeTimerValueMilliSec = 5000;

        public long SN { get; private set; }

        public int MaxHP { get; protected set; }
        public int CurHP { get; set; }

        public State CurState { get; protected set; }
        public GameObject Own { get { return this.gameObject; } }

        protected virtual void Awake()
        {
            SN = this.GetInstanceID();
            Init();
        }

        public virtual void Init()
        {
            UnitManager.Instance.RegistUnit(this);
        }

        protected virtual void SetState(State _state)
        {
            if (CurState != _state)
                CurState = _state;
        }

}
}
