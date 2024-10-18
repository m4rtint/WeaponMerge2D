using TMPro;
using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface.WaveModeUI
{
    public struct WaveModeData
    {
        public int WaveNumber;
    }
    
    public class HUDWaveView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _waveNumberLabel;

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
        }

        public void Set(WaveModeData modeData)
        {
            _waveNumberLabel.text = $"Wave {modeData.WaveNumber}";
        }
    }
}