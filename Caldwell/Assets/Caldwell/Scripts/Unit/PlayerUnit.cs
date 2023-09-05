using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Caldwell.Unit
{
    public class PlayerUnit : MonoBehaviour, IUnit
    {
        public long SN { get; private set; }
        public int MaxHP { get; protected set; }
        public int CurHP { get; set; }


        private Material m_skin = null;


        public virtual void Init()
        {

            SN = this.GetInstanceID();
            MaxHP = 150;
            CurHP = MaxHP;
            UnitManager.Instance.AllUnits.Add(this);
        }

        private void Awake()
        {
            Init();

            if (!m_skin)
            {
                var renderer = this.gameObject.GetComponent<Renderer>();
                m_skin = new Material(renderer.material);
                m_skin.color = Color.green;
                renderer.material = m_skin;
            }
        }
    }
}