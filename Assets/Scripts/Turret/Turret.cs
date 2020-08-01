using UnityEngine;


namespace Game.Turrets
{
    public class Turret : TurretStateMachine
    {
        public float ChargeupTime => m_ChargeupTime;
        public float CooldownTime => m_CooldownTime;

        [Header("References")]
        [SerializeField] private Transform m_Target = null;
        [SerializeField] private Transform m_Head = null;
        [SerializeField] private Transform m_CannonEnd = null;
        [SerializeField] private GameObject m_Projectile = null;

        [Header("Ray Properties")]
        [SerializeField] private float m_DetectionRadius = 100f;
        [SerializeField] private LayerMask m_CollisionLayers = -1;

        [Header("Timers")]
        [SerializeField] private float m_ChargeupTime = 0f;
        [SerializeField] private float m_CooldownTime = 0f;


        private void Start()
        {
            SetState(new Idle(this));
        }

        public void Update()
        {
            if (m_Target == null) return;

            m_Head.LookAt(GetTargetPosition());
            m_State.Update();
        }


        public bool HasLineOfSightWithTarget()
        {
            if (m_Target == null) return false;

            Vector3 rayDirection = (m_Target.position - transform.position).normalized;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, rayDirection, out hit, m_DetectionRadius, m_CollisionLayers))
            {
                return hit.transform == m_Target;
            }

            return false;
        }

        public void FireProjectile()
        {
            if (m_Target == null) return;

            GameObject projectile = Instantiate(m_Projectile, m_CannonEnd.position, Quaternion.identity);
            projectile.transform.LookAt(GetTargetPosition());
        }


        private Vector3 GetTargetPosition()
        {
            Vector3 heightCorrectedPosition = m_Target.position;
            heightCorrectedPosition.y = m_Head.position.y;
            return heightCorrectedPosition;
        }
    }
}
