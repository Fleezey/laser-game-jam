namespace Game.Turrets
{
    public class Firing : TurretState
    {
        public Firing(Turret turret) : base(turret) {}


        public override void Start()
        {
            m_Turret.FireProjectile();
            m_Turret.SetState(new Cooldown(m_Turret));
        }
    }
}
