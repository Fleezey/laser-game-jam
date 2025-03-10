﻿using UnityEngine;


namespace Game.Turrets
{
    public class SimpleProjectile : MonoBehaviour
    {
        [SerializeField] private float m_Force;

        protected Rigidbody m_Rigidbody;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            Vector3 force = transform.forward * m_Force;
            m_Rigidbody.AddForce(force);
        }
    }
}
