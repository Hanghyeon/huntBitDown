using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caldwell.Unit
{
    public class AnimalBase : NonePlayerUnit
    {
        // 적대 유닛
        protected List<IUnit> m_hostileUnit;

        protected int alertTimer = 0; //밀리세컨드 단위임
        protected int engaugeTimer = 0; //밀리세컨드 단위임

        [SerializeField]
        protected float updateDelayTime = UPDATE_DELAY;

        [SerializeField]
        protected int rangeAlert = 10;
        [SerializeField]
        protected int rangeEngauge = 7;


        protected float GetAlertRange { get { return rangeAlert * RANGE_SCALING_VALUE; } }
        protected float GetEngaugeRange { get { return rangeEngauge * RANGE_SCALING_VALUE; } }

        protected IUnit m_lastTrackingUnit = null;

        protected List<IUnit> m_detectedUnits = null;
        
        protected Material testSkin = null;
        protected Renderer m_renderer = null;

        protected State nextState { get; set; }


#if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, GetAlertRange);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, GetEngaugeRange);
        }
#endif

        protected void Awake()
        {
            SN = this.GetInstanceID();
            Set(50);
        }

        protected virtual void Set(int _maxHP)
        {
            
            MaxHP = _maxHP;
            CurHP = MaxHP;

            SetTestStateMaterial();
            SetTimers(AlertTimerValueMilliSec, EngaugeTimerValueMilliSec);

            StartCoroutine(UpdateState());
        }

        protected virtual void SetTestStateMaterial()
        {
            if (testSkin)
                return;

            m_renderer = this.gameObject.GetComponent<Renderer>();
            testSkin = new Material(m_renderer.material);
            testSkin.name = string.Format("testSkin{0}-{1}", this.GetType().Name, SN);
            m_renderer.material = testSkin;
        }

        protected virtual void SetTimers(int alertTime, int engaugeTime)
        {
            alertTimer = alertTime;
            engaugeTimer = engaugeTime;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void LateUpdate()
        {
            IntroductionOther();
        }

        protected virtual void IntroductionOther()
        {
            if (!testSkin)
                return;

            if (CurState == State.Sleep)
            {
                testSkin.color = Color.green;
                m_renderer.material = testSkin;
            }

            if (CurState == State.Alert)
            {
                testSkin.color = Color.yellow;
                m_renderer.material = testSkin;
            }

            if (CurState == State.Roaming)
            {
                testSkin.color = Color.red + Color.green * 0.5f;
                m_renderer.material = testSkin;
            }

            if (CurState == State.Engauge)
            {
                testSkin.color = Color.red;
                m_renderer.material = testSkin;
            }

            if (CurHP <= 0)
            {
                testSkin.color = Color.white * (Color.grey * 0.2f);
                m_renderer.material = testSkin;
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

        protected virtual IEnumerator UpdateState()
        {
            while (true)
            {
                yield return null;

                UpdateNearUnits(GetAlertRange, ref m_detectedUnits);

                if (m_detectedUnits == null)
                    yield break;

                m_lastTrackingUnit = m_detectedUnits.Count != 0 ? m_detectedUnits[0] : null;

                int deltaTime = 0;
                while (m_detectedUnits.Count == 0)
                {
                    if (alertTimer <= 0 ||
                        engaugeTimer <= 0)
                    {
                        TakeSleep();
                        break;
                    }

                    yield return null;

                    deltaTime = Mathf.FloorToInt(Time.deltaTime * 1000);

                    if (CurState == State.Alert) alertTimer -= deltaTime;
                    if (CurState == State.Engauge) engaugeTimer -= deltaTime;

                    UpdateNearUnits(GetAlertRange, ref m_detectedUnits);
                }

                alertTimer = AlertTimerValueMilliSec;
                engaugeTimer = EngaugeTimerValueMilliSec;

                if (CurState != State.Engauge)
                    TakeAlert(m_lastTrackingUnit);
                TakeEngauge(m_lastTrackingUnit);

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
            if (CurState < State.Alert)
                return;

            SetState(State.Sleep);
        }

        protected virtual void TakeAlert(Unit.IUnit _target)
        {
            bool isTargetInRange = false;
            if (_target is PlayerUnit)
            {
                var playerUnit = _target as PlayerUnit;
                isTargetInRange = Util.IsInRange(this.transform, playerUnit.transform, GetAlertRange);
            }

            if (isTargetInRange)
            {
                SetState(State.Alert);
            }
        }

        protected virtual void TakeEngauge(Unit.IUnit _target)
        {
            bool isTargetInRange = false;
            if (_target is PlayerUnit)
            {
                var playerUnit = _target as PlayerUnit;
                isTargetInRange = Util.IsInRange(this.transform, playerUnit.transform, GetEngaugeRange);
            }

            if (isTargetInRange)
            {
                SetState(State.Engauge);
            }
        }
    }
}