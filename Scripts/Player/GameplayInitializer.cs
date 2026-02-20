using UnityEngine;
using Core;

namespace Player
{
    public class GameplayInitializer : MonoBehaviour
    {
        private PlayerController controller;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
            controller.enabled = false; // כבוי עד תחילת משחק
        }

        private void OnEnable()
        {
            SceneFlowController.OnGameplayStarted += StartGameplay;
        }

        private void OnDisable()
        {
            SceneFlowController.OnGameplayStarted -= StartGameplay;
        }

        private void StartGameplay()
        {
            MoveToSpawn();
            controller.enabled = true;
        }

        private void MoveToSpawn()
        {
            GameObject spawn = GameObject.Find("SpawnPoint");

            if (spawn != null)
            {
                transform.position = spawn.transform.position;
                transform.rotation = spawn.transform.rotation;
            }
            else
            {
                Debug.LogWarning("SpawnPoint not found.");
            }
        }
    }
}
