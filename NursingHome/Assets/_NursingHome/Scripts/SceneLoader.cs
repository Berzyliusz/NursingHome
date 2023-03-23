using UnityEngine;
using UnityEngine.SceneManagement;

namespace NursingHome
{
    public class SceneLoader : MonoBehaviour
    {


        public void LoadGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}