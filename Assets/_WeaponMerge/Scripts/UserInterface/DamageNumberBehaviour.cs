using UnityEngine;
using TMPro;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class DamageNumberBehaviour : MonoBehaviour
    {
        [SerializeField] private float _floatSpeed = 1.0f;
        [SerializeField] private float _fadeDuration = 1.0f;
        [SerializeField] private float _offsetHeight = 1.0f; // New offset height field
        private TMP_Text _damageText;
        private float _elapsedTime = 0.0f;

        private void Awake()
        {
            _damageText = GetComponent<TMP_Text>();
        }

        public void SetDamageAndPosition(int damage, Vector3 position)
        {
            _damageText.text = damage.ToString();
            position.y += _offsetHeight; // Apply the offset height
            transform.position = position;
            _elapsedTime = 0.0f;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            transform.position += Vector3.up * _floatSpeed * Time.deltaTime;

            float alpha = Mathf.Lerp(1.0f, 0.0f, _elapsedTime / _fadeDuration);
            _damageText.color = new Color(_damageText.color.r, _damageText.color.g, _damageText.color.b, alpha);

            if (_elapsedTime >= _fadeDuration)
            {
                DamageNumbersObjectPool.Instance.Release(this);
            }
        }
    }
}