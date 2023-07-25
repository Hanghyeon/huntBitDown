using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class Horse : NonePlayerUnit
    {
        

        private List<IUnit> m_hostileUnit;

        private int curTimerCooldown = 0; //밀리세컨드 단위임

        [SerializeField]
        private float updateDelayTime = UPDATE_DELAY;

        [SerializeField]
        private int rangeAlert = 10;
        [SerializeField]
        private int rangeEngauge = 7;


        private float GetAlertRange { get { return rangeAlert * RANGE_SCALING_VALUE; } }
        private float GetEngaugeRange { get { return rangeEngauge * RANGE_SCALING_VALUE; } }

        private IUnit m_lastTrackingUnit = null;

        private List<IUnit> m_detectedUnits = null;

        [SerializeField]
        public Material testSkin = null;

        private State nextState { get; set; }

        public void Set()
        {
            SN = this.GetInstanceID();
            MaxHP = 50;
            CurHP = MaxHP;
            
            curTimerCooldown = milliSecTimerValue;

            StartCoroutine("GetState");
        }

        private void Awake()
        {
            Set();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateNearUnits(GetAlertRange, ref m_detectedUnits);
        }

        private void LateUpdate()
        {
            IntroductionOther();
        }

        private void IntroductionOther()
        {
            if (!testSkin)
                return;

            if (CurState == State.Sleep)
            {
                testSkin.color = Color.green;
            }

            if (CurState == State.Alert)
            {
                testSkin.color = Color.yellow;
            }

            if (CurState == State.Roaming)
            {
                testSkin.color = Color.red + Color.green * 0.5f;
            }

            if (CurState == State.Engauge)
            {
                testSkin.color = Color.red;
            }

            if (CurHP <= 0)
            {
                testSkin.color = Color.white * (Color.grey * 0.2f);
            }
        }

        public List<IUnit> GetNearUnits(float _range)
        {
            List<IUnit> nearUnits = new List<IUnit>();

            var allUnits = UnitManager.Instance.AllUnits;

            for (int unitIdx = 0; unitIdx < allUnits.Count; unitIdx++)
            {
                IUnit _unit = allUnits[unitIdx];

                Component compo = _unit as Component;

                if (!compo)
                    continue;

                if (!Util.IsInRange(this.transform, compo.transform, _range))
                    continue;

                nearUnits.Add(_unit);
            }

            return nearUnits;
        }

        public void UpdateNearUnits(float _range, ref List<IUnit> _collection)
        {
            if (_collection == null)
                _collection = new List<IUnit>();
            else
                _collection.Clear();

            var allUnits = UnitManager.Instance.AllUnits;

            for (int unitIdx = 0; unitIdx < allUnits.Count; unitIdx++)
            {
                IUnit _unit = allUnits[unitIdx];

                Component compo = _unit as Component;

                if (!compo)
                    continue;

                if (!Util.IsInRange(this.transform, compo.transform, _range))
                    continue;

                _collection.Add(_unit);
            }
        }

        IEnumerator GetState()
        {
            while (true)
            {
                yield return null;

                if (m_detectedUnits != null)
                {
                    m_lastTrackingUnit = m_detectedUnits.Count != 0 ? m_detectedUnits[0] : null;

                    if (m_detectedUnits.Count == 0)
                    {
                        while (m_detectedUnits.Count == 0 && curTimerCooldown > 0)
                        {
                            yield return null;
                            curTimerCooldown -= Mathf.FloorToInt(Time.deltaTime * 1000);
                        }

                        if (curTimerCooldown <= 0)
                        {
                            TakeSleep();
                            continue;
                        }
                    }

                    curTimerCooldown = milliSecTimerValue;
                    TakeAlert(m_lastTrackingUnit);
                    TakeEngauge(m_lastTrackingUnit);
                }
            }
        }

        private void IsValidState(State _rawState)
        {

        }

        private void DecreaseState()
        {
            int stateIdx = (int)CurState;
            stateIdx -= 1;

            if (stateIdx < 0)
                stateIdx = 0;

            nextState = (State)stateIdx;
            IsValidState(nextState);
        }

        private void IncreaseState()
        {
            int stateIdx = (int)CurState;
            stateIdx += 1;

            if (stateIdx >= (int)State.MAX)
                stateIdx = (int)(State.MAX - 1);

            nextState = (State)stateIdx;
            IsValidState(nextState);
        }

        public void TakeSleep()
        {
            if (CurState == State.Alert)
                SetState(State.Sleep);
        }

        public void TakeAlert(Unit.IUnit _target)
        {
            bool isTargetInRange = false;
            if (_target is PlayerUnit)
            {
                var playerUnit = _target as PlayerUnit;

                float delta = Mathf.Abs((playerUnit.transform.position - this.transform.position).magnitude);
                float rangeCheckValue = GetAlertRange;

                isTargetInRange = (delta < rangeCheckValue);
            }

            if (isTargetInRange)
            {
                SetState(State.Alert);
            }
        }

        public void TakeEngauge(Unit.IUnit _target)
        {
            bool isTargetInRange = false;
            if (_target is PlayerUnit)
            {
                var playerUnit = _target as PlayerUnit;

                float delta = Mathf.Abs((playerUnit.transform.position - this.transform.position).magnitude);
                float rangeCheckValue = GetEngaugeRange;

                isTargetInRange = (delta < rangeCheckValue);
            }

            if (isTargetInRange)
            {
                SetState(State.Engauge);
            }
        }
    }
}