using System;
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

        private bool _isAnnouncementActive;
        private float _announcementTimer;
        private readonly float _fadeDuration = 3f;
        private Action _onAnnouncementComplete;
        private bool _isFadingOut;
        private float _fadeTimer;

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
            _waveAnnouncementLabel.gameObject.SetActive(false);
        }

        private void Update()
        {
            var data = _getWaveModeDataUseCase.Execute();
            Render(data);
            HandleAnnouncement();
            HandleFadeOut();
        }

        private void Render(WaveModeData data)
        {
            _waveNumberLabel.text = $"Wave {data.RoundNumber}";
            _waveAnnouncementLabel.text = $"Wave {data.RoundNumber} starting!";
            _remainingEnemiesLabel.text = $"Remaining: {data.ActiveEnemies}";
            _enemiesKilledLabel.text = $"Killed: {data.KilledEnemies}";
        }

        public void ShowAnnouncement(float delayAnnouncement, Action onAnnouncementComplete)
        {
            _waveAnnouncementLabel.gameObject.SetActive(true);
            _isAnnouncementActive = true;
            _announcementTimer = delayAnnouncement;
            _onAnnouncementComplete = onAnnouncementComplete;
        }

        private void HandleAnnouncement()
        {
            if (_isAnnouncementActive)
            {
                _announcementTimer -= Time.deltaTime;
                if (_announcementTimer <= 0)
                {
                    _announcementTimer = _fadeDuration;
                    _isAnnouncementActive = false;
                    StartFadeOut();
                }
            }
        }

        private void HandleFadeOut()
        {
            if (_isFadingOut)
            {
                _fadeTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, _fadeTimer / _fadeDuration);
                _waveAnnouncementLabel.color = new Color(_waveAnnouncementLabel.color.r, _waveAnnouncementLabel.color.g, _waveAnnouncementLabel.color.b, alpha);

                if (_fadeTimer >= _fadeDuration)
                {
                    _isFadingOut = false;
                    _waveAnnouncementLabel.gameObject.SetActive(false);
                    _onAnnouncementComplete.Invoke();
                }
            }
        }

        private void StartFadeOut()
        {
            _isFadingOut = true;
            _fadeTimer = 0f;
        }
    }
}