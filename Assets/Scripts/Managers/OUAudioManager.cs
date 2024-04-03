using UnityEngine;

namespace Managers
{
    public class OUAudioManager : MonoBehaviour
    {
        public static OUAudioManager Instance { set; get; }

        public AudioSource PlayButtonSource;
        public AudioSource ButtonClickSource;
        public AudioSource BackgroundMusicSource;
        public AudioSource EnergyCollectSoundSource;


        private void Awake()
        {
            DontDestroyOnLoad(this);
            if(Instance == null) Instance = this;
            else Destroy(gameObject);
        }
    }
}
