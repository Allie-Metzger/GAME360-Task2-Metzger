using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GAME360Project.Assets.Scripts
{

    public class ManagingScenes : MonoBehaviour
    {

        public static ManagingScenes Instance { get; private set; }

        public void PlayFirstLevel()

        {
            SceneManager.LoadScene("Level");
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("StartMenu");

        }

        public void GoToCredits()
        {
            SceneManager.LoadScene("Credits");
        }

        public void Continue()
        {
            // Get the current scene's build index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
