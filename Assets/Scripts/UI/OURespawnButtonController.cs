using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OURespawnButtonController : MonoBehaviour
    {
        [SerializeField] private Button _thisButton;

        private void OnEnable()
        {
            _thisButton.interactable = OUGameManager.IsCheckPointChecked;
        }
    }
}