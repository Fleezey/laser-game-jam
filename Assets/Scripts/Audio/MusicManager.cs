using UnityEngine;
using UnityEngine.SceneManagement;


namespace Game.Audio
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip m_MainTheme;
        
        private string sceneName;


        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);    
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != sceneName)
            {
                sceneName = scene.name;
                Invoke("PlayMusic", 0.2f);
            }
        }

        private void PlayMusic()
        {
            AudioClip clipToPlay = m_MainTheme;
            if (clipToPlay != null)
            {
                AudioManager.Instance.PlayMusic(clipToPlay, 2f);
                Invoke("PlayMusic", clipToPlay.length);
            }
        }
    }
}
