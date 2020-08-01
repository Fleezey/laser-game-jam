using System;
using System.Collections;
using System.Collections.Generic;
using Game.Turrets;
using UnityEngine;

namespace Game.Player{
    public class TurretSpawner : MonoBehaviour{

        [SerializeField] private GameObject turretPrefab;
        private float m_currTime;

        // Update is called once per frame
        void Update()
        {
            if (m_currTime <= 0.0f){
                GameObject turret = Instantiate(turretPrefab, transform.position, Quaternion.Euler(0, 180, 0) * transform.rotation);
                GameObject hero = GameObject.Find("Perceus");
                if (hero != null)
                    turret.GetComponent<Turret>().SetTarget(hero.transform);
                Destroy(gameObject);
            }
            m_currTime -= Time.deltaTime;
        }

        public void SetTravelTime(float timeLeft)
        {
            m_currTime = timeLeft;
        }
    }
}