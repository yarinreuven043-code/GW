using UnityEngine;
using System;

namespace Core
{
    public enum GameState
    {
        Menu,
        CharacterCreation,
        Gameplay
    }

    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;

        public static event Action<GameState> OnStateChanged;

        public GameState CurrentState { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void SetState(GameState newState)
        {
            if (CurrentState == newState)
                return;

            CurrentState = newState;
            OnStateChanged?.Invoke(newState);

            Debug.Log("[GameState] -> " + newState);
        }
    }
}
