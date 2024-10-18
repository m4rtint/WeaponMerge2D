using System;
using System.Collections;
using _WeaponMerge.Scripts.Managers.Data;
using _WeaponMerge.Scripts.Managers.Domain;
using _WeaponMerge.Scripts.Managers.Domain.UseCases;
using _WeaponMerge.Tools;
using TMPro;
using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface.WaveModeUI
{
    public class HUDWaveView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _waveNumberLabel;
        [SerializeField] private TMP_Text _waveAnnouncementLabel;
        [SerializeField] private TMP_Text _remainingEnemiesLabel;
        [SerializeField] private TMP_Text _enemiesKilledLabel;
        
        private static HUDWaveView _instance;
        public static HUDWaveView Instance => _instance;
        
        private GetWaveModeDataUseCase _getWaveModeDataUseCase; 

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
            
            var waveRepository = new WaveModeRepository();
            _getWaveModeDataUseCase = new GetWaveModeDataUseCase(waveRepository);
        }

        private void Start()
        {
            _waveAnnouncementLabel.gameObject.SetActive(false);
        }

        private void Update()
        {
            var data = _getWaveModeDataUseCase.Execute();
            Render(data);
        }

        private void Render(WaveModeData data)
        {
            _waveNumberLabel.text = $"Wave {data.RoundNumber}";
            _waveAnnouncementLabel.text = $"Wave {data.RoundNumber} starting!";
            _remainingEnemiesLabel.text = $"Remaining: {data.ActiveEnemies}";
            _enemiesKilledLabel.text = $"Killed: {data.KilledEnemies}";
        }

        public void ShowAnnouncement()
        {
            _waveAnnouncementLabel.gameObject.SetActive(true);
            StartCoroutine(FadeOutAnnouncement());
        }

        private IEnumerator FadeOutAnnouncement()
        {
            yield return new WaitForSeconds(2f);

            float fadeDuration = 1f;
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