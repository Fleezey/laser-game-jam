using UnityEngine;


namespace Game.Turrets
{
    public class Turret : TurretStateMachine
    {
        public float ChargeupTime => m_ChargeupTime;
        public float CooldownTime => m_CooldownTime;

        [Header("References")]
        [SerializeField] private Transform m_Target;
        [SerializeField] private Transform m_Head;
        [SerializeField] private Transform m_CannonEnd;
        [SerializeField] private GameObject m_Projectile;

        [Header("Ray Properties")]
        [SerializeField] private float m_DetectionRadius;
        [SerializeField] private LayerMask m_CollisionLayers;

        [Header("Timers")]
        [SerializeField] private float m_ChargeupTime;
        [SerializeField] private float m_CooldownTime;


        private void Start()
        {
            SetState(new Idle(this));
        }

        public void Update()
        {
            m_Head.LookAt(GetTargetPosition());
            m_State.Update();
        }

        public bool HasLineOfSightWithTarget()
        {
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
