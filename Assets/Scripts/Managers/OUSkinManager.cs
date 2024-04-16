using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class OUSkinManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _skins;
        [SerializeField] private List<Avatar> _skinAvatars;
        [SerializeField] private Animator _animator;
    
        private void Awake()
        {
            var skinIndex = PlayerPrefs.GetInt("skin_equipped", 0);
            for (int i = 0; i < _skins.Count; i++) _skins[i].SetActive(i == skinIndex);
            _animator.avatar = _skinAvatars[skinIndex];
        }
    }
}
