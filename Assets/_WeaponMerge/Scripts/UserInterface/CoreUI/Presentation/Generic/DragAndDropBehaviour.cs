using _WeaponMerge.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Generic
{
    public interface IDragAndDrop
    {
        void OnBeginDrag(Vector3 position, Sprite sprite);
        void Dragging();
        void OnDropDragging();
    }
    
    public class DragAndDropBehaviour: MonoBehaviour, IDragAndDrop
    {
        private Image _image;
        private Vector3 _initialPosition = Vector3.zero;

        private void Awake()
        {
            _image = GetComponent<Image>();
            PanicHelper.CheckAndPanicIfNull(_image);
            transform.localScale = Vector3.zero;
        }

        public void OnBeginDrag(Vector3 position, Sprite sprite)
        {
            _initialPosition = position;
            transform.localScale = Vector3.one;
            _image.sprite = sprite;
        }

        public void Dragging()
        {
            _image.transform.position = Mouse.current.position.ReadValue();
        }

        public void OnDropDragging()
        {
            
        }
    }
}