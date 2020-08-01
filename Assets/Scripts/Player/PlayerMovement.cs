using Game.Entities;
using UnityEngine;

namespace Game.Player{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        public float rotSpeed;

        public float deploySpeed;
        // Shield cooldown?? Player state?

        public CharacterController controller;


        // Movement debuff for player when bringing up shield
        // Should it be a flat or multiplicative debuff?
        [SerializeField] private float shieldMvtDebuff;
        [SerializeField] private GameObject spawnerPrefab;


        private Camera m_cam;
        private bool m_shieldActive;
        private bool m_turretActive;
        private Transform m_shield;

        protected void Start()
        {
            m_shield = gameObject.transform.GetChild(0);
            m_shieldActive = false;
            m_turretActive = false;
            m_cam = Camera.main;
        }

        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = Vector3.right * x + Vector3.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0) && !m_shieldActive){
                m_shield.Rotate(0, 0, 30);
                m_shieldActive = true;
                speed *= shieldMvtDebuff;
            }

            if (Input.GetMouseButtonUp(0) && m_shieldActive){
                m_shield.Rotate(0, 0, -30);
                m_shieldActive = false;
                speed /= shieldMvtDebuff;
            }

            Vector3 myPos = transform.position;
            Vector3 mousePos = Input.mousePosition;
            
            Debug.DrawLine(myPos, transform.position + transform.forward, Color.red);
            Ray mouseCast = m_cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(mouseCast, out var hit, 100)){
                Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                if (!m_turretActive){
                    if (Input.GetKey(KeyCode.Space)){
                        Debug.DrawLine(targetPos, targetPos + Vector3.up, Color.green);
                    }
                    if (Input.GetKeyUp(KeyCode.Space)){
                        GameObject spawner = Instantiate(spawnerPrefab, myPos, transform.rotation);
                        float dist = Vector3.Distance(myPos, targetPos);
                        spawner.GetComponent<Rigidbody>().velocity = transform.forward * deploySpeed;
                        spawner.GetComponent<TurretSpawner>().SetTravelTime(dist / deploySpeed);
                        m_turretActive = true;
                    }
                }

                Debug.DrawLine(myPos, targetPos, Color.blue);

                // We need some distance margin with our movement
                // or else the character could twitch back and forth with slight movement
                if (Vector3.Distance(targetPos, myPos) >= 2.5f){
                    var q = Quaternion.LookRotation(targetPos - myPos);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotSpeed * Time.deltaTime);
                }
            }
        }
    }
}
