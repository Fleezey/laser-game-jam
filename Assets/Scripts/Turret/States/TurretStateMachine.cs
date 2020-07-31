using UnityEngine;


namespace Game.Turrets
{
    public abstract class TurretStateMachine : MonoBehaviour
    {
        protected TurretState m_State;


        public void SetState(TurretState state)
        {
            m_State = state;
            m_State.Start();
        }
    }
}
