using UnityEngine;
using TMPro;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class DamageNumberBehaviour : MonoBehaviour
    {
        [SerializeField] private float _floatSpeed = 1.0f;
        [SerializeField] private float _offsetHeight = 1.0f;
        [SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.EaseInOut(0, 0.25f, 1, 1.2f);
        [SerializeField] private AnimationCurve _easeOutBackCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        private TMP_Text _damageText;
        private float _elapsedTime = 0.0f;
        private const float _totalDuration = 1.0f;

        private void Awake()
        {
            _damageText = GetComponent<TMP_Text>();
        }

        public void SetDamageAndPosition(int damage, Vector3 position)
        {
            _damageText.text = damage.ToString();
            position.y += _offsetHeight;
            transform.position = position;
            _elapsedTime = 0.0f;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            transform.position += Vector3.up * _floatSpeed * Time.deltaTime;

            float progress = Mathf.Clamp01(_elapsedTime / _totalDuration);
            float easedProgress = _easeOutBackCurve.Evaluate(progress);

            float alpha = progress < 0.8f ? 1.0f : Mathf.Lerp(1.0f, 0.0f, (progress - 0.8f) / 0.2f);
            _damageText.color = new Color(_damageText.color.r, _damageText.color.g, _damageText.color.b, alpha);

            float scale = _scaleCurve.Evaluate(progress);
            transform.localScale = new Vector3(scale, scale, scale);

            if (_elapsedTime >= _totalDuration)
            {
                DamageNumbersObjectPool.Instance.Release(this);
            }
        }
    }
}