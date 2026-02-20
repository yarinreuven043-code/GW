using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [System.Serializable]
    public class SceneTransformProfile
    {
        public string sceneName;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale = Vector3.one;
    }

    public class PlayerSceneTransformHandler : MonoBehaviour
    {
        [Header("Scene Profiles")]
        public SceneTransformProfile[] profiles;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ApplyProfile(scene.name);
        }

        private void ApplyProfile(string sceneName)
        {
            foreach (var profile in profiles)
            {
                if (profile.sceneName == sceneName)
                {
                    transform.position = profile.position;
                    transform.rotation = Quaternion.Euler(profile.rotation);
                    transform.localScale = profile.scale;

                    Debug.Log("[PlayerSceneTransformHandler] Applied profile for: " + sceneName);
                    return;
                }
            }
        }
    }
}
