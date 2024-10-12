using System;
using _WeaponMerge.Tools;

namespace _WeaponMerge.Scripts.Managers
{
    public enum GameState
    {
        InitialState,
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
            _currentState = GameState.InitialState;
        }

        public static GameStateManager Instance => _instance ??= new GameStateManager();

        public GameState GetState()
        {
            return _currentState;
        }
        
        public void ChangeState(GameState newState)
        {
            if (_currentState != newState)
            {
                Logger.Log($"Changing state from {_currentState} to {newState}", LogKey.State, color: LogColor.Red);
                _currentState = newState;
                OnGameStateChanged?.Invoke(_currentState);
            }
        }
    }
}