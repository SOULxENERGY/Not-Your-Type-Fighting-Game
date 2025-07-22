using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Reload()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToHome()
    {
        SceneManager.LoadScene(0);
    }
}
