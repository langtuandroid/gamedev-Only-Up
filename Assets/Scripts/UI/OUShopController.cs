using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OUShopController : MonoBehaviour
    {
        private const string k_skinKey = "skin_unlocked_";
        private const string k_currentSkin = "skin_equipped";
    
        [SerializeField] private List<int> _skinPrices;
        [SerializeField] private List<RenderTexture> _skinTextures;
        [SerializeField] private OUShopButtonController _shopButtonPrefab;
        [SerializeField] private Transform _buttonsController;
        [SerializeField] private RawImage _mainSkinImage;
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private Button _addMoneyButton;

        private List<OUShopButtonController> _skinButtons = new();
        private int _money;
    
        void Start()
        {
            _money = PlayerPrefs.GetInt("Energy", 0);
            _moneyText.text = _money.ToString();
            SetSkinUnlocked(0);
            for (int i = 0; i < _skinPrices.Count; i++)
            {
                var buttonPrefab = Instantiate(_shopButtonPrefab, _buttonsController);
                buttonPrefab.Initialize(i, _skinPrices[i], 1 == PlayerPrefs.GetInt(k_skinKey + i, 0), i == PlayerPrefs.GetInt(k_currentSkin, 0), _skinTextures[i], OnSkinButtonClick);
                _skinButtons.Add(buttonPrefab);
            }
        
            _addMoneyButton.onClick.AddListener(() => { _money += 10; SaveMoney(); });
            EquipSkin(PlayerPrefs.GetInt(k_currentSkin, 0));
        }

        private void OnSkinButtonClick(int skinIndex)
        {
            var skinPrice = _skinPrices[skinIndex];
            var isSkinUnlocked = PlayerPrefs.GetInt(k_skinKey + skinIndex, 0) == 1;
            if (skinPrice > _money && !isSkinUnlocked) return;
        
            OUAudioManager.Instance.ClickSound();
            if (isSkinUnlocked)
            {
                EquipSkin(skinIndex);
                return;
            }

            _money -= skinPrice;
            SetSkinUnlocked(skinIndex);
            SaveMoney();
            EquipSkin(skinIndex);
        }

        private void SetSkinUnlocked(int index) => PlayerPrefs.SetInt(k_skinKey + index, 1);
    
        private void SaveMoney()
        {
            _moneyText.text = _money.ToString();
            PlayerPrefs.SetInt("Energy", _money);
        }

        private void EquipSkin(int index)
        {
            _mainSkinImage.texture = _skinTextures[index];
            for (int i = 0; i < _skinButtons.Count; i++)
            {
                if (PlayerPrefs.GetInt(k_skinKey + i, 0) == 0) continue;
                _skinButtons[i].SetEquipped(i == index);
            }
            PlayerPrefs.SetInt(k_currentSkin, index);
        }
    }
}
