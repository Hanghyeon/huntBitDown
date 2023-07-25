using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Util
{
    public static bool IsInRange(Transform _own, Transform _target, float _range)
    {
        bool isTargetInRage = false;
        if (_target)
        {
            float delta = Mathf.Abs((_target.position - _own.position).magnitude);

            isTargetInRage = (delta < _range);
        }
        return isTargetInRage;
    }
}
