using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{

    public void OpenPauseMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnContinueButtonClicked()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        GameManager.Instance.SetPause(false);
    }
    
    
    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
    
    public void ClosePauseMenu()
    {
        OnContinueButtonClicked();
    }
    
}

