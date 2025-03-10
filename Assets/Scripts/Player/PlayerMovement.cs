﻿using Game.Entities;
using UnityEngine;

namespace Game.Player{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        public float rotSpeed;

        public float deploySpeed;
        // Shield cooldown?? Player state?

        public CharacterController controller;

        [SerializeField] private GameObject spawnerPrefab;
        [SerializeField] private float m_GravityScale = 1f;
        [SerializeField] private Shield m_Shield = null;

        [Header("Crosshairs stuff")]
        [SerializeField] private Transform m_Crosshairs = null;
        [SerializeField] private Transform m_CrosshairsHeight = null;

        private Camera m_cam;
        private bool m_turretActive;
        private Animator m_Animator;


        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
        }

        protected void Start()
        {
            m_turretActive = false;
            m_cam = Camera.main;
        }

        void Update()
        {
            float moveSpeed = speed;

            if (Input.GetMouseButtonDown(0))
            {
                if (m_Animator != null) 
                {
                    m_Animator.SetTrigger("TriggerBlock");
                    m_Animator.SetBool("IsBlocking", true);
                }

                moveSpeed = 0f;
            }

            if (Input.GetMouseButton(0))
            {
                moveSpeed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (m_Animator != null) 
                {
                    m_Animator.SetBool("IsBlocking", false);
                }
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = (Vector3.right * x + Vector3.forward * z) * moveSpeed;
            Vector3 gravity = Physics.gravity * m_GravityScale;
            Vector3 targetVelocity = move + gravity;

            controller.Move(targetVelocity * Time.deltaTime);

            if (m_Animator != null)
            {
                m_Animator.SetBool("IsRunning", move.magnitude > 0f);
            }

            Vector3 myPos = transform.position;
            Vector3 mousePos = Input.mousePosition;
            
            Debug.DrawLine(myPos, transform.position + transform.forward, Color.red);
            Ray mouseCast = m_cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(mouseCast, out var hit, 100)){
                Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                m_Crosshairs.position = new Vector3(targetPos.x, m_CrosshairsHeight.position.y, targetPos.z);

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

        public void Anim_OnShieldArmed()
        {
            m_Shield.SetArmed(true);
        }

        public void Anim_OnShieldUnarmed()
        {
            m_Shield.SetArmed(false);
        }
    }
}
