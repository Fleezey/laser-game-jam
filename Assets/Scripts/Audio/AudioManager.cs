using System.Collections;
using UnityEngine;


namespace Game.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        public enum AudioChannel { Master, Sfx, Music };


        public float MasterVolumePercent { get; private set; }
        public float SfxVolumePercent { get; private set; }
        public float MusicVolumePercent { get; private set; }

        private AudioSource m_Sfx2DSource;
        private AudioSource[] m_MusicSources;
        private int m_ActiveMusicSourceIndex;

        private Transform m_AudioListener;
        private Transform m_PlayerTransform;

        [Header("In-Editor Volume")]
        [SerializeField][Range(0, 1)] private float m_EditorMasterVolume = 1f;
        [SerializeField][Range(0, 1)] private float m_EditorSfxVolume = 1f;
        [SerializeField][Range(0, 1)] private float m_EditorMusicVolume = 1f;


        private void Awake()
        {
            SetupMusicSources();
            SetupSfxSource();
            SetupAudioListener();
            SetupVolume();
        }

        private void Update()
        {
            if (m_PlayerTransform != null)
            {
                m_AudioListener.position = m_PlayerTransform.position;
            }

#if UNITY_EDITOR
            SetupEditorVolume();
#endif
        }

        public void SetVolume(float volumePercent, AudioChannel channel)
        {
            switch (channel)
            {
                case AudioChannel.Master:
                    MasterVolumePercent = volumePercent;
                    break;

                case AudioChannel.Sfx:
                    SfxVolumePercent = volumePercent;
                    break;

                case AudioChannel.Music:
                    MusicVolumePercent = volumePercent;
                    break;
            }

            foreach (var musicSource in m_MusicSources)
            {
                musicSource.volume = MusicVolumePercent * MasterVolumePercent;
            }

            PlayerPrefs.SetFloat("master_volume", MasterVolumePercent);
            PlayerPrefs.SetFloat("sfx_volume", SfxVolumePercent);
            PlayerPrefs.SetFloat("music_volume", MusicVolumePercent);
            PlayerPrefs.Save();
        }

        public void PlayMusic(AudioClip clip, float fadeDuration = 1f)
        {
            m_ActiveMusicSourceIndex = 1 - m_ActiveMusicSourceIndex;
            m_MusicSources[m_ActiveMusicSourceIndex].clip = clip;
            m_MusicSources[m_ActiveMusicSourceIndex].Play();
            m_MusicSources[m_ActiveMusicSourceIndex].loop = true;

            StartCoroutine(AnimateMusicCrossFade(fadeDuration));
        }

        public void PlaySound(AudioClip clip, Vector3 position)
        {
            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, position, SfxVolumePercent * MasterVolumePercent);
            }
        }

        public void PlaySound(AudioClip clip)
        {
            if (clip != null)
            {
                m_Sfx2DSource.PlayOneShot(clip, SfxVolumePercent * MasterVolumePercent);
            }
        }


        private IEnumerator AnimateMusicCrossFade(float duration)
        {
            float percent = 0;

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / duration;
                m_MusicSources[m_ActiveMusicSourceIndex].volume = Mathf.Lerp(0, MusicVolumePercent * MasterVolumePercent, percent);
                m_MusicSources[1 - m_ActiveMusicSourceIndex].volume = Mathf.Lerp(MusicVolumePercent * MasterVolumePercent, 0 , percent);
                yield return null;
            }
        }

        private void SetupMusicSources()
        {
            m_MusicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject source = new GameObject("Music Source " + (i + 1));
                m_MusicSources[i] = source.AddComponent<AudioSource>();
                source.transform.parent = transform;
            }
        }

        private void SetupSfxSource()
        {
            GameObject source = new GameObject("2D SFX Source");
            m_Sfx2DSource = source.AddComponent<AudioSource>();
            source.transform.parent = transform;
        }

        private void SetupAudioListener()
        {
            m_AudioListener = FindObjectOfType<AudioListener>().transform;

            Entities.PlayerEntity player = FindObjectOfType<Entities.PlayerEntity>();
            if (player != null) {
                m_PlayerTransform = player.transform;
            }
        }

        private void SetupVolume() 
        {
            MasterVolumePercent = PlayerPrefs.GetFloat("master_volume", 1f);
            SfxVolumePercent = PlayerPrefs.GetFloat("sfx_volume", 1f);
            MusicVolumePercent = PlayerPrefs.GetFloat("music_volume", 1f);

#if UNITY_EDITOR
            SetupEditorVolume();
#endif
        }

        private void SetupEditorVolume()
        {
            MasterVolumePercent = m_EditorMasterVolume;
            SfxVolumePercent = m_EditorSfxVolume;
            MusicVolumePercent = m_EditorMusicVolume;

            foreach (var source in m_MusicSources)
            {
                source.volume = MusicVolumePercent * MasterVolumePercent;
            }
        }
    }
}
