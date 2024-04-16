using System;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {
        [SerializeField] private Sprite _pressedImage;
        [SerializeField] private Sprite _unPressedImage;
        public string Name;
        
        private Image _thisImage;

        private void Awake()
        {
            _thisImage = GetComponent<Image>();
        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
            if (_pressedImage != null && _thisImage != null && _unPressedImage != null) _thisImage.sprite = _pressedImage;
        }
        
        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
            if (_unPressedImage != null && _thisImage != null && _unPressedImage != null) _thisImage.sprite = _unPressedImage;
        }
        
        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }
        
        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }
        
        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }
    }
}
