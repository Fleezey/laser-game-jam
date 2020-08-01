using UnityEngine;


namespace Game.Audio
{
    [CreateAssetMenu(menuName = "Audio/Create a sound")]
    [System.Serializable]
    public class Sound : ScriptableObject
    {
        public AudioClip[] m_Clips;


        public AudioClip GetClip()
        {
            return (m_Clips.Length != 0
                ? m_Clips[Random.Range(0, m_Clips.Length)]
                : null
            );
        }
    }
}
