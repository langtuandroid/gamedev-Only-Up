using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class OUMenuUIController : MonoBehaviour
    {
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _openMenuButton;
        [SerializeField] private Button _openSettingsButton;
        [SerializeField] private Button _openShopButton;

        private void Start()
        {
            _menuPanel.SetActive(true);
            _settingsPanel.SetActive(false);
            _shopPanel.SetActive(false);
            
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _openMenuButton.onClick.AddListener(OnMenuButtonClick);
            _openSettingsButton.onClick.AddListener(OnSettingButtonClick);
            _openShopButton.onClick.AddListener(OnShopButtonClick);
        }

        private void OnPlayButtonClick()
        {
            OUAudioManager.Instance.PlaySound();
            SceneManager.LoadScene(1);
        }

        private void OnMenuButtonClick()
        {
            OUAudioManager.Instance.ClickSound();
            _menuPanel.SetActive(true);
            _settingsPanel.SetActive(false);
            _shopPanel.SetActive(false);
        }

        private void OnSettingButtonClick()
        {
            OUAudioManager.Instance.ClickSound();
            _menuPanel.SetActive(false);
            _settingsPanel.SetActive(true);
            _shopPanel.SetActive(false);
        }

        private void OnShopButtonClick()
        {
            OUAudioManager.Instance.ClickSound();
            _menuPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            _shopPanel.SetActive(true);
        }
    }
}
