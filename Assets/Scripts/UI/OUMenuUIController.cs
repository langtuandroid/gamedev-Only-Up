using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class OUMenuUIController : MonoBehaviour
    {
        [SerializeField] private OUMenuUITweeningController _tweeningController;
    
        [SerializeField] private GameObject _playerModelReference;
        [SerializeField] private Image _lowQualityButton;
        [SerializeField] private Image _highQualityButton;
        [SerializeField] private Text _energyText;
        [SerializeField] private Color _activeButtonColor;
        [SerializeField] private Color _disabledButtonColor;
    
        private int _currentGraphicsQuality;
        private int _energyAmount;

        private void Awake()
        {
            _currentGraphicsQuality = PlayerPrefs.GetInt("DefaultGraphics", 1);

            if(_currentGraphicsQuality == 1)
            {
                _highQualityButton.color = _activeButtonColor;
                _lowQualityButton.color = _disabledButtonColor;
                QualitySettings.SetQualityLevel(1);
            }
            else
            {
                _lowQualityButton.color = _activeButtonColor;
                _highQualityButton.color = _disabledButtonColor;
                QualitySettings.SetQualityLevel(0);
            }
        }
        private void Start()
        {
            _energyAmount = PlayerPrefs.GetInt("Energy", 0);
            _energyText.text = _energyAmount.ToString();
        }
    
        public void OnPlayButtonClick()
        {
            StartCoroutine(OnPlayGame());
        }

        private IEnumerator OnPlayGame()
        {
            OUAudioManager.Instance.PlayButtonSource.Play();
            _tweeningController.FadeImageAnimation(false);
            _playerModelReference.SetActive(false);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(1);
        }

        public void OnSettingButtonClick()
        {
            OUAudioManager.Instance.ButtonClickSource.Play();
            _tweeningController.OpenSettingPanel();
        }

        public void OnLowGraphicsButtonClick()
        {
            OUAudioManager.Instance.ButtonClickSource.Play();
            _lowQualityButton.color = _activeButtonColor;
            _highQualityButton.color = _disabledButtonColor;
            QualitySettings.SetQualityLevel(0);
            PlayerPrefs.SetInt("DefaultGraphics", 0);
            _playerModelReference.SetActive(true);
        }

        public void OnHighGraphicsButtonClick()
        {
            OUAudioManager.Instance.ButtonClickSource.Play();
            _highQualityButton.color = _activeButtonColor;
            _lowQualityButton.color = _disabledButtonColor;
            QualitySettings.SetQualityLevel(1);
            PlayerPrefs.SetInt("DefaultGraphics", 1);
            _playerModelReference.SetActive(true);
        }

        public void OnCloseSettingButtonClick()
        {
            OUAudioManager.Instance.ButtonClickSource.Play();
            _tweeningController.CloseSettingsPanel();
        }
    }
}
