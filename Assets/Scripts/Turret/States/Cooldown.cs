using UnityEngine;


namespace Game.Turrets
{
    public class Cooldown : TurretState
    {
        private float m_CooldownTime = 0f;


        public Cooldown(Turret turret) : base(turret) {}


        public override void Start() {}

        public override void Update()
        {
            if (m_CooldownTime >= m_Turret.CooldownTime)
            {
                m_Turret.SetState(new Idle(m_Turret));
            }

            m_CooldownTime += Time.deltaTime;
        }
    }
}
