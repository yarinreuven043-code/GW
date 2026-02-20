using UnityEngine;

namespace Audio
{
    public class AutoPlayMenuTheme : MonoBehaviour
    {
        private void Start()
        {
            MusicManager musicManager = FindObjectOfType<MusicManager>();

            if (musicManager == null)
            {
                Debug.LogWarning("AutoPlayMenuTheme: MusicManager not found.");
                return;
            }

            musicManager.PlayMusic(musicManager.menuTheme);
        }
    }
}
