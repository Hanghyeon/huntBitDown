using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Caldwell.Unit
{
    [CustomEditor(typeof(UnitManager))]
    public class UnitManagerInspector : Editor
    {
        public List<GameObject> allUnits;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement uppermost = new VisualElement();
            uppermost.name = "uppermost_conatiner";

            var unitManager = target as UnitManager;

            List<IUnit> iUnits = new List<IUnit>(unitManager.AllUnits);
            
            if (iUnits.Count > 0)
            {
                allUnits = iUnits.ConvertAll<GameObject>(i => i.Own);
            }

            Label title = new Label("All Units");
            uppermost.Add(title);

            ListView unitList = new ListView(allUnits, -1, () => new ObjectField(), null);
            uppermost.Add(unitList);
            {
                unitList.bindItem = (element, idx) =>
                {
                    ObjectField of = element as ObjectField;
                    of.allowSceneObjects = true;
                    of.value = allUnits[idx];
                };
            }

            return uppermost;
        }

    }
}