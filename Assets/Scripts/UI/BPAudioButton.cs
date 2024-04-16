using Managers;
using Scripts.Gameplay.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class BPAudioButton : MonoBehaviour
    {
        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;
        [SerializeField] private Image _icon;

        private bool _soundState;
        private Button _thisButton;
        
        private void Awake()
        {
            _thisButton = GetComponent<Button>();
            _soundState = BPPlayerPrefsManager.GetSoundState() == 1;
            _thisButton.onClick.AddListener(SwitchSoundState);
            SetAudioState();
        }

        private void OnDestroy()
        {
            _thisButton.onClick.RemoveAllListeners();
        }

        private void SwitchSoundState()
        {
            _soundState = !_soundState;
            SetAudioState();
            SaveState();
            OUAudioManager.Instance.ClickSound();
        }
        
        private void SetAudioState()
        {
            _icon.color = _soundState ? _onColor : _offColor;
            OUAudioManager.Instance.SoundIsOn = _soundState;
        }
        
        void SaveState() => BPPlayerPrefsManager.SetSoundState(_soundState ? 1 : 0);
    }
}