namespace Game.Turrets
{
    public abstract class TurretState
    {
        protected Turret m_Turret;

        public TurretState(Turret turret)
        {
            m_Turret = turret;
        }

        public virtual void Start() {}

        public virtual void Update() {}
    }
}
