using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unit
{
    public abstract class NonPlayerUnit : MonoBehaviour, InterfaceUnit
    {
        public enum State
        {
            Sleep,      // ��Ȱ�� ����
            Alert,      // ��� ����
            Roaming,    // ����� �Ҿ������ ã�� ���� -> �ΰ���¡�� ���� �ι� �Ʒ��� �������� ����
            Engauge     // ����� �߰��ϰ� �ش� ��ġ�� �̵��Ͽ� �����ϴ� ����
        }

        public int MaxHP { get; protected set; }
        public int CurHP { get; set; }

        public State CurState { get; protected set; }


        protected virtual void SetState(State _state)
        {
            if (State.Roaming <= CurState)
            {
                if (State.Roaming <= _state)
                    CurState = _state;
            }
            else
            {
                if (State.Roaming > _state)
                    CurState = _state;
            }
        }
    }
}
