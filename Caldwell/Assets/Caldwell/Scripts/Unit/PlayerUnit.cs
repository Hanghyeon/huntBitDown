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

        public GameObject Own { get { return this.gameObject; } }

        public Renderer m_testSkinRenderer;

        private Material m_skin = null;


        public virtual void Init()
        {
            SN = this.GetInstanceID();
            MaxHP = 150;
            CurHP = MaxHP;
            UnitManager.Instance.RegistUnit(this);
        }

        private void Awake()
        {
            Init();

            if (!m_skin)
            {
                m_skin = new Material(m_testSkinRenderer.material);
                m_skin.color = Color.green;
                m_testSkinRenderer.material = m_skin;
            }
        }
    }
}