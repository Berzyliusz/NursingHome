using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    [CreateAssetMenu(menuName = "Utilities/SceneLoader")]
    public class SceneLoader : ScriptableObject
    {
        [SerializeField] int GameSceneIndex = 1;
        [SerializeField] int MainMenuIndex = 0; 

        public void LoadGameScene()
        {
            SceneManager.LoadScene(GameSceneIndex);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenuIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}