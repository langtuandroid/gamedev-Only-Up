using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OUScrollButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private float _scrollSpeed;

    private bool _isPressed;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }

    private void Update()
    {
        if (_isPressed)
        {
            if (_scrollSpeed < 0 && _scrollRect.horizontalNormalizedPosition > 0 || _scrollSpeed > 0 && _scrollRect.horizontalNormalizedPosition < 1)
            {
                _scrollRect.horizontalNormalizedPosition += _scrollSpeed;
            }
        }
    }
}
