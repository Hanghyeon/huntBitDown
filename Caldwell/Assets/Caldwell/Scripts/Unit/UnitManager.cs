using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
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
                    _instance.AllUnits = new List<IUnit>();
                }

                return _instance;
            } 
        }

        public List<IUnit> AllUnits { get; set; }


    }
}