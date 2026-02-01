using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class UIMainMenu : MonoBehaviour
    {

        public void OnButtonPlayClicked()
        {
            SceneManager.LoadScene(1);
        }
        
    }
}
