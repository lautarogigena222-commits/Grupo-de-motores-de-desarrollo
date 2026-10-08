using UnityEngine;
using UnityEngine.SceneManagement;

public class E : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
