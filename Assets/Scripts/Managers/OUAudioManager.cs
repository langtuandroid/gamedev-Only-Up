using Scripts.Gameplay.Managers;
using UnityEngine;

namespace Managers
{
    public class OUAudioManager : MonoBehaviour
    {
        public static OUAudioManager Instance { private set; get; }

        [SerializeField] private AudioSource PlayButtonSource;
        [SerializeField] private AudioSource ButtonClickSource;
        [SerializeField] private AudioSource BackgroundMusicSource;
        [SerializeField] private AudioSource EnergyCollectSoundSource;
        
        private bool _soundIsOn = true;

        public bool SoundIsOn
        {
            get => _soundIsOn;
            set
            {
                if (_soundIsOn == value) return;
                _soundIsOn = value;
                if (_soundIsOn) PlayBackgroundMusic();
                else StopBackgroundMusic();
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
            SoundIsOn = BPPlayerPrefsManager.GetSoundState() == 1;
            PlayBackgroundMusic();
        }

        private void StopBackgroundMusic() => BackgroundMusicSource.Stop();

        private void PlayBackgroundMusic()
        {
            if (!SoundIsOn) return;
            BackgroundMusicSource.Play();
        }

        public void ClickSound()
        {
            if (!SoundIsOn) return;
            ButtonClickSource.Play();
        }

        public void PlaySound()
        {
            if (!SoundIsOn) return;
            PlayButtonSource.Play();
        }

        public void EnergyCollectSound()
        {
            if (!SoundIsOn) return;
            EnergyCollectSoundSource.Play();
        }
    }
}
