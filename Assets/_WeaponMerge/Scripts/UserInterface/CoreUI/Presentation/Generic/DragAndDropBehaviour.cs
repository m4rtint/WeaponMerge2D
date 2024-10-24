using _WeaponMerge.Tools;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Generic
{
    public interface IDragAndDrop
    {
        void OnBeginDrag(Sprite sprite);
        void Dragging();
        void OnEndDrag(SlotView fromSlotView, bool isOverSlot);
    }
    
    public class DragAndDropBehaviour: MonoBehaviour, IDragAndDrop
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            PanicHelper.CheckAndPanicIfNull(_image);
            transform.localScale = Vector3.zero;
        }

        public void OnBeginDrag(Sprite sprite)
        {
            transform.localScale = Vector3.one;
            _image.sprite = sprite;
        }

        public void Dragging()
        {
            _image.transform.position = Mouse.current.position.ReadValue();
        }

        public void OnEndDrag(SlotView fromSlotView, bool isOverSlot)
        {
            if (isOverSlot)
            {
                transform.localScale = Vector3.zero;
            }
            else
            {
                AnimateBackToOriginalPosition(fromSlotView);
            }
        }
        
        private void AnimateBackToOriginalPosition(SlotView fromSlowView)
        {
            transform.DOMove(fromSlowView.transform.position, 0.25f)
                .SetUpdate(true)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    fromSlowView.ShowIcon();
                    transform.localScale = Vector3.zero;
                });
        }
    }
}