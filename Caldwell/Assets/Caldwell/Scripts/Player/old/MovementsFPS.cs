using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caldwell.Keys;

namespace Caldwell.Movements
{
    public enum MovementState
    {
        OnGround,   // 발을 바닥에 붙이고 있는 상태
        InAir,      // 공중에 있는 상태
        OnVault,     // 파쿠르 상태
        MAX
    }

    public class MovementsFPS : MonoBehaviour
    {
        private MovementState m_state = MovementState.InAir;
        private Transform m_actorTrans = null;
        private Rigidbody m_actorRigid = null;
        private CharacterController m_characterController = null;
        [SerializeField]
        private int m_movementSpeed = 10000;
        private bool isGrounded = false;

        private float m_isForward = 0f;
        private float m_isRightward = 0f;

        private float m_offset = 1f;

        private void Start()
        {
            Set(this.transform, MovementState.InAir);
        }

        protected void Update()
        {
            InputForMove();
        }

        protected void FixedUpdate()
        {
            CheckPhysics();
            Movements();
        }

        protected void LateUpdate()
        {
        }

        public void Set(Transform _actorTrans, MovementState _setState)
        {
            m_state = _setState;
            m_actorTrans = _actorTrans;
            m_characterController = GetComponent<CharacterController>();

            Rigidbody _rigid = m_actorTrans.GetComponent<Rigidbody>();
            m_actorRigid = _rigid;
        }

        public void SetState(MovementState _setState)
        {
            if (m_state != _setState)
                m_state = _setState;
        }

        protected virtual void InputForMove()
        {
            m_isForward   = Input.GetKey(InputMapper.Forward)   ? 1f : 0f;
            m_isForward   = Input.GetKey(InputMapper.Backward)  ? -1f : 0f;
            m_isRightward  = Input.GetKey(InputMapper.Rightward) ? 1f : 0f;
            m_isRightward  = Input.GetKey(InputMapper.Leftward)  ? -1f : 0f;
        }

        protected virtual void CheckPhysics()
        {
            isGrounded = IsGrounded();
        }

        public bool IsGrounded()
        {
            if (!m_characterController)
                return false;

            return m_characterController.isGrounded;
        }

        protected virtual void Movements()
        {
            if (!m_actorTrans) return;
            if (!m_actorRigid) return;

            Vector3 resultVector = m_actorTrans.forward * m_isForward * Time.deltaTime * (m_movementSpeed * 0.001f) * m_offset;
            resultVector += m_actorTrans.right * m_isRightward * Time.deltaTime * (m_movementSpeed * 0.001f) * m_offset;

            m_actorTrans.Translate(resultVector);

            //Vector3 resultVector = m_actorTrans.forward * m_isForward * (0.001f * m_movementSpeed) * Time.deltaTime * m_offset;
            //resultVector += m_actorTrans.right * m_isRightward * (0.001f * m_movementSpeed) * Time.deltaTime * m_offset;

            //m_actorRigid.velocity = (resultVector);
        }
    }
}
