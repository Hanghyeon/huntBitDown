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
            Engauge     // ����� �߰��ϰ� �ش� ��ġ�� �̵��Ͽ� �����ϴ� ����
        }

        public long SN { get; private set; }

        public int MaxHP { get; protected set; }
        public int CurHP { get; set; }

        public State CurState { get; protected set; }

        protected virtual void SetState(State _state)
        {
            if (CurState != _state)
                CurState = _state;
        }
    }
}
