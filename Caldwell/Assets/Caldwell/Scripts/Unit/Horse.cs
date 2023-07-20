using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit;

public class Hourse : NonePlayerUnit
{
    private List<IUnit> m_hostileUnit;

    [SerializeField]
    private int rangeAttention = 10;

    [SerializeField]
    private const float RANGE_SCALING_VALUE = 1f;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        
    }

    public void TakeAttention(Unit.IUnit _target)
    {
        bool isTargetInRage = false;
        if (_target is PlayerUnit)
        {
            var playerUnit = _target as PlayerUnit;
            
            float delta = Mathf.Abs((playerUnit.transform.position - this.transform.position).magnitude);
            float rangeCheckValue = rangeAttention * RANGE_SCALING_VALUE;
            
            isTargetInRage = (delta < rangeCheckValue);
        }

        if(isTargetInRage)
        {
            SetState(State.Alert);
        }
    }
}
