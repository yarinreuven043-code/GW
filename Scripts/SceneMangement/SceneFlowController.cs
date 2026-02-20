using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace SceneManagement
{
    public class SceneFlowController : MonoBehaviour
    {
        public static SceneFlowController Instance;

        public static event Action OnGameplayStarted;
		
		private Scene _currentLoadedScene;
		
        [Header("Scenes")]
        public string mainMenuScene = "Main Menu";
        public string characterCreationScene = "Character Creation";
        public string cityScene = "Summerlake City";

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            LoadMainMenu();
        }

        public void LoadMainMenu()
		{
			StartCoroutine(LoadInitialScene(mainMenuScene));
			GameStateManager.Instance.SetState(GameState.Menu);
		}

		private System.Collections.IEnumerator LoadInitialScene(string sceneName)
		{
			yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

			_currentLoadedScene = SceneManager.GetSceneByName(sceneName);
			SceneManager.SetActiveScene(_currentLoadedScene);
		}

        public void LoadCharacterCreation()
        {
            StartCoroutine(SwitchScene(characterCreationScene));
			GameStateManager.Instance.SetState(GameState.CharacterCreation);
        }

        public void LoadCity()
        {
            StartCoroutine(SwitchScene(cityScene, true));
			GameStateManager.Instance.SetState(GameState.Gameplay);
        }

        private System.Collections.IEnumerator LoadSceneAdditive(string sceneName)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
                yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(loadedScene);
        }

		private System.Collections.IEnumerator SwitchScene(string sceneName, bool startGameplay = false)
		{
			yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

			Scene newScene = SceneManager.GetSceneByName(sceneName);
			SceneManager.SetActiveScene(newScene);

			if (_currentLoadedScene.IsValid())
			{
				yield return SceneManager.UnloadSceneAsync(_currentLoadedScene);
			}

			_currentLoadedScene = newScene;

			if (startGameplay)
				OnGameplayStarted?.Invoke();
		}
    }
}
