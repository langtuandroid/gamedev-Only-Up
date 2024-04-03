using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OUMenuUITweeningController : MonoBehaviour
    {
        [SerializeField] private RectTransform _settingsTransform;
        [SerializeField] private RectTransform _playButtonTransform;
        [SerializeField] private RectTransform _shopButtonTransform;
        [SerializeField] private RectTransform _settingsButtonTransform;
        [SerializeField] private RectTransform _energyPanelTransform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Image _fadeImg;
        [SerializeField] private Vector3 _playerOnSettingPosition;
        [SerializeField] private Vector3 _playerStartPosition;

        private void Start()
        {
            _playButtonTransform.DOAnchorPosX(-280f, 0.6f).SetEase(Ease.Linear);
            _shopButtonTransform.DOAnchorPosX(-300f, 0.8f).SetEase(Ease.Linear);
            _settingsButtonTransform.DOAnchorPosX(-300f, 1f).SetEase(Ease.Linear);
            _energyPanelTransform.DOAnchorPosX(-300f, 1f).SetEase(Ease.Linear);
            FadeImageAnimation(true);
        }

        public void FadeImageAnimation(bool isFadein)
        {
            if (isFadein) _fadeImg.DOFade(0, 1).SetEase(Ease.Linear);
            else _fadeImg.DOFade(1, 1).SetEase(Ease.Linear);
        }
    
        public void OpenSettingPanel()
        {
            _settingsTransform.DOAnchorPosX(0, 0.3f).SetEase(Ease.Linear);
            _playerTransform.DOMove(_playerOnSettingPosition,0.3f).SetEase(Ease.Linear);
        }
    
        public void CloseSettingsPanel()
        {
            _settingsTransform.DOAnchorPosX(-890f, 0.3f).SetEase(Ease.Linear);
            _playerTransform.DOMove(_playerStartPosition, 0.3f).SetEase(Ease.Linear);
        }
    }
}
