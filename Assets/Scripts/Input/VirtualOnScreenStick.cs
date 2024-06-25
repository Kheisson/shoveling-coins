using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace Input
{
    public class VirtualOnScreenStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        #region Fields
        
        [SerializeField] private RectTransform _stickTransform;
        private CanvasGroup _canvasGroup;
        private OnScreenStick _onScreenStick;
        
        #endregion
        
        
        #region Unity Methods
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _onScreenStick = GetComponentInChildren<OnScreenStick>();
            SetCanvasAlphaToTransparent(false);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _stickTransform.position = eventData.position;
            SetCanvasAlphaToTransparent(true);
            _onScreenStick.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetCanvasAlphaToTransparent(false);
            _onScreenStick.OnPointerUp(eventData);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            _onScreenStick.OnDrag(eventData);
        }

        #endregion
        
        
        #region Private Methods
        
        private void SetCanvasAlphaToTransparent(bool state)
        {
            _canvasGroup.alpha = state ? 1 : 0;
        }
        
        #endregion
    }
}
