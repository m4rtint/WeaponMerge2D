using System;

namespace _WeaponMerge.Scripts.Managers
{
    public enum GameState
    {
        Loading,
        InGame,
        Paused,
        GameOver,
        Restarting
    }

    public class GameStateManager
    {
        private static GameStateManager _instance;
        public Action<GameState> OnGameStateChanged;
        private GameState _currentState;

        private GameStateManager()
        {
            _currentState = GameState.Loading;
        }

        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        public GameState GetState()
        {
            return _currentState;
        }

        public void ChangeState(GameState newState)
        {
            if (_currentState != newState)
            {
                _currentState = newState;
                OnGameStateChanged?.Invoke(_currentState);
            }
        }
    }
}