using System.Collections;
using _WeaponMerge.Tools;
using TMPro;
using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface.WaveModeUI
{
    public struct WaveModeData
    {
        public int WaveNumber;
        
        public WaveModeData(int waveNumber)
        {
            WaveNumber = waveNumber;
        }
    }
    
    public class HUDWaveView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _waveNumberLabel;
        [SerializeField] private TMP_Text _waveAnnouncementLabel;

        private static HUDWaveView _instance;
        public static HUDWaveView Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            
            PanicHelper.CheckAndPanicIfNull(_waveNumberLabel);
            PanicHelper.CheckAndPanicIfNull(_waveAnnouncementLabel);
        }

        private void Start()
        {
            _waveAnnouncementLabel.gameObject.SetActive(false);
        }

        public void Set(WaveModeData modeData)
        {
            _waveNumberLabel.text = $"Wave {modeData.WaveNumber}";
            _waveAnnouncementLabel.text = $"Wave {modeData.WaveNumber} starting!";
        }

        public void ShowAnnouncement()
        {
            _waveAnnouncementLabel.gameObject.SetActive(true);
            StartCoroutine(FadeOutAnnouncement());
        }

        private IEnumerator FadeOutAnnouncement()
        {
            yield return new WaitForSeconds(2f);

            float fadeDuration = 2f;
            float elapsedTime = 0f;
            Color originalColor = _waveAnnouncementLabel.color;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                _waveAnnouncementLabel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            _waveAnnouncementLabel.gameObject.SetActive(false);
        }
    }
}