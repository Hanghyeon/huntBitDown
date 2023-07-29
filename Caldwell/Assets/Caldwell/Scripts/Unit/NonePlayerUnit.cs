using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unit
{
    public abstract class NonePlayerUnit : MonoBehaviour, IUnit
    {
        public enum State
        {
            Sleep,      // ��Ȱ�� ����
            Alert,      // ��� ����
            Roaming,    // ����� �Ҿ������ ã�� ���� -> �ΰ���¡�� ���� �ι� �Ʒ��� �������� ����
            Engauge,     // ����� �߰��ϰ� �ش� ��ġ�� �̵��Ͽ� �����ϴ� ����
            MAX
        }

        protected const float UPDATE_DELAY = 0.3f;
        protected const float RANGE_SCALING_VALUE = 1f;

        [SerializeField]
        protected int AlertTimerValueMilliSec = 10000;
        [SerializeField]
        protected int EngaugeTimerValueMilliSec = 5000;

        public long SN { get; protected set; }

        public int MaxHP { get; protected set; }
        public int CurHP { get; set; }

        public State CurState { get; protected set; }

        public virtual void Init()
        {
            UnitManager.Instance.AllUnits.Add(this);
        }

        protected virtual void SetState(State _state)
        {
            if (CurState != _state)
                CurState = _state;
        }
    }
}
