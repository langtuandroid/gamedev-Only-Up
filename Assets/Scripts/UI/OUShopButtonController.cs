using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OUShopButtonController : MonoBehaviour
    {
        private const string k_PricePattern = "Buy $"; 
    
        [SerializeField] private GameObject _pricePanel;
        [SerializeField] private GameObject _equippedPanel;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private RawImage _thisTexture;
        [SerializeField] private Button _thisButton;

        public void Initialize(int skinIndex, int skinPrice, bool isUnlocked, bool isEquipped, RenderTexture skinTexture, Action<int> onSkinButtonClick)
        {
            _thisTexture.texture = skinTexture;
            _priceText.text = k_PricePattern + skinPrice;
            _pricePanel.SetActive(!isUnlocked);
            _equippedPanel.SetActive(isEquipped);
            _thisButton.onClick.AddListener(() => onSkinButtonClick.Invoke(skinIndex));
        }

        private void OnDestroy() => _thisButton.onClick.RemoveAllListeners();

        public void SetEquipped(bool isEquipped)
        {
            _pricePanel.SetActive(false);
            _equippedPanel.SetActive(isEquipped);
        }
    }
}
