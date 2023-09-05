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

        public List<IUnit> m_allUnits = new List<IUnit>();
        public List<IUnit> AllUnits
        {
            get
            {
                return m_allUnits;
            }
        }


        public List<IUnit> m_disableUnits = new List<IUnit>();
        public List<IUnit> DisableUnits
        {
            get
            {
                return m_disableUnits;
            }
        }
    }
}