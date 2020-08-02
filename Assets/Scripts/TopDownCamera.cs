using UnityEngine;


namespace Game
{
    public class TopDownCamera : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Target that the camera should follow")]
        private Transform m_Target;

        [Header("Position Properties")]
        [SerializeField]
        [Tooltip("How high should the camera be from the target")]
        private float m_Height = 12f;

        [SerializeField]
        [Tooltip("How far should the camera be from the target")]
        private float m_Distance = 6f;

        [Header("Offset Properties")]
        [SerializeField]
        [Tooltip("How long should the offset take")]
        [Range(0, 1)]
        private float m_OffsetSmoothTime = 1f;

        private Camera m_Camera;
        private Vector3 m_OffsetVelocity;


        private void Awake()
        {
            m_Camera = GetComponent<Camera>();
            SetTarget(m_Target.gameObject);
        }

        private void Update()
        {
            UpdateCameraPosition();
        }


        public void SetTarget(GameObject target)
        {
            m_Target = target.transform;
        }


        private Vector3 GetCameraPosition()
        {
            if (m_Target)
            {
                return m_Target.position + (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
            }

            return transform.position;
        }

        private void UpdateCameraPosition()
        {
            Vector3 newPosition = GetCameraPosition();

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref m_OffsetVelocity, m_OffsetSmoothTime);
        }
    }
}
