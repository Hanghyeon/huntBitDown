using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caldwell.Unit
{
    public class UnitManager : MonoBehaviour
    {
        private static UnitManager _instance = null;
        public static UnitManager Instance 
        {
            get 
            {
                if (!_instance)
                {
                    var _manager = new GameObject("_UnitManager");
                    
                    DontDestroyOnLoad(_manager);
                    
                    _instance = _manager.AddComponent<UnitManager>();
                    _instance.Set();
                }

                return _instance;
            } 
        }

        public void Set()
        {
        }

        public void RegistUnit(IUnit _unit)
        {
            long sn = _unit.SN;
            if (!m_unitBySN.ContainsKey(sn))
            {
                m_unitBySN.Add(sn, _unit);
            }
        }


        public void UnregistUnit(IUnit _unit)
        {
            long sn = _unit.SN;
            if (m_unitBySN.ContainsKey(sn))
            {
                m_unitBySN.Remove(sn);
            }
        }

        private Dictionary<long, IUnit> m_unitBySN = new Dictionary<long, IUnit>();

        public ICollection<IUnit> AllUnits
        {
            get
            {
                return m_unitBySN.Values;
            }
        }


        public List<IUnit> m_disableUnits = new List<IUnit>();
        public List<IUnit> DisableUnits
        {
            get
            {
                m_disableUnits.Clear();

                foreach (IUnit unit in m_unitBySN.Values)
                {
                    if (unit.CurHP > 0)
                        continue;

                    m_disableUnits.Add(unit);
                }
                
                return m_disableUnits;
            }
        }
    }
}