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

        [Header("Sound Effects")]
        [SerializeField] private Audio.Sound m_ShotSounds = null;


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

            float distance = Vector3.Distance(transform.position, m_Target.position);
            if (distance > m_DetectionRadius) return false;

            Vector3 rayDirection = (m_Target.position - transform.position).normalized;
            RaycastHit hit;

            return !Physics.Raycast(transform.position, rayDirection, out hit, m_DetectionRadius, m_CollisionLayers);
        }

        public void FireProjectile()
        {
            if (m_Target == null) return;

            GameObject projectile = Instantiate(m_Projectile, m_CannonEnd.position, Quaternion.identity);
            projectile.transform.LookAt(GetTargetPosition());

            Audio.AudioManager.Instance.PlaySound(m_ShotSounds.GetClip(), projectile.transform.position);
        }

        public void SetTarget(Transform t)
        {
            m_Target = t;
        }


        private Vector3 GetTargetPosition()
        {
            Vector3 heightCorrectedPosition = m_Target.position;
            heightCorrectedPosition.y = m_Head.position.y;
            return heightCorrectedPosition;
        }
    }
}
