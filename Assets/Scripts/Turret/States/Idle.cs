namespace Game.Turrets
{
    public class Idle : TurretState
    {
        public Idle(Turret turret) : base(turret) {}


        public override void Start()
        {

        }

        public override void Update()
        {
            if (m_Turret.HasLineOfSightWithTarget())
            {
                m_Turret.SetState(new Charging(m_Turret));
            }
        }
    }
}
