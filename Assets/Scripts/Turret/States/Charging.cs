using UnityEngine;


namespace Game.Turrets
{
    public class Charging : TurretState
    {
        private float m_ChargeTime = 0f;


        public Charging(Turret turret) : base(turret) {}


        public override void Start() {}

        public override void Update()
        {
            if (m_ChargeTime >= m_Turret.ChargeupTime)
            {
                m_Turret.SetState(new Firing(m_Turret));
            }

            m_ChargeTime += Time.deltaTime;
        }
    }
}
