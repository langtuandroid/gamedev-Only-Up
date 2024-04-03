using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OUFadeImageTween : MonoBehaviour
    {
        [SerializeField] private Image _fadeImg;
    
        void Start() => FadeImageAnimation(true);

        private void FadeImageAnimation(bool isFadein)
        {
            if (isFadein) _fadeImg.DOFade(0, 1).SetEase(Ease.Linear);
            else _fadeImg.DOFade(1, 1).SetEase(Ease.Linear);
        }
    }
}
