using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class OUGameUIController : MonoBehaviour
    {
        public static OUGameUIController Instance { private set; get;}

        [SerializeField] private GameObject _respawnPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private Button _respawnButton;
    
        private int _energyAmount;

        public int EnergyAmount
        {
            get => _energyAmount;
            set
            {
                _energyAmount = value;
                _energyText.text = _energyAmount.ToString();
                PlayerPrefs.SetInt("Energy", _energyAmount);
            }
        }

        private void Awake()
        {
            Instance = this;
            EnergyAmount = PlayerPrefs.GetInt("Energy", 0);
            _respawnPanel.SetActive(false);
            _pausePanel.SetActive(false);
        }

        public void OnGameOver() => StartCoroutine(GameOverCoroutine());

        private IEnumerator GameOverCoroutine()
        {
            _respawnButton.interactable = OUGameManager.IsCheckPointChecked;
            yield return new WaitForSeconds(2);
            _respawnPanel.SetActive(true);
        }

        public void OnCloseButtonClick()
        {
            OUAudioManager.Instance.ClickSound();
            Time.timeScale = 1;
            OUGameManager.IsPlayerDead = false;
            _respawnPanel.SetActive(false);
            _pausePanel.SetActive(false);
        }

        public void OnPauseButtonClick()
        {
            OUAudioManager.Instance.ClickSound();
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void OnResumeButtonClick()
        {
            OUAudioManager.Instance.ClickSound();
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
        }

        public void OnRestartButtonClick()
        {
            OUGameManager.IsPlayerDead = false;
            OUAudioManager.Instance.ClickSound();
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnHomeButtonClick()
        {
            OUAudioManager.Instance.ClickSound();
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void DisableRespawnPanel() => _respawnPanel.SetActive(false);
    }
}
